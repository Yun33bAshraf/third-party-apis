using OpenProfileAPI.Application.Common.Interfaces;
using OpenProfileAPI.Application.Common.Models;
using OpenProfileAPI.Domain.Common;
using OpenProfileAPI.Domain.Entities;
using OpenProfileAPI.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace OpenProfileAPI.Application.Auth.Command.CompleteSignUp;
public class SignUpCompleteCommand : IRequest<ResponseBase>
{
    public int UserId { get; set; }
    public required string Token { get; set; }
}

public class SignUpCompleteCommandValidator : AbstractValidator<SignUpCompleteCommand>
{
    public SignUpCompleteCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Verification token is required.");
    }
}

public class CompleteSignUpCommandHandler(UserManager<User> userManager, IEmailSenderRepository emailRepo) : IRequestHandler<SignUpCompleteCommand, ResponseBase>
{
    private ResponseBase ErrorResponse(string error)
    {
        return new ResponseBase
        {
            Status = false,
            Error = error
        };
    }

    public async Task<ResponseBase> Handle(SignUpCompleteCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return ErrorResponse(AppMessage.UserNotFound.GetDescription());

        if (user.EmailConfirmed)
            return new ResponseBase
            {
                Status = true,
                Message = AppMessage.EmailAlreadyConfirmed.GetDescription(),
            };

        var decodedToken = Uri.UnescapeDataString(request.Token);
        var result = await userManager.ConfirmEmailAsync(user, decodedToken);

        if (!result.Succeeded)
            return ErrorResponse(AppMessage.InvalidOrExpiredToken.GetDescription());

        user.EmailConfirmed = true;
        await userManager.UpdateAsync(user);

        await VerificationCompleteEmailAsync(user);

        return new ResponseBase
        {
            Status = true,
            Message = AppMessage.EmailVerificationSuccessful.GetDescription(),
        };
    }

    private async Task VerificationCompleteEmailAsync(User user)
    {
        var subject = "Your Email Has Been Successfully Verified";

        var htmlBody = $"""
            <p>Dear {user.FirstName},</p>

            <p>We’re pleased to let you know that your email address has been successfully verified.</p>

            <p>You can now log in and start using your account without any limitations.</p>

            <p>If you have any questions or need help, feel free to contact our support team.</p>

            <p>Best regards,<br/>
            The OpenProfileAPI Team</p>
            """;

        await emailRepo.SendEmailAsync(user.Email ?? string.Empty, subject, htmlBody);
    }
}

