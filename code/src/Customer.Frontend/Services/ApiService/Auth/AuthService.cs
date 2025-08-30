using System.Text.Json;
using Blazored.SessionStorage;
using IApply.Frontend.Models.Auth._2Factor;
using Microsoft.AspNetCore.Components;
using OpenProfileAPI.Frontend.Common.Constants;
using OpenProfileAPI.Frontend.Models;
using OpenProfileAPI.Frontend.Models.Auth;
using OpenProfileAPI.Frontend.Models.Auth.Login;
using OpenProfileAPI.Frontend.Models.Auth.Otp;
using OpenProfileAPI.Frontend.Models.Auth.Register;
using OpenProfileAPI.Frontend.Models.Auth.VerifyEmail;
using OpenProfileAPI.Frontend.Services;
using OpenProfileAPI.Frontend.Services.ApiService.Me;

namespace OpenProfileAPI.Frontend.Services.ApiService.Auth;

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

    public async Task<BaseResponse<RegisterResponse?>> RegisterAsync(RegisterRequest request)
    {
        var baseResponse = new BaseResponse<RegisterResponse?>();

        try
        {
            var response = await _apiService.PostAsync(ApiEndpoints.Auth.Register, request);

            if (response.Response is not null && response.StatusCode == 200)
            {
                var intResponse = await response.Response.ReadFromJsonAsync<BaseResponse<int>>();

                baseResponse.Status = intResponse?.Status ?? false;
                baseResponse.Message = intResponse?.Message;
                baseResponse.Data = intResponse != null
                    ? new RegisterResponse { UserId = intResponse.Data }
                    : null;
            }
            else
            {
                baseResponse.Status = false;
                baseResponse.Message = $"Registration failed: {response.Response}";
            }
        }
        catch (Exception ex)
        {
            baseResponse.Status = false;
            baseResponse.Message = $"An error occurred: {ex.Message}";
        }

        return baseResponse;
    }

    public async Task<BaseResponse<VerifyEmailResponse>> VerifyEmail(VerifyEmailRequest request)
    {
        var baseResponse = new BaseResponse<VerifyEmailResponse>();

        try
        {
            var response = await _apiService.PostAsync(ApiEndpoints.Auth.VerifyEmail, request);

            if (response.Response != null)
            {
                var responseContent = await response.Response.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(responseContent))
                {
                    try
                    {
                        var parsedResponse = JsonSerializer.Deserialize<BaseResponse<VerifyEmailResponse>>(responseContent,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        if (parsedResponse != null)
                            baseResponse = parsedResponse;
                        else
                        {
                            baseResponse.Status = false;
                            baseResponse.Message = $"Failed to parse response JSON. Raw: {responseContent}";
                        }
                    }
                    catch (Exception jsonEx)
                    {
                        baseResponse.Status = false;
                        baseResponse.Message = $"JSON parse error: {jsonEx.Message}. Raw: {responseContent}";
                    }
                }
                else
                {
                    baseResponse.Status = false;
                    baseResponse.Message = $"Empty response content. Status code: {response.StatusCode}";
                }
            }
            else
            {
                baseResponse.Status = false;
                baseResponse.Message = "No response received from server.";
            }
        }
        catch (Exception ex)
        {
            baseResponse.Status = false;
            baseResponse.Message = $"An error occurred: {ex.Message}";
        }

        return baseResponse;
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

    // need to remove
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
