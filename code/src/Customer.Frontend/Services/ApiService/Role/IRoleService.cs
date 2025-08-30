using OpenProfileAPI.Frontend.Models;
using OpenProfileAPI.Frontend.Models.Rights;
using OpenProfileAPI.Frontend.Models.Rights.GetRights;
using OpenProfileAPI.Frontend.Models.Roles;
using OpenProfileAPI.Frontend.Models.Roles.GetRoles;

namespace OpenProfileAPI.Frontend.Services.ApiService.Role;

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