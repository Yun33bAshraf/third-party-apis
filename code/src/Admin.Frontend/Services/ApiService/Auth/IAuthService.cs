using IApply.Frontend.Models;
using IApply.Frontend.Models.Auth;
using IApply.Frontend.Models.Auth._2Factor;
using IApply.Frontend.Models.Auth.Login;
using IApply.Frontend.Models.Auth.Otp;
using IApply.Frontend.Models.User;
using IApply.Frontend.Models.User.ERPUsers.CompleteRegistration;

namespace IApply.Frontend.Services.ApiService.Auth;

public interface IAuthService
{
    Task<LoginResponse?> Login(LoginRequest request);
    Task<BaseResponse?> CreateBackendUser(CreateBackendUserRequest request);

    Task<GetUserProfileResponse?> GetProfile();
    //Task<BaseResponse?> CreateBackendAsset(CreateAssetRequest request);
    //Task<BaseResponse?> UpdateAsset(CreateAssetRequest request);
    Task<LoginResponse?> ChangePassword(ChangePasswordRequest request);
    Task<LoginResponse?> CompleteRegistration(CompleteRegistrationRequest request);
    Task<LoginResponse?> OtpVerification(VerifyOtpRequest request);
    Task<LoginResponse?> Verify2Factor(VerifyTwoFactorRequest request);
    Task<ConfigureAuthenticatorResponse?> ConfigureAuthenticator(ConfigureAuthenticatorRequest request);
    Task<SendOtpResponse?> SendOtp(SendOtpRequest request);
    Task<LoginResponse?> RefreshToken(RefreshTokenRequest request);
}
