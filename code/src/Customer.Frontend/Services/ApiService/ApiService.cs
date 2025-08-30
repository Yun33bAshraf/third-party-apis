using Blazored.LocalStorage;
using IApply.Frontend.Models.Auth.Login;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Security.Claims;
using OpenProfileAPI.Frontend.Models;
using OpenProfileAPI.Frontend.Common.Constants;
using OpenProfileAPI.Frontend.Services;
using OpenProfileAPI.Frontend.Models.Auth.Login;

namespace OpenProfileAPI.Frontend.Services.ApiService;

public class ApiService(HttpClient httpClient, string baseApiUrl, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider, AlertService alertService, NavigationManager navigationManager)
{
    private readonly HttpClient _httpClient = httpClient;
    public readonly string _baseApiUrl = baseApiUrl;
    private readonly ILocalStorageService _localStorage = localStorage;
    private readonly AuthenticationStateProvider _authStateProvider = authStateProvider;
    private readonly AlertService _alertService = alertService;
    private readonly NavigationManager _navigationManager = navigationManager;


    public async Task<string> GetToken(bool refresh = false)
    {
        var result = string.Empty;
        try
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var userState = authState.User;
            if (userState.Identity.IsAuthenticated)
            {
                if (refresh)
                {
                    result = userState.FindFirst(ClaimTypes.AuthenticationInstant)?.Value;
                }
                else
                {
                    result = userState.FindFirst(ClaimTypes.Thumbprint)?.Value;
                }
            }
            //var userSession = await _localStorage.ReadItemEncryptedAsync<LoginResponse>("UserSession");
            //if (userSession != null)
            //{
            //    result = userSession.AccessToken;
            //}
        }
        catch
        {

        }
        return result;
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest request)
    {
        await AddAuthorizationHeaderAsync();
        var response = await _httpClient.PostAsJsonAsync(_baseApiUrl + endpoint, request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    // GET method
    public async Task<TResponse?> GetAsync<TResponse>(string endpoint)
    {
        await AddAuthorizationHeaderAsync();
        var response = await _httpClient.GetAsync(_baseApiUrl + endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    // PUT method
    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest request)
    {
        await AddAuthorizationHeaderAsync();
        var response = await _httpClient.PutAsJsonAsync(_baseApiUrl + endpoint, request);
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    // DELETE method
    public async Task<(bool? IsSuccess, bool? Data, string? ErrorMessage)> DeleteAsync(string endpoint)
    {
        await AddAuthorizationHeaderAsync();
        var response = await _httpClient.DeleteAsync(_baseApiUrl + endpoint);
        return (response.IsSuccessStatusCode, response.IsSuccessStatusCode, response.IsSuccessStatusCode ? null : "Failed to delete the resource.");
    }

    // DELETE WITH JSON method
    public async Task<TResponse?> DeleteWithJsonAsync<TRequest, TResponse>(string endpoint, TRequest request)
    {
        await AddAuthorizationHeaderAsync();
        var requestMessage = new HttpRequestMessage(HttpMethod.Delete, _baseApiUrl + endpoint)
        {
            Content = JsonContent.Create(request)
        };
        var response = await _httpClient.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task<GenericResponse> PostAsync<TRequest>(string endpoint, TRequest request)
    {
        return await SendRequestAsync(() => _httpClient.PostAsJsonAsync(_baseApiUrl + endpoint, request));
    }

    public async Task<GenericResponse> PostMultipartAsync(string endpoint, MultipartFormDataContent content)
    {
        return await SendRequestAsync(() => _httpClient.PostAsync(_baseApiUrl + endpoint, content));
    }

    public async Task<GenericResponse> GetAsync(string endpoint)
    {
        return await SendRequestAsync(() => _httpClient.GetAsync(_baseApiUrl + endpoint));
    }

    public async Task<GenericResponse> PutAsync<TRequest>(string endpoint, TRequest request)
    {
        return await SendRequestAsync(() => _httpClient.PutAsJsonAsync(_baseApiUrl + endpoint, request));
    }

    public async Task<GenericResponse> PutMultipartAsync(string endpoint, MultipartFormDataContent content)
    {
        return await SendRequestAsync(() => _httpClient.PutAsync(_baseApiUrl + endpoint, content));
    }

    public async Task<GenericResponse> DeleteAsJsonAsync<TRequest>(string endpoint, TRequest request)
    {
        return await SendRequestAsync(() =>
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, _baseApiUrl + endpoint)
            {
                Content = JsonContent.Create(request)
            };
            return _httpClient.SendAsync(requestMessage);
        });
    }

    public async Task<GenericResponse> DeleteAsyncNew(string endpoint)
    {
        return await SendRequestAsync(() => _httpClient.DeleteAsync(_baseApiUrl + endpoint));
    }

    private async Task<GenericResponse> SendRequestAsync(Func<Task<HttpResponseMessage>> httpRequestFunc)
    {
        try
        {

            await AddAuthorizationHeaderAsync();

            var response = await httpRequestFunc();
            var genericResponse = await ProcessResponse(response);

            if (genericResponse.StatusCode != 401)
            {
                return genericResponse;
            }

            if (await GetRefreshToken())
            {
                await AddAuthorizationHeaderAsync();
                response = await httpRequestFunc();
                genericResponse = await ProcessResponse(response);

                if (genericResponse.StatusCode != 401)
                {
                    return genericResponse;
                }
            }

            _alertService.ShowError("Your session has expired. Please log in again to continue.");
            _navigationManager.NavigateTo("logout", true);

            return genericResponse;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    #region PRIVATE METHODS
    private async Task AddAuthorizationHeaderAsync(bool refresh = false)
    {
        try
        {
            var token = await GetToken(refresh);
            if (!string.IsNullOrEmpty(token))
            {
                if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
                {
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                }
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public async Task AddAuthorizationHeaderAsync(HttpClient http)
    {
        var token = await GetToken();
        if (!string.IsNullOrEmpty(token) && !http.DefaultRequestHeaders.Contains("Authorization"))
        {
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
    }

    private async Task<GenericResponse> ProcessResponse(HttpResponseMessage response)
    {
        var genericResponse = new GenericResponse();

        genericResponse.StatusCode = (int)response.StatusCode;

        if (response.StatusCode == HttpStatusCode.OK)
        {
            genericResponse.Response = response.Content;
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.Forbidden)
        {
            var jsonResponse = await response.Content.ReadFromJsonAsync<GenericResponse>();
            genericResponse.Errors = jsonResponse.Errors;
            var ErrorMessage = string.Empty;
            if (jsonResponse.Errors != null)
            {
                ErrorMessage = GetErrorMessage(jsonResponse.Errors);
            }
            if (jsonResponse.ExtraInformation != null)
            {
                ErrorMessage = jsonResponse.ExtraInformation;
            }
            _alertService.ShowError(ErrorMessage);
            throw new Exception(ErrorMessage);
        }
        //else if (response.StatusCode == HttpStatusCode.Forbidden)
        //{
        //    _alertService.ShowError("GetUsersProfile Has No Access");
        //    throw new Exception("GetUsersProfile Has No Access");
        //}
        else if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            //_alertService.ShowError("Your session has expired. Please log in again to continue.");
            //_navigationManager.NavigateTo("logout", true);
        }
        else if (response.StatusCode == HttpStatusCode.NotFound)
        {
            try
            {
                var baseResponse = await response.Content.ReadFromJsonAsync<BaseResponse>();
                if (baseResponse.ErrorCode == 601)
                {
                    throw new Exception(ErrorService.GetErrorMessage(baseResponse.ErrorCode));
                }
                else
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        else
        {
            throw new Exception(response.StatusCode.ToString());
        }

        return genericResponse;

    }

    private string GetErrorMessage(Dictionary<string, string[]> Errors)
    {
        var allErrors = Errors.Select(kv => $"{kv.Key}: {string.Join("\n ", kv.Value)}").ToList();
        return string.Join("\n ", allErrors);
    }

    private async Task<bool> GetRefreshToken()
    {
        try
        {
            var request = new RefreshTokenRequest();
            await AddAuthorizationHeaderAsync(true);
            var response = await _httpClient.PostAsJsonAsync(_baseApiUrl + ApiEndpoints.Auth.RefreshToken, request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                var authState = await _authStateProvider.GetAuthenticationStateAsync();
                var userState = authState.User;

                if (userState.Identity is ClaimsIdentity identity && loginResponse != null)
                {
                    // Update Thumbprint claim
                    var thumbprintClaim = identity.FindFirst(ClaimTypes.Thumbprint);
                    if (thumbprintClaim != null)
                    {
                        identity.RemoveClaim(thumbprintClaim);
                    }
                    identity.AddClaim(new Claim(ClaimTypes.Thumbprint, loginResponse?.Data?.AccessToken));

                    // Update Refresh Token claim
                    var refreshTokenClaim = identity.FindFirst(ClaimTypes.AuthenticationInstant);
                    if (refreshTokenClaim != null)
                    {
                        identity.RemoveClaim(refreshTokenClaim);
                    }
                    identity.AddClaim(new Claim(ClaimTypes.AuthenticationInstant, loginResponse?.Data?.RefreshToken));

                    return true;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    #endregion PRIVATE METHODS
}
