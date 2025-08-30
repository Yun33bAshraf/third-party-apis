using OpenProfileAPI.Frontend.Common.Constants;
using OpenProfileAPI.Frontend.Models;
using OpenProfileAPI.Frontend.Models.Rights;
using OpenProfileAPI.Frontend.Models.Rights.GetRights;
using OpenProfileAPI.Frontend.Models.Roles;
using OpenProfileAPI.Frontend.Models.Roles.GetRoles;
using OpenProfileAPI.Frontend.Services;

namespace OpenProfileAPI.Frontend.Services.ApiService.Role;

public class RoleService : IRoleService
{
    private readonly ApiService _apiService;
    private readonly AlertService _alertService;

    public RoleService(ApiService apiService, AlertService alertService)
    {
        _apiService = apiService;
        _alertService = alertService;
    }

    public async Task<GetRolesResponse?> GetRoles()
    {
        var baseResponse = new GetRolesResponse();
        var response = await _apiService.GetAsync(ApiEndpoints.Role.GetRoles);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<GetRolesResponse>();
        }
        return baseResponse;
    }
    public async Task<GetRolesResponse?> GetRoleById(Guid RoleId)
    {
        var baseResponse = new GetRolesResponse();
        string endpoint = ApiEndpoints.Role.GetRoleById.Replace("${roleId}", RoleId.ToString());
        var response = await _apiService.GetAsync(endpoint);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<GetRolesResponse>();
        }
        return baseResponse;
    }

    public async Task<BaseResponse?> AddRole(RoleRequest request)
    {
        var baseResponse = new BaseResponse();
        var response = await _apiService.PostAsync(ApiEndpoints.Role.AddRole, request);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
        }
        return baseResponse;
    }

    public async Task<BaseResponse?> UpdateRole(RoleRequest request)
    {
        var baseResponse = new BaseResponse();
        var response = await _apiService.PutAsync(ApiEndpoints.Role.UpdateRole, request);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
        }
        return baseResponse;
    }

    public async Task<BaseResponse?> DeleteRole(Guid RoleId)
    {
        var baseResponse = new BaseResponse();
        string endpoint = ApiEndpoints.Role.DeleteRole.Replace("${roleId}", RoleId.ToString());
        var response = await _apiService.DeleteAsyncNew(endpoint);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
        }
        return baseResponse;
    }

    public async Task<GetRightsResponse?> GetRoleRights(Guid RoleId)
    {
        var baseResponse = new GetRightsResponse();
        string endpoint = ApiEndpoints.Role.GetRoleRights.Replace("${roleId}", RoleId.ToString());
        var response = await _apiService.GetAsync(endpoint);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<GetRightsResponse>();
        }
        return baseResponse;
    }

    public async Task<BaseResponse?> CreateRoleRights(RightsRequest request, Guid RoleId)
    {
        var baseResponse = new BaseResponse();
        string endpoint = ApiEndpoints.Role.CreateRoleRights.Replace("${roleId}", RoleId.ToString());
        var response = await _apiService.PostAsync(endpoint, request);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
        }
        return baseResponse;
    }

    public async Task<BaseResponse?> DeleteRoleRights(RightsRequest request, Guid RoleId)
    {
        var baseResponse = new BaseResponse();
        string endpoint = ApiEndpoints.Role.DeleteRoleRights.Replace("${roleId}", RoleId.ToString());
        var response = await _apiService.DeleteAsJsonAsync(endpoint, request);

        if (response.StatusCode == 200)
        {
            baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
        }
        return baseResponse;
    }
}
