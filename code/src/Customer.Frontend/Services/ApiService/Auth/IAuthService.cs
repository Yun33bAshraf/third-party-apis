using IApply.Frontend.Models;
using IApply.Frontend.Models.Auth;
using IApply.Frontend.Models.Auth._2Factor;
using IApply.Frontend.Models.Auth.Login;
using IApply.Frontend.Models.Auth.Otp;
using IApply.Frontend.Models.Auth.Register;
using IApply.Frontend.Models.Auth.VerifyEmail;

namespace IApply.Frontend.Services.ApiService.Auth;

public interface IAuthService
{
    Task<BaseResponse<RegisterResponse?>> RegisterAsync(RegisterRequest request);
    Task<BaseResponse<VerifyEmailResponse>> VerifyEmail(VerifyEmailRequest request);
    Task<LoginResponse?> Login(LoginRequest request);
    Task<GetUserProfileResponse?> GetProfile();
    Task<LoginResponse?> ChangePassword(ChangePasswordRequest request);
    Task<LoginResponse?> OtpVerification(VerifyOtpRequest request);
    Task<LoginResponse?> Verify2Factor(VerifyTwoFactorRequest request);
    Task<ConfigureAuthenticatorResponse?> ConfigureAuthenticator(ConfigureAuthenticatorRequest request);
    Task<SendOtpResponse?> SendOtp(SendOtpRequest request);
    Task<LoginResponse?> RefreshToken(RefreshTokenRequest request);
}
