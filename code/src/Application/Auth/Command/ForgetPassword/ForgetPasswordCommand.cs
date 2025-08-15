using System.Web;
using Microsoft.Extensions.Configuration;
using ThirdPartyAPIs.Application.Common.Interfaces;
using ThirdPartyAPIs.Application.Common.Models;
using ThirdPartyAPIs.Domain.Common;
using ThirdPartyAPIs.Domain.Enums;

namespace ThirdPartyAPIs.Application.Auth.Command.ForgetPassword;

public record ForgetPasswordCommand : IRequest<ResponseBase>
{
    public required string Email { get; set; }
}

public class ForgetPasswordCommandValidator : AbstractValidator<ForgetPasswordCommand>
{
    public ForgetPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
       .NotEmpty().WithMessage("Email is required.");
    }
}


public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, ResponseBase>
{
    private readonly IIdentityService _identityService;
    private readonly IEmailSenderRepository _emailSenderRepository;
    private readonly IConfiguration _configuration;

    public ForgetPasswordCommandHandler(IIdentityService identityService, IEmailSenderRepository emailSenderRepository, IConfiguration configuration)
    {
        _identityService = identityService;
        _emailSenderRepository = emailSenderRepository;
        _configuration = configuration;
    }

    private ResponseBase ErrorResponse(string error)
    {
        return new ResponseBase
        {
            Status = false,
            Error = error
        };
    }
    public async Task<ResponseBase> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _identityService.GetUserByEmail(request.Email);
            if (user == null)
                return ErrorResponse(AppMessage.EmailIsIncorrect.GetDescription());

            var unEncodedToken = await _identityService.CreatePasswordResetToken(user.Id);
            var token = HttpUtility.UrlEncode(unEncodedToken);

            var feBaseUrl = _configuration.GetValue<string>("AppConfig:FEBaseURL");
            var forgetPasswordUrl = string.Format("{0}/auth/forget-password?type=verifyemail&code={1}&id={2}&email={3}", feBaseUrl, token, user.Id, HttpUtility.UrlEncode(request.Email));

            string body = string.Format("Hi {0},<br /><br /><a href='{1}'>Click Here</a> to Reset Password. <br /><br />Thanks,<br />ThirdPartyAPIs Admin", user.FirstName ?? string.Empty, feBaseUrl);

            // SEND Registration Complete Email
            await _emailSenderRepository.SendEmailAsync(request.Email ?? string.Empty, "Reset Password", body);

            return new ResponseBase()
            {
                Status = true,
                Data = new ForgetPasswordResponse
                {
                    UserId = user.Id,
                    UnencodeToken = unEncodedToken,
                    Token = token,
                },
                Message = AppMessage.PasswordResetEmailSent.GetDescription()
            };

        }
        catch (Exception ex)
        {
            return ErrorResponse(ex.Message);
        }
    }
}

public class ForgetPasswordResponse
{
    public int UserId { get; set; }
    public string? UnencodeToken { get; set; }
    public string? Token { get; set; }
}
