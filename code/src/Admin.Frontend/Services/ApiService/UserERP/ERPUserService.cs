using IApply.Frontend.Models.User.ERPUsers;
using IApply.Frontend.Common.Constants;
using IApply.Frontend.Models;
using IApply.Frontend.Services;

namespace IApply.Frontend.Services.ApiService.UserERP
{
    public class ERPUserService : IERPUserService
    {
        private readonly ApiService _apiService;
        private readonly AlertService _alertService;
        private readonly HttpClient _httpClient;

        public ERPUserService(ApiService apiService, AlertService alertService, HttpClient httpClient)
        {
            _apiService = apiService;
            _alertService = alertService;
            _httpClient = httpClient;
        }
        public async Task<ListingBaseResponse<UsersModel>?> GetERPUsers(ERPUsersRequest request)
        {
            var baseResponse = new ListingBaseResponse<UsersModel>();

            // Build query parameters dynamically
            var queryParams = new List<string>();

            if (request.UserId > 0)
            {
                queryParams.Add($"UserId={request.UserId}");
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                queryParams.Add($"Name={Uri.EscapeDataString(request.Name)}");
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
            var apiUrl = ApiEndpoints.ERPUsers.GetERPUsers + queryString;

            var response = await _apiService.GetAsync(apiUrl);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<ListingBaseResponse<UsersModel>>();
            }
            return baseResponse;
        }
        public async Task<BaseResponse?> UpdateUser(CreateUserRequest request)
        {
            var baseResponse = new BaseResponse();
            var response = await _apiService.PutAsync(ApiEndpoints.User.UpdateUser, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
            }

            return baseResponse;

        }
        public async Task<BaseResponse?> CreateUser(CreateUserRequest request)
        {
            var baseResponse = new BaseResponse();
            var response = await _apiService.PostAsync(ApiEndpoints.Auth.CreateBackendUser, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
            }

            return baseResponse;

        }
    }
}
