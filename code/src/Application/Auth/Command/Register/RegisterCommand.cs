using System.Web;
using OpenProfileAPI.Application.Common.Interfaces;
using OpenProfileAPI.Application.Common.Models;
using OpenProfileAPI.Domain.Common;
using OpenProfileAPI.Domain.Entities;
using OpenProfileAPI.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace OpenProfileAPI.Application.Auth.Command.Register;

public record RegisterCommand : IRequest<ResponseBase>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName)
           .NotEmpty().WithMessage("Name is required.")
           .NotNull().WithMessage("Name must required")
           .MaximumLength(100).WithMessage("Name must not exceed 50 characters.");

        RuleFor(x => x.LastName)
           .NotEmpty().WithMessage("Name is required.")
           .NotNull().WithMessage("Name must required")
           .MaximumLength(100).WithMessage("Name must not exceed 50 characters.");

        RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email is required.")
           .NotNull().WithMessage("Email must required")
           .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

        RuleFor(x => x.Password)
           .NotEmpty().WithMessage("Password is required.")
           .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
           .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
           .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
           .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
           .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.")
           .Matches(@"^\S+$").WithMessage("Password must not contain whitespace.")
           .Must(password => !CommonPasswords.Contains(password.ToLower()))
               .WithMessage("Password is too common. Please choose a stronger one.")
           .Must((model, password) =>
           {
               var loweredPassword = password.ToLower();
               var emailPrefix = model.Email?.Split('@')[0].ToLower() ?? "";
               var firstName = model.FirstName?.ToLower() ?? "";
               var lastName = model.LastName?.ToLower() ?? "";

               return !loweredPassword.Contains(emailPrefix)
                      && !loweredPassword.Contains(firstName)
                      && !loweredPassword.Contains(lastName);
           }).WithMessage("Password must not contain your name or email.");
    }

    private static readonly HashSet<string> CommonPasswords =
    [
        "password", "123456", "123456789", "qwerty", "12345678", "111111", "123123",
        "abc123", "password1", "1234", "admin", "letmein", "welcome", "iloveyou"
    ];
}

public class RegisterCommandHandler(
    UserManager<User> userManager,
    IApplicationDbContext dbContext,
    IEmailSenderRepository emailRepo) : IRequestHandler<RegisterCommand, ResponseBase>
{
    private ResponseBase ErrorResponse(string error)
    {
        return new ResponseBase
        {
            Status = false,
            Error = error
        };
    }

    public async Task<ResponseBase> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            return ErrorResponse(AppMessage.AccountAlreadyExists.GetDescription());

        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            IsProfileCompleted = false,
            TwoFactorEnabled = true,
            AuthKey = TypeExtensions.GenerateRandomPassword(),
            SmsKey = TypeExtensions.GenerateRandomPassword(),
            EmailKey = TypeExtensions.GenerateRandomPassword(),
            UserTypeId = (int)UserType.Customer,
            Created = DateTime.UtcNow,
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errorMessages = string.Join(" | ", result.Errors.Select(e => e.Description));
            return ErrorResponse(errorMessages);
        }

        await SendVerificationEmailAsync(user);

        UserProfile userProfile = new()
        {
            UserId = user.Id,
            DisplayName = $"{request.FirstName} {request.LastName}",
            Email = request.Email,
            Created = DateTime.UtcNow,
        };

        UserRole userRole = new()
        {
            UserId = user.Id,
            RoleId = (int)UserType.Customer,
            Created = DateTime.UtcNow,
        };

        dbContext.UserProfile.Add(userProfile);
        dbContext.UserRole.Add(userRole);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ResponseBase
        {
            Status = true,
            Data = user.Id,
            Message = AppMessage.AccountCreatedSuccessfully.GetDescription()
        };
    }

    public async Task SendVerificationEmailAsync(User user)
    {
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = HttpUtility.UrlEncode(token);

        var verificationLink = $"https://localhost:7001/verify-email?userId={user.Id}&token={encodedToken}";
        //var verificationLink = $"https://your-app.com/verify-email?userId={user.Id}&token={encodedToken}";

        // Subject
        var subject = "Verify your email address to complete your registration";

        // Email HTML body
        var htmlBody = $@"
        <!DOCTYPE html>
        <html>
        <head>
          <meta charset='UTF-8'>
          <title>Email Verification</title>
        </head>
        <body style='font-family: Arial, sans-serif; background-color: #f9f9f9; padding: 30px;'>
          <table width='100%' cellspacing='0' cellpadding='0'>
            <tr>
              <td align='center'>
                <table style='max-width: 600px; background-color: #ffffff; border-radius: 8px; padding: 40px; box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);'>
                  <tr>
                    <td align='center' style='padding-bottom: 20px;'>
                      <h2 style='color: #333;'>Welcome to OpenProfileAPI 👋</h2>
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <p>Hi <strong>{user.FirstName}</strong>,</p>
                      <p>Thank you for registering with <strong>OpenProfileAPI</strong>. To get started, please verify your email address by clicking the button below:</p>
                      <p style='text-align: center; margin: 30px 0;'>
                        <a href='{verificationLink}' style='background-color: #0066ff; color: #ffffff; padding: 12px 24px; border-radius: 4px; text-decoration: none;'>Verify Email</a>
                      </p>
                      <p>If the button above doesn’t work, please copy and paste the following URL into your browser:</p>
                    <p style='word-break: break-all;'>
                        <span style='display: inline-block; background-color: #f1f1f1; color: #0066ff; padding: 8px 16px; border-radius: 4px; cursor: text; text-decoration: underline;'>Copy URL</span>
                        <span style='font-size: 0; color: transparent; user-select: text;'>{verificationLink}</span>
                    </p>
                      <p>If you didn’t create an account, no further action is required.</p>
                      <p style='margin-top: 40px;'>Best regards,<br/>The OpenProfileAPI Team</p>
                    </td>
                  </tr>
                  <tr>
                    <td style='padding-top: 30px; font-size: 12px; color: #777777;'>
                      <p>This is an automated message, please do not reply.</p>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </body>
        </html>";

        // Send email
        await emailRepo.SendEmailAsync(user.Email ?? string.Empty, subject, htmlBody);
    }
}

