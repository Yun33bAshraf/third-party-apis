using IApply.Frontend.Models;
using IApply.Frontend.Models.AuthPolicies.GetAuthPolicies;
using IApply.Frontend.Models.AuthPolicies.UpdateAuthPolicy;
using IApply.Frontend.Models.Rights;
using IApply.Frontend.Models.Rights.GetRights;
using IApply.Frontend.Models.Roles;
using IApply.Frontend.Models.Roles.GetRoles;
using IApply.Frontend.Models.System.DeviceRegister;
using IApply.Frontend.Models.System.DeviceSetting;

namespace IApply.Frontend.Services.ApiService.System;

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
