using OpenProfileAPI.Frontend.Models;
using OpenProfileAPI.Frontend.Models.AuthPolicies.GetAuthPolicies;
using OpenProfileAPI.Frontend.Models.AuthPolicies.UpdateAuthPolicy;
using OpenProfileAPI.Frontend.Models.Rights;
using OpenProfileAPI.Frontend.Models.Rights.GetRights;
using OpenProfileAPI.Frontend.Models.Roles;
using OpenProfileAPI.Frontend.Models.Roles.GetRoles;
using OpenProfileAPI.Frontend.Models.System.DeviceRegister;
using OpenProfileAPI.Frontend.Models.System.DeviceSetting;

namespace OpenProfileAPI.Frontend.Services.ApiService.System;

public interface ISystemService
{
    Task<BaseResponseListing<GetAuthPoliciesResponse>?> AuthPolicyGet(int authpolicyId, int userTypeId, int pageNumber, int pageSize);
    Task<BaseResponse<AuthPolicyUpdateResponse>?> AuthPolicyUpdate(UpdateAuthPolicyRequest request);
    Task<GetRightsResponse?> GetSystemRights();
    Task<BaseResponse?> DeviceRegister(DeviceRegisterRequest request);
    Task<GetFileResponse?> GetFile(Guid FileId);
    Task<DeviceSetting> GetDeviceSetting();
    Task<BaseResponse?> AddUpdateDeviceSettings(DeviceSetting request);
    Task<ListingBaseResponse<RoleModel?>?> GetRoles(string? Name, DateTime? startDate, DateTime? endDate, int pgNo, int pgSize);
    Task<ListingBaseResponse<RightsGet>?> GetRights();
    Task<BaseResponse?> RolesCreate(RolesCreateUpdateRequest request);
    Task<BaseResponse?> RolesUpdate(RolesCreateUpdateRequest request);


}
