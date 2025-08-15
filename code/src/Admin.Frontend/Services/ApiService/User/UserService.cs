using IApply.Frontend.Common.Constants;
using IApply.Frontend.Models;
using IApply.Frontend.Models.Assets;
using IApply.Frontend.Models.Rights;
using IApply.Frontend.Models.Rights.GetRights;
using IApply.Frontend.Models.Roles.GetRoles;
using IApply.Frontend.Models.User.CreateUserRole;
using IApply.Frontend.Models.User.DeleteUserRole;
using IApply.Frontend.Models.User.UpdateFileStatus;
using IApply.Frontend.Models.User.UpdateUserStatus;
using IApply.Frontend.Models.User.UserProfile;
using IApply.Frontend.Models.User.UserProfileUpdate;

namespace IApply.Frontend.Services.ApiService.User;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly ApiService _apiService;
    private readonly AlertService _alertService;
    public UserService(HttpClient httpClient, ApiService apiService, AlertService alertService)
    {
        _httpClient = httpClient;
        _apiService = apiService;
        _alertService = alertService;
    }

    public async Task<BaseResponseListing<UserProfileGetResponse>?> GetUserProfileAsync(int userId, string? username, int pageNumber, int pageSize)
    {
        try
        {
            var queryParams = new List<string>();

            if (userId > 0)
                queryParams.Add($"UserId={userId}");

            if (!string.IsNullOrWhiteSpace(username))
                queryParams.Add($"Name={Uri.EscapeDataString(username)}");

            queryParams.Add($"PageNumber={pageNumber}");
            queryParams.Add($"PageSize={pageSize}");

            var queryString = queryParams.Count != 0 ? "?" + string.Join("&", queryParams) : string.Empty;
            var requestUrl = $"{ApiEndpoints.User.UserGet}{queryString}";

            Console.WriteLine($"API Request URL: {requestUrl}");

            var response = await _apiService.GetAsync(requestUrl);

            if (response.StatusCode == 200 && response.Response != null)
            {
                return await response.Response.ReadFromJsonAsync<BaseResponseListing<UserProfileGetResponse>>();
            }

            return new BaseResponseListing<UserProfileGetResponse>
            {
                Status = false,
                Message = $"API call failed with status code {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            return new BaseResponseListing<UserProfileGetResponse>
            {
                Status = false,
                Message = $"An error occurred: {ex.Message}"
            };
        }
    }

    public async Task<BaseResponse<UserProfileUpdateResponse>?> UserProfileUpdate(UserProfileUpdateRequest request)
    {
        try
        {
            var baseResponse = new BaseResponse<UserProfileUpdateResponse>();

            var response = await _apiService.PutAsync(ApiEndpoints.User.UserUpdate, request);

            if (response.Response != null && response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse<UserProfileUpdateResponse>>();
            }

            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<UserProfileUpdateResponse>
            {
                Status = false,
                Message = $"An error occurred: {ex.Message}"
            };
        }
    }

    // Remove the following methods if not needed in the future

    #region METHODS TO REMOVE IN FUTURE


    //public async Task<GetUsersResponse?> GetUsers(GetUsersRequest request)
    //{
    //    var baseResponse = new GetUsersResponse();
    //    var response = await _apiService.PostAsync(ApiEndpoints.User.GetUsers, request);

    //    if (response.StatusCode == 200)
    //    {
    //        baseResponse = await response.Response.ReadFromJsonAsync<GetUsersResponse>();
    //    }
    //    return baseResponse;
    //}
    //public async Task<ProfileResponse?> GetProfile()
    //{
    //    var baseResponse = new ProfileResponse();
    //    var response = await _apiService.GetAsync(ApiEndpoints.User.GetProfile);

    //    if (response.StatusCode == 200)
    //    {
    //        baseResponse = await response.Response.ReadFromJsonAsync<ProfileResponse>();
    //    }
    //    return baseResponse;
    //}

    //public async Task<GetUsersResponse?> GetUserById(string UserId)
    //{
    //    var baseResponse = new GetUsersResponse();
    //    var endpoint = ApiEndpoints.User.GetUserById.Replace("${userId}", UserId);
    //    var response = await _apiService.GetAsync(endpoint);

    //    if (response.StatusCode == 200)
    //    {
    //        baseResponse = await response.Response.ReadFromJsonAsync<GetUsersResponse>();
    //    }
    //    return baseResponse;
    //}

    public async Task<GetRolesResponse?> GetUserRoles(Guid UserId)
    {
        var baseResponse = new GetRolesResponse();
        string endpoint = ApiEndpoints.User.GetUserRoles.Replace("${userId}", UserId.ToString());
        var response = await _apiService.GetAsync(endpoint);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<GetRolesResponse>();
        }
        return baseResponse;
    }

    public async Task<BaseResponse?> CreateUserRole(CreateUserRolesRequest request, Guid UserId)
    {
        var baseResponse = new BaseResponse();
        string endpoint = ApiEndpoints.User.CreateUserRoles.Replace("${userId}", UserId.ToString());
        var response = await _apiService.PostAsync(endpoint, request);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
        }
        return baseResponse;
    }

    public async Task<BaseResponse?> DeleteUserRole(DeleteUserRolesRequest request, Guid UserId)
    {
        var baseResponse = new BaseResponse();
        string endpoint = ApiEndpoints.User.DeleteUserRoles.Replace("${userId}", UserId.ToString());
        var response = await _apiService.DeleteAsJsonAsync(endpoint, request);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
        }
        return baseResponse;
    }

    public async Task<GetRightsResponse?> GetUserRights(Guid UserId)
    {
        var baseResponse = new GetRightsResponse();
        string endpoint = ApiEndpoints.User.GetUserRights.Replace("${userId}", UserId.ToString());
        var response = await _apiService.GetAsync(endpoint);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<GetRightsResponse>();
        }
        return baseResponse;
    }

    public async Task<BaseResponse?> CreateUserRights(RightsRequest request, Guid UserId)
    {
        var baseResponse = new BaseResponse();
        string endpoint = ApiEndpoints.User.CreateUserRights.Replace("${userId}", UserId.ToString());
        var response = await _apiService.PostAsync(endpoint, request);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
        }
        return baseResponse;
    }

    public async Task<BaseResponse?> DeleteUserRights(RightsRequest request, Guid UserId)
    {
        var baseResponse = new BaseResponse();
        string endpoint = ApiEndpoints.User.DeleteUserRights.Replace("${userId}", UserId.ToString());
        var response = await _apiService.DeleteAsJsonAsync(endpoint, request);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
        }
        return baseResponse;
    }

    public async Task<BaseResponse?> UpdateUserStatus(UpdateUserStatusRequest request, string UserId)
    {
        var baseResponse = new BaseResponse();
        string endpoint = ApiEndpoints.User.UpdateUserStatus.Replace("${userId}", UserId);
        var response = await _apiService.PostAsync(endpoint, request);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
        }
        return baseResponse;
    }

    public async Task<BaseResponse?> UpdateFileStatus(UpdateFileStatusRequest request, string UserId, string FileId)
    {
        var baseResponse = new BaseResponse();
        string endpoint = ApiEndpoints.User.UpdateFileStatus.Replace("${userId}", UserId).Replace("${fileId}", FileId);
        var response = await _apiService.PostAsync(endpoint, request);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
        }
        return baseResponse;
    }

    #endregion METHODS TO REMOVE IN FUTURE
}
