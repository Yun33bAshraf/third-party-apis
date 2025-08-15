using static IApply.Frontend.Pages.Employee.View.EmployeeListing;
using System.Net.Http.Headers;
using IApply.Frontend.Common.Constants;
using IApply.Frontend.Models;
using IApply.Frontend.Services;
using IApply.Frontend.Models.Employee;

namespace IApply.Frontend.Services.ApiService.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApiService _apiService;
        private readonly AlertService _alertService;
        private readonly HttpClient _httpClient;

        public EmployeeService(ApiService apiService, AlertService alertService, HttpClient httpClient)
        {
            _apiService = apiService;
            _alertService = alertService;
            _httpClient = httpClient;
        }
        public async Task<ListingBaseResponse<Employee>?> GetEmployees(EmployeeGetRequest request)
        {
            var baseResponse = new ListingBaseResponse<Employee>();

            // Build query parameters dynamically
            var queryParams = new List<string>();

            if (request.EmployeeId > 0)
            {
                queryParams.Add($"EmployeeId={request.EmployeeId}");
            }

            if (!string.IsNullOrWhiteSpace(request.EmployeeName))
            {
                queryParams.Add($"EmployeeName={Uri.EscapeDataString(request.EmployeeName)}");
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                queryParams.Add($"Email={Uri.EscapeDataString(request.Email)}");
            }

            if (!string.IsNullOrWhiteSpace(request.Gender))  // Add Gender filter
            {
                queryParams.Add($"Gender={Uri.EscapeDataString(request.Gender)}");
            }

            if (request.City > 0)  // Add City filter (send as integer)
            {
                queryParams.Add($"City={request.City}");
            }

            if (request.StartDate != default)
            {
                queryParams.Add($"StartDate={request.StartDate:yyyy-MM-dd}");
            }

            var endDate = request.EndDate == default ? DateTime.UtcNow : request.EndDate;
            queryParams.Add($"EndDate={endDate:yyyy-MM-dd}");

            queryParams.Add($"PageNumber={request.PageNumber}");
            queryParams.Add($"PageSize={request.PageSize}");

            var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
            var apiUrl = ApiEndpoints.Employees.GetEmployees + queryString;

            //Console.WriteLine("API Request URL: " + apiUrl);

            var response = await _apiService.GetAsync(apiUrl);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<ListingBaseResponse<Employee>>();
            }

            return baseResponse;
        }
        public async Task<BaseResponse?> EmployeeFileUpload(EmployeeFileUploadRequest request)
        {
            var baseResponse = new BaseResponse();

            using var content = new MultipartFormDataContent();

            // Add EmployeeId as a form field
            content.Add(new StringContent(request.EmployeeId.ToString()), "EmployeeId");

            if (request.EmployeeFile != null)
            {
                int count = 0;
                foreach (var fileModel in request.EmployeeFile)
                {
                    // Add CategoryId and SubCategoryId as form fields
                    content.Add(new StringContent(fileModel.CategoryId.ToString()), $"EmployeeFile[{count}].CategoryId");
                    if (fileModel.SubCategoryId.HasValue)
                    {
                        content.Add(new StringContent(fileModel.SubCategoryId.Value.ToString()), $"EmployeeFile[{count}].SubCategoryId");
                    }

                    // Add files
                    var file = fileModel.Files;
                    var fileContent = new StreamContent(file.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                    content.Add(fileContent, $"EmployeeFile[{count}].Files", file.Name);
                    count++;
                }
            }

            var response = await _apiService.PostMultipartAsync(ApiEndpoints.Employees.EmployeeFileUpload, content);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
            }

            return baseResponse;
        }
        public async Task<BaseResponse?> CreateBackendEmployee(EmployeeCreateRequest request)
        {
            var baseResponse = new BaseResponse();
            var response = await _apiService.PostAsync(ApiEndpoints.Employees.CreateEmployees, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
            }

            return baseResponse;

        }
        public async Task<BaseResponse?> UpdateEmployee(EmployeeCreateRequest request)
        {
            var baseResponse = new BaseResponse();
            var response = await _apiService.PutAsync(ApiEndpoints.Employees.UpdateEmployees, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
            }

            return baseResponse;

        }
    }
}
