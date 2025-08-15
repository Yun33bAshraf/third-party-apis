using IApply.Frontend.Models;
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

public interface IUserService
{
    Task<BaseResponseListing<UserProfileGetResponse>?> GetUserProfileAsync(int userId, string? username, int pageNumber, int pageSize);
    Task<BaseResponse<UserProfileUpdateResponse>?> UserProfileUpdate(UserProfileUpdateRequest request);

    // Remove the following methods if not needed in the future

    #region METHODS TO REMOVE IN FUTURE

    //Task<GetUsersResponse?> GetUsers(GetUsersRequest request);
    //Task<ProfileResponse?> GetProfile();
    //Task<GetUsersResponse?> GetUserById(string UserId);
    Task<GetRolesResponse?> GetUserRoles(Guid UserId);
    Task<BaseResponse?> CreateUserRole(CreateUserRolesRequest request, Guid UserId);
    Task<BaseResponse?> DeleteUserRole(DeleteUserRolesRequest request, Guid UserId);
    Task<GetRightsResponse?> GetUserRights(Guid UserId);
    Task<BaseResponse?> CreateUserRights(RightsRequest request, Guid UserId);
    Task<BaseResponse?> DeleteUserRights(RightsRequest request, Guid UserId);
    Task<BaseResponse?> UpdateUserStatus(UpdateUserStatusRequest request, string UserId);
    Task<BaseResponse?> UpdateFileStatus(UpdateFileStatusRequest request, string UserId, string FileId);

    #endregion METHODS TO REMOVE IN FUTURE
}
