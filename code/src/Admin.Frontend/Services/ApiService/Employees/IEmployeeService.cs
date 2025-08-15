using static IApply.Frontend.Pages.Employee.View.EmployeeListing;
using IApply.Frontend.Models;
using IApply.Frontend.Models.Employee;

namespace IApply.Frontend.Services.ApiService.Employees
{
    public interface IEmployeeService
    {
        Task<ListingBaseResponse<Employee>?> GetEmployees(EmployeeGetRequest request);
        Task<BaseResponse?> EmployeeFileUpload(EmployeeFileUploadRequest request);
        Task<BaseResponse?> CreateBackendEmployee(EmployeeCreateRequest request);
        Task<BaseResponse?> UpdateEmployee(EmployeeCreateRequest request);
    }
}
