using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using IApply.Frontend.Common.Constants;
using IApply.Frontend.Models;
using IApply.Frontend.Models.Auth;
using IApply.Frontend.Models.Auth._2Factor;
using IApply.Frontend.Models.Auth.Login;
using IApply.Frontend.Models.Auth.Otp;
using IApply.Frontend.Models.User;
using IApply.Frontend.Models.User.ERPUsers.CompleteRegistration;
using IApply.Frontend.Services.ApiService.Me;

namespace IApply.Frontend.Services.ApiService.Auth;

public class AuthService : IAuthService
{
    private readonly ApiService _apiService;
    private readonly AuthResponseService _authResponseService;
    private readonly NavigationManager _navigationManager;
    private readonly ISessionStorageService _sessionStorage;
    private readonly AlertService _alertService;
    private readonly HttpClient _httpClient;
    private readonly IMeService _meService;


    public AuthService(ApiService apiService, AuthResponseService authResponseService, NavigationManager navigationManager, ISessionStorageService sessionStorageService, AlertService alertService, HttpClient httpClient, IMeService meService)
    {
        _apiService = apiService;
        _authResponseService = authResponseService;
        _navigationManager = navigationManager;
        _sessionStorage = sessionStorageService;
        _alertService = alertService;
        _httpClient = httpClient;
        _meService = meService;
    }

    public async Task<LoginResponse?> Login(LoginRequest request)
    {
        var loginResponse = new LoginResponse();
        var response = await _apiService.PostAsync(ApiEndpoints.Auth.Login, request);
        loginResponse = await response.Response.ReadFromJsonAsync<LoginResponse>();
        if (loginResponse != null && loginResponse.Data.IsSuccess && !loginResponse.Data.Enforce2FactorConfiguration && !loginResponse.Data.Enforce2FactorVerification)
        {
            await ProcessLoginResponse(loginResponse);

        }
        else if (loginResponse != null && loginResponse.Data.IsSuccess && loginResponse.Data.Enforce2FactorConfiguration)
        {
            await _sessionStorage.SetItemAsync("Token", loginResponse.Data.AccessToken);
            await _sessionStorage.SetItemAsync("RefreshToken", loginResponse.Data.RefreshToken);
            _navigationManager.NavigateTo("2Factor");
        }
        else if (loginResponse != null && loginResponse.Data.IsSuccess && loginResponse.Data.Enforce2FactorVerification)
        {
            await _sessionStorage.SetItemAsync("Token", loginResponse.Data.AccessToken);
            await _sessionStorage.SetItemAsync("RefreshToken", loginResponse.Data.RefreshToken);
            _navigationManager.NavigateTo("otp-verification");
        }
        else
        {
            string errorMessage = loginResponse?.Error != null ? loginResponse.Error : "An unknown error occurred.";
            _alertService.ShowError(errorMessage);
        }
        return loginResponse;
    }
    public async Task<BaseResponse?> CreateBackendUser(CreateBackendUserRequest request)
    {
        var baseResponse = new BaseResponse();
        var response = await _apiService.PostAsync(ApiEndpoints.Auth.CreateBackendUser, request);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
        }

        return baseResponse;

    }

    public async Task<GetUserProfileResponse?> GetProfile()
    {
        var baseResponse = new GetUserProfileResponse();
        var response = await _apiService.GetAsync(ApiEndpoints.Auth.GetProfile);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<GetUserProfileResponse>();
        }
        return baseResponse;
    }




    //public async Task<BaseResponse?> CreateBackendAsset(CreateAssetRequest request)
    //{
    //    var baseResponse = new BaseResponse();
    //    var response = await _apiService.PostAsync(ApiEndpoints.Assets.PostAssets, request);

    //    if (response.StatusCode == 200)
    //    {
    //        baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
    //    }

    //    return baseResponse;

    //}

    //public async Task<BaseResponse?> UpdateAsset(CreateAssetRequest request)
    //{
    //    var baseResponse = new BaseResponse();
    //    var response = await _apiService.PutAsync(ApiEndpoints.Assets.UpdateAssets, request);

    //    if (response.StatusCode == 200)
    //    {
    //        baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
    //    }

    //    return baseResponse;

    //}

    public async Task<LoginResponse?> ChangePassword(ChangePasswordRequest request)
    {
        var baseResponse = new LoginResponse();
        var response = await _apiService.PostAsync(ApiEndpoints.Auth.ChangePassword, request);
        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<LoginResponse>();
            if (baseResponse != null && baseResponse.Status)
            {
                _alertService.Show("Password Changed Successfully");
                _navigationManager.NavigateTo("home");
            }
            else if (response != null)
            {
                string errorMessage = baseResponse?.Error != null ? baseResponse.Error : "An unknown error occurred.";
                _alertService.ShowError(errorMessage);
            }
        }
        return baseResponse;
    }
    public async Task<LoginResponse?> CompleteRegistration(CompleteRegistrationRequest request)
    {
        var baseResponse = new LoginResponse();
        var response = await _apiService.PostAsync(ApiEndpoints.Auth.CompleteRegistration, request);
        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<LoginResponse>();
            if (baseResponse != null && baseResponse.Status)
            {
                _alertService.Show("Registration Successfully");
                _navigationManager.NavigateTo("home");
            }
            else if (response != null)
            {
                string errorMessage = baseResponse?.Error != null ? baseResponse.Error : "An unknown error occurred.";
                _alertService.ShowError(errorMessage);
            }
        }
        return baseResponse;
    }
    public async Task<LoginResponse?> OtpVerification(VerifyOtpRequest request)
    {
        var baseResponse = new LoginResponse();
        var response = await _apiService.PostAsync(ApiEndpoints.Auth.VerifyOtp, request);
        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<LoginResponse>();
            if (baseResponse != null && baseResponse.Status)
            {
                await ProcessLoginResponse(baseResponse);
            }
        }
        return baseResponse;
    }
    public async Task<LoginResponse?> Verify2Factor(VerifyTwoFactorRequest request)
    {
        var baseResponse = new LoginResponse();
        var response = await _apiService.PostAsync(ApiEndpoints.Auth.Verify2Factor, request);
        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<LoginResponse>();
            if (baseResponse != null && baseResponse.Status)
            {
                await ProcessLoginResponse(baseResponse);
            }
            else
            {
                string errorMessage = baseResponse?.Error != null ? baseResponse.Error : "An unknown error occurred.";
                _alertService.ShowError(errorMessage);
            }
        }
        return baseResponse;
    }
    public async Task<ConfigureAuthenticatorResponse?> ConfigureAuthenticator(ConfigureAuthenticatorRequest request)
    {
        var baseResponse = new ConfigureAuthenticatorResponse();
        var token = await _sessionStorage.GetItemAsync<string>("Token");
        if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
        {
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
        }
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var response = await _httpClient.PostAsJsonAsync(_apiService._baseApiUrl + ApiEndpoints.Auth.Configure2Factor, request);
        if (response.IsSuccessStatusCode)
        {
            baseResponse = await response.Content.ReadFromJsonAsync<ConfigureAuthenticatorResponse>();

        }
        return baseResponse;
    }
    public async Task<SendOtpResponse?> SendOtp(SendOtpRequest request)
    {
        var baseResponse = new SendOtpResponse();
        var token = await _sessionStorage.GetItemAsync<string>("Token");
        if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
        {
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
        }
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var response = await _httpClient.PostAsJsonAsync(_apiService._baseApiUrl + ApiEndpoints.Auth.SendOtp, request);
        if (response.IsSuccessStatusCode)
        {
            baseResponse = await response.Content.ReadFromJsonAsync<SendOtpResponse>();

        }
        return baseResponse;
    }

    private async Task ProcessLoginResponse(LoginResponse loginResponse)
    {
        var guid = Guid.NewGuid();
        /*var rightsResponse = await _meService.GetMyRights(loginResponse.AccessToken);
        if (rightsResponse != null && rightsResponse.IsSuccess)
        {
            var rights = rightsResponse.Rights;
            _authResponseService.Rights[guid.ToString()] = rights;
        }*/
        _authResponseService.Responses[guid.ToString()] = loginResponse;
        _navigationManager.NavigateTo($"login-redirect/{guid.ToString()}", true);
    }

    public async Task<LoginResponse?> RefreshToken(RefreshTokenRequest request)
    {
        var baseResponse = new LoginResponse();
        var token = await _sessionStorage.GetItemAsync<string>("RefreshToken");
        if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
        {
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
        }
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var response = await _httpClient.PostAsJsonAsync(_apiService._baseApiUrl + ApiEndpoints.Auth.RefreshToken, request);
        if (response.IsSuccessStatusCode)
        {
            baseResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            await _sessionStorage.SetItemAsync("Token", baseResponse?.Data?.AccessToken);
            await _sessionStorage.SetItemAsync("RefreshToken", baseResponse?.Data?.RefreshToken);

        }
        return baseResponse;
    }
}
