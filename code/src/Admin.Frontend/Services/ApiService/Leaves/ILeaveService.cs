using IApply.Frontend.Models;
using IApply.Frontend.Models.EmployeeLeave;

namespace IApply.Frontend.Services.ApiService.Leaves
{
    public interface ILeaveService
    {
        Task<ListingBaseResponse<EmployeeLeave>?> GetEmployeesLeave(EmpLeaveGetRequest request);
        Task<BaseResponse?> EmpLeaveCreate(EmpLeaveCreateUpdateRequest request);
        Task<BaseResponse?> EmpLeaveUpdate(EmpLeaveCreateUpdateRequest request);
    }
}
