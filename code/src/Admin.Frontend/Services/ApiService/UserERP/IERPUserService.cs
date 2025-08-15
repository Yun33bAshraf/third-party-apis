using IApply.Frontend.Models.User.ERPUsers;
using IApply.Frontend.Models;

namespace IApply.Frontend.Services.ApiService.UserERP
{
    public interface IERPUserService
    {
        Task<ListingBaseResponse<UsersModel>?> GetERPUsers(ERPUsersRequest request);
        Task<BaseResponse?> UpdateUser(CreateUserRequest request);
        Task<BaseResponse?> CreateUser(CreateUserRequest request);


    }
}
