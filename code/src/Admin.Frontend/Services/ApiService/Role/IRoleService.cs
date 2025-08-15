using IApply.Frontend.Models;
using IApply.Frontend.Models.Rights;
using IApply.Frontend.Models.Rights.GetRights;
using IApply.Frontend.Models.Roles;
using IApply.Frontend.Models.Roles.GetRoles;

namespace IApply.Frontend.Services.ApiService.Role;

public interface IRoleService
{
    Task<GetRolesResponse?> GetRoles();
    Task<GetRolesResponse?> GetRoleById(Guid RoleId);
    Task<BaseResponse?> AddRole(RoleRequest request);
    Task<BaseResponse?> UpdateRole(RoleRequest request);
    Task<BaseResponse?> DeleteRole(Guid RoleId);
    Task<GetRightsResponse?> GetRoleRights(Guid RoleId);
    Task<BaseResponse?> CreateRoleRights(RightsRequest request, Guid RoleId);
    Task<BaseResponse?> DeleteRoleRights(RightsRequest request, Guid RoleId);
}