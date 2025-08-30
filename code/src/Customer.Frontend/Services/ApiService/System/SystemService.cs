using System.Web;
using OpenProfileAPI.Frontend.Common.Constants;
using OpenProfileAPI.Frontend.Models;
using OpenProfileAPI.Frontend.Models.AuthPolicies.GetAuthPolicies;
using OpenProfileAPI.Frontend.Models.AuthPolicies.UpdateAuthPolicy;
using OpenProfileAPI.Frontend.Models.Rights;
using OpenProfileAPI.Frontend.Models.Rights.GetRights;
using OpenProfileAPI.Frontend.Models.Roles;
using OpenProfileAPI.Frontend.Models.Roles.GetRoles;
using OpenProfileAPI.Frontend.Models.System.DeviceRegister;
using OpenProfileAPI.Frontend.Models.System.DeviceSetting;
using OpenProfileAPI.Frontend.Services;

namespace OpenProfileAPI.Frontend.Services.ApiService.System;

public class SystemService : ISystemService
{
    private readonly ApiService _apiService;
    private readonly AlertService _alertService;
    private readonly HttpClient _httpClient;

    public SystemService(ApiService apiService, AlertService alertService, HttpClient httpClient)
    {
        _apiService = apiService;
        _alertService = alertService;
        _httpClient = httpClient;
    }

    public async Task<BaseResponseListing<GetAuthPoliciesResponse>?> AuthPolicyGet(int authpolicyId, int userTypeId, int pageNumber, int pageSize)
    {
        var baseResponse = new BaseResponseListing<GetAuthPoliciesResponse>();
        var queryParams = new List<string>();

        if (authpolicyId > 0)
            queryParams.Add($"AuthPolicyId={authpolicyId}");

        if (userTypeId > 0)
            queryParams.Add($"UserTypeId={userTypeId}");

        queryParams.Add($"PageNumber={pageNumber}");
        queryParams.Add($"PageSize={pageSize}");

        var queryString = queryParams.Count != 0 ? "?" + string.Join("&", queryParams) : string.Empty;
        var requestUrl = $"{ApiEndpoints.System.GetAuthPolicies}{queryString}";
        var response = await _apiService.GetAsync(requestUrl);

        Console.WriteLine($"API Request URL: {requestUrl}");

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponseListing<GetAuthPoliciesResponse>?>();
        }
        return baseResponse;
    }

    public async Task<BaseResponse<AuthPolicyUpdateResponse>?> AuthPolicyUpdate(UpdateAuthPolicyRequest request)
    {
        var baseResponse = new BaseResponse<AuthPolicyUpdateResponse>();
        var response = await _apiService.PutAsync(ApiEndpoints.System.UpdateAuthPolicy, request);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse<AuthPolicyUpdateResponse>>();
        }
        return baseResponse;
    }

    public async Task<GetRightsResponse?> GetSystemRights()
    {
        var baseResponse = new GetRightsResponse();
        var response = await _apiService.GetAsync(ApiEndpoints.System.GetSystemRights);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<GetRightsResponse>();
        }
        return baseResponse;
    }

    public async Task<BaseResponse?> DeviceRegister(DeviceRegisterRequest request)
    {
        var baseResponse = new BaseResponse();
        var response = await _apiService.PutAsync(ApiEndpoints.System.DeviceRegister, request);
        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
        }
        return baseResponse;
    }

    public async Task<GetFileResponse?> GetFile(Guid FileId)
    {
        var baseResponse = new GetFileResponse();
        var endpoint = ApiEndpoints.System.GetFile.Replace("${fileId}", FileId.ToString());
        var response = await _apiService.GetAsync(endpoint);

        if (response.StatusCode == 200)
        {
            baseResponse.FileName = response.Response?.Headers.ContentDisposition?.FileName ?? "attachment";
            var fileContent = await response.Response.ReadAsByteArrayAsync();

            baseResponse.Base64 = Convert.ToBase64String(fileContent);
            baseResponse.ContentType = response.Response.Headers.ContentType.ToString() ?? "application/octet-stream";
        }
        return baseResponse;
    }

    public async Task<DeviceSetting> GetDeviceSetting()
    {
        var baseResponse = new DeviceSetting();
        var response = await _apiService.GetAsync(ApiEndpoints.System.GetDeviceSettings);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<DeviceSetting>();
        }
        return baseResponse;
    }

    public async Task<BaseResponse?> AddUpdateDeviceSettings(DeviceSetting request)
    {
        var baseResponse = new BaseResponse();
        var response = await _apiService.PostAsync(ApiEndpoints.System.AddUpdateDeviceSettings, request);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
        }
        return baseResponse;
    }

    public async Task<ListingBaseResponse<RoleModel?>?> GetRoles(string? name, DateTime? startDate, DateTime? endDate, int pgNo, int pgSize)
    {
        var baseResponse = new ListingBaseResponse<RoleModel>();

        var queryParams = HttpUtility.ParseQueryString(string.Empty);

        if (!string.IsNullOrWhiteSpace(name))
        {
            queryParams["Name"] = name;
        }

        if (startDate.HasValue)
        {
            queryParams["StartDate"] = startDate.Value.ToString("yyyy-MM-dd");
        }

        var finalEndDate = endDate ?? DateTime.UtcNow;
        queryParams["EndDate"] = finalEndDate.ToString("yyyy-MM-dd");

        queryParams["PageNumber"] = pgNo.ToString();
        queryParams["PageSize"] = pgSize.ToString();

        var queryString = queryParams.ToString();
        var apiUrl = ApiEndpoints.Role.RolesGet + (string.IsNullOrWhiteSpace(queryString) ? string.Empty : "?" + queryString);

        Console.WriteLine($"API Request URL: {apiUrl}"); // Debugging

        var response = await _apiService.GetAsync(apiUrl);

        if (response.StatusCode == 200 && response.Response is not null)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<ListingBaseResponse<RoleModel>>();
        }

        return baseResponse;
    }
    
    public async Task<ListingBaseResponse<RightsGet>?> GetRights()
    {
        var baseResponse = new ListingBaseResponse<RightsGet>();
        var response = await _apiService.GetAsync(ApiEndpoints.Role.RightsGet);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<ListingBaseResponse<RightsGet>>();
        }
        return baseResponse;
    }
    
    public async Task<BaseResponse?> RolesCreate(RolesCreateUpdateRequest request)
    {
        var baseResponse = new BaseResponse();
        var response = await _apiService.PostAsync(ApiEndpoints.Role.RolesCreate, request);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
        }
        return baseResponse;
    }
    
    public async Task<BaseResponse?> RolesUpdate(RolesCreateUpdateRequest request)
    {
        var baseResponse = new BaseResponse();
        var response = await _apiService.PutAsync(ApiEndpoints.Role.RolesUpdate, request);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
        }
        return baseResponse;
    }
}
