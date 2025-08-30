using Microsoft.AspNetCore.Identity;
using OpenProfileAPI.Application.Common.Interfaces;
using OpenProfileAPI.Application.Common.Models;
using OpenProfileAPI.Domain.Common;
using OpenProfileAPI.Domain.Entities;
using OpenProfileAPI.Domain.Enums;

namespace OpenProfileAPI.Application.Auth.Command.CompleteForgetPassword;

public class CompleteForgetPasswordCommand : IRequest<ResponseBase>
{
    public int UserId { get; set; }
    public string Token { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class CompleteForgetPasswordValidator : AbstractValidator<CompleteForgetPasswordCommand>
{
    public CompleteForgetPasswordValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(AppMessage.PasswordCannotBeEmpty.GetDescription())
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
    }
}

public class CompleteForgetPasswordHandler : IRequestHandler<CompleteForgetPasswordCommand, ResponseBase>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    public CompleteForgetPasswordHandler(IApplicationDbContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    private ResponseBase ErrorResponse(string error)
    {
        return new ResponseBase
        {
            Status = false,
            Error = error
        };
    }

    public async Task<ResponseBase> Handle(CompleteForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        var token = request.Token.Replace(' ', '+');

        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return ErrorResponse(AppMessage.UserNotFound.GetDescription());

        var result = await _userManager.ResetPasswordAsync(user, token, request.Password);

        if (!result.Succeeded)
        {
            var errorMessages = string.Join("; ", result.Errors.Select(e => e.Description));
            return ErrorResponse(errorMessages);
        }

        return new ResponseBase
        {
            Status = true,
            Message = AppMessage.PasswordResetSuccessfully.GetDescription()
        };
    }
}
