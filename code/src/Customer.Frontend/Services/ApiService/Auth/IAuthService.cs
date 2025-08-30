using IApply.Frontend.Models.Auth._2Factor;
using OpenProfileAPI.Frontend.Models;
using OpenProfileAPI.Frontend.Models.Auth;
using OpenProfileAPI.Frontend.Models.Auth.Login;
using OpenProfileAPI.Frontend.Models.Auth.Otp;
using OpenProfileAPI.Frontend.Models.Auth.Register;
using OpenProfileAPI.Frontend.Models.Auth.VerifyEmail;

namespace OpenProfileAPI.Frontend.Services.ApiService.Auth;

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
