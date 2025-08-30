using Microsoft.AspNetCore.Identity;
using OpenProfileAPI.Application.Common.Contracts;
using OpenProfileAPI.Application.Common.Interfaces;
using OpenProfileAPI.Application.Common.Models;
using OpenProfileAPI.Domain.Common;
using OpenProfileAPI.Domain.Entities;
using AuthPolicyModel = OpenProfileAPI.Domain.Entities.AuthPolicy;
using OpenProfileAPI.Domain.Enums;

namespace OpenProfileAPI.Application.Auth.Command.Login;

public record LoginCommand : IRequest<ResponseBase>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string ClientInformation { get; set; } = string.Empty;
    public string? CurrentBrowserTimeZone { get; set; } = string.Empty;
}

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]").WithMessage("Must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Must contain at least one number.")
            .Matches(@"[!@#$%^&*(),.?:{}|<>]").WithMessage("Must contain at least one special character.");
    }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseBase>
{
    private readonly ITokenRepository _tokenRepository;
    private readonly UserManager<User> _userManager;
    private readonly IQueryRepository<AuthPolicyModel> _authPolicyRepository;
    private readonly IDataRepository<LoginAttempts> _loginAttemptsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IQueryRepository<UserRole> _userRoleRepo;
    private readonly IQueryRepository<RoleRight> _roleRightRepo;

    public LoginCommandHandler(
        ITokenRepository tokenRepository,
        UserManager<User> userManager,
        IQueryRepository<AuthPolicyModel> authPolicyRepository,
        IDataRepository<LoginAttempts> loginAttemptsRepository,
        IUnitOfWork unitOfWork,
        IQueryRepository<UserRole> userRoleRepo,
        IQueryRepository<RoleRight> roleRightRepo
        )
    {
        _tokenRepository = tokenRepository;
        _userManager = userManager;
        _authPolicyRepository = authPolicyRepository;
        _loginAttemptsRepository = loginAttemptsRepository;
        _unitOfWork = unitOfWork;
        _userRoleRepo = userRoleRepo;
        _roleRightRepo = roleRightRepo;
    }

    private ResponseBase ErrorResponse(string error)
    {
        return new ResponseBase
        {
            Status = false,
            Error = error
        };
    }
    public async Task<ResponseBase> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Initialize login attempt
            LoginAttempts attempt = new()
            {
                IpAddress = request.IpAddress,
                ClientInformation = request.ClientInformation,
                AttemptDate = DateTime.UtcNow,
                LoginType = (int)LoginType.FreshLogin,
                TwoFacVerified = true
            };

            // Attempt to retrieve the user
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return ErrorResponse(AppMessage.EmailOrPasswordIsIncorrect.GetDescription());
            }

            //TODO: if the login api is different for admin & customer then apply check on user type here

            // Check for lockout
            if (await _userManager.IsLockedOutAsync(user))
            {
                attempt.IsSuccess = false;
                attempt.RejectionType = (int)RejectionType.AccountLocked;
                attempt.UserId = user.Id;

                await SaveAttempt(attempt);
                return ErrorResponse(AppMessage.AccountAlreadyLocked.GetDescription());
            }

            // Password verification
            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                await _userManager.AccessFailedAsync(user);

                if (user.AccessFailedCount >= 5)
                {
                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(15));
                }

                attempt.IsSuccess = false;
                attempt.RejectionType = (int)RejectionType.WrongPassword;
                attempt.UserId = user.Id;

                await SaveAttempt(attempt);
                return ErrorResponse(AppMessage.EmailOrPasswordIsIncorrect.GetDescription());
            }

            // Fetch policy
            var policy = await _authPolicyRepository.GetAsync(x => x.UserTypeId == user.UserTypeId);
            if (policy == null)
            {
                return ErrorResponse("Authentication policy not found.");
            }

            // Reset failed count
            await _userManager.ResetAccessFailedCountAsync(user);

            // Apply token policy
            var context = await _tokenRepository.ApplyPolicy(user, attempt, policy, request.CurrentBrowserTimeZone ?? string.Empty);
            context.Attempt.IsSuccess = true;
            context.Attempt.UserId = user.Id;

            user.LastLoginAttempt = context.Attempt.Id;
            user.LastLoginDate = DateTime.UtcNow;
            user.LastModifiedBy = user.Id;

            await SaveAttempt(context.Attempt);

            var authResponse = _tokenRepository.GetAuthResponse(user, context, policy);

            var userRole = await _userRoleRepo.GetAsync(x => x.UserId == user.Id);
            if (userRole == null)
                return ErrorResponse(AppMessage.UnableToFindRecord.GetDescription());

            var rights = await _roleRightRepo.GetAllAsync(x => x.RoleId == userRole.RoleId);

            var rightIds = rights.Select(r => r.RightId).ToList();

            return new ResponseBase()
            {
                Status = true,
                Data = new AuthResponse
                {
                    IsSuccess = true,
                    UserId = user.Id,
                    Name = $"{user.FirstName} {user.LastName}",
                    Email = user.Email ?? string.Empty,
                    UserTypeId = user.UserTypeId,
                    SessionId = attempt.Id,
                    AccessToken = authResponse.AccessToken,
                    RefreshToken = authResponse.RefreshToken,
                    ExpiryDate = authResponse.ExpiryDate,
                    EnforceEmailConfirmation = policy.EnforceEmailConfirmation,
                    EnforceMobileConfirmation = policy.EnforceMobileConfirmation,
                    Enforce2FactorVerification = policy.Enforce2FactorVerification && !attempt.TwoFacVerified,
                    EnforcePasswordChangeOnFirstLogin = policy.EnforcePasswordChangeOnFirstLogin,
                    EnforceProfileCompletion = policy.EnforceProfileCompletion,
                    IsProfileCompleted = user.IsProfileCompleted,
                    RoleId = userRole.RoleId,
                    Rights = rightIds
                },
                Message = AppMessage.LoginSuccessful.GetDescription()
            };
        }
        catch (Exception ex)
        {
            return ErrorResponse(ex.Message);
        }
    }

    private async Task SaveAttempt(LoginAttempts attempt)
    {
        attempt.CreatedBy = attempt.UserId;
        attempt.LastModifiedBy = attempt.UserId;

        _loginAttemptsRepository.Add(attempt, attempt.UserId);
        await _unitOfWork.SaveChangesAsync(default);
    }
}
