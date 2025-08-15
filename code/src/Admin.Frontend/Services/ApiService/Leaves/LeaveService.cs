using IApply.Frontend.Common.Constants;
using IApply.Frontend.Models;
using IApply.Frontend.Services;
using IApply.Frontend.Models.EmployeeLeave;

namespace IApply.Frontend.Services.ApiService.Leaves
{
    public class LeaveService : ILeaveService
    {
        private readonly ApiService _apiService;
        private readonly AlertService _alertService;
        private readonly HttpClient _httpClient;

        public LeaveService(ApiService apiService, AlertService alertService, HttpClient httpClient)
        {
            _apiService = apiService;
            _alertService = alertService;
            _httpClient = httpClient;
        }
        public async Task<ListingBaseResponse<EmployeeLeave>?> GetEmployeesLeave(EmpLeaveGetRequest request)
        {
            var baseResponse = new ListingBaseResponse<EmployeeLeave>();
            var queryParams = new List<string>();

            // Add filter parameters only if they are provided

            if (request.LeaveId > 0)
            {
                queryParams.Add($"LeaveId={request.LeaveId}");
            }

            if (request.EmployeeId > 0)
            {
                queryParams.Add($"EmployeeId={request.EmployeeId}");
            }

            if (!string.IsNullOrWhiteSpace(request.EmployeeName))
            {
                queryParams.Add($"EmployeeName={Uri.EscapeDataString(request.EmployeeName)}");
            }

            if (request.LeaveType > 0)
            {
                queryParams.Add($"LeaveType={request.LeaveType}");
            }

            // LeaveStatus filter: Check if it's not null
            if (request.LeaveStatus.HasValue)
            {
                queryParams.Add($"LeaveStatus={request.LeaveStatus}");
            }

            // Handle StartDate filter: Only add it if it's not null
            if (request.StartDate.HasValue)
            {
                queryParams.Add($"StartDate={request.StartDate:yyyy-MM-dd}");
            }

            if (request.EndDate.HasValue)
            {
                queryParams.Add($"StartDate={request.EndDate:yyyy-MM-dd}");
            }

            //// Handle EndDate filter: If null, default to current UTC time
            //var endDate = request.EndDate ?? DateTime.UtcNow;
            //queryParams.Add($"EndDate={endDate:yyyy-MM-dd}");

            // Pagination parameters
            queryParams.Add($"PageNumber={request.PageNumber}");
            queryParams.Add($"PageSize={request.PageSize}");

            // Build query string
            var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
            var apiUrl = ApiEndpoints.EmployeeLeave.GetEmployeeLeaves + queryString;

            Console.WriteLine("API Request URL: " + apiUrl); // Debugging

            // Call the API
            var response = await _apiService.GetAsync(apiUrl);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<ListingBaseResponse<EmployeeLeave>>();
            }

            return baseResponse;
        }
        public async Task<BaseResponse?> EmpLeaveCreate(EmpLeaveCreateUpdateRequest request)
        {
            var baseResponse = new BaseResponse();
            var response = await _apiService.PostAsync(ApiEndpoints.EmployeeLeave.EmpLeaveCreate, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
            }
            return baseResponse;
        }
        public async Task<BaseResponse?> EmpLeaveUpdate(EmpLeaveCreateUpdateRequest request)
        {
            var baseResponse = new BaseResponse();
            var response = await _apiService.PutAsync(ApiEndpoints.EmployeeLeave.EmpLeaveUpdate, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
            }
            return baseResponse;
        }
    }
}
