using IApply.Frontend.Common.Constants;
using IApply.Frontend.Models;
using IApply.Frontend.Services;
using IApply.Frontend.Models.Settings.Configurations;
using IApply.Frontend.Models.Employee;

namespace IApply.Frontend.Services.ApiService.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ApiService _apiService;
        private readonly AlertService _alertService;
        private readonly HttpClient _httpClient;

        public CategoryService(ApiService apiService, AlertService alertService, HttpClient httpClient)
        {
            _apiService = apiService;
            _alertService = alertService;
            _httpClient = httpClient;
        }
        public async Task<ListingBaseResponse<GetCategory>?> GetCategory(GetCategory request)
        {
            var baseResponse = new ListingBaseResponse<GetCategory>();

            // Build query parameters dynamically
            var queryParams = new List<string>();

            if (request.CategoryId > 0)
            {
                queryParams.Add($"CategoryId={request.CategoryId}");
            }
            if (request.EntityTypeId > 0)
            {
                queryParams.Add($"EntityTypeId={request.EntityTypeId}");
            }

            if (!string.IsNullOrWhiteSpace(request.CategoryName))
            {
                queryParams.Add($"CategoryName={Uri.EscapeDataString(request.CategoryName)}");
            }

            var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
            var apiUrl = ApiEndpoints.System.Get + queryString;

            Console.WriteLine("API Request URL: " + apiUrl); // Debugging

            var response = await _apiService.GetAsync(apiUrl);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<ListingBaseResponse<GetCategory>>();
            }

            return baseResponse;
        }
        public async Task<ListingBaseResponse<EntityGetRequest>?> GetEntity(EntityGetRequest request)
        {
            var baseResponse = new ListingBaseResponse<EntityGetRequest>();

            // Build query parameters dynamically
            var queryParams = new List<string>();

            if (request.EntityTypeId > 0)
            {
                queryParams.Add($"EntityTypeId={request.EntityTypeId}");
            }

            if (!string.IsNullOrWhiteSpace(request.EntityName))
            {
                queryParams.Add($"EntityName={Uri.EscapeDataString(request.EntityName)}");
            }

            var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
            var apiUrl = ApiEndpoints.System.EntityGet + queryString;

            Console.WriteLine("API Request URL: " + apiUrl); // Debugging

            var response = await _apiService.GetAsync(apiUrl);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<ListingBaseResponse<EntityGetRequest>>();
            }

            return baseResponse;
        }
        public async Task<ListingBaseResponse<CategoryGetResponse>?> GetCategories(CategoryGetRequest request)
        {
            try
            {
                var baseResponse = new ListingBaseResponse<CategoryGetResponse>();

                var queryParams = new List<string>();

                if (request.EntityTypeId > 0)
                    queryParams.Add($"EntityTypeId={request.EntityTypeId}");

                if (request.ParentCategoryId > 0)
                    queryParams.Add($"ParentCategoryId={request.ParentCategoryId}");

                queryParams.Add($"PageNumber={request.PageNumber}");
                queryParams.Add($"PageSize={request.PageSize}");

                var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
                var apiUrl = ApiEndpoints.System.Get + queryString;

                Console.WriteLine("API Request URL: " + apiUrl); // Debugging

                var response = await _apiService.GetAsync(apiUrl);

                if (response.StatusCode == 200 && response.Response != null)
                {
                    baseResponse = await response.Response.ReadFromJsonAsync<ListingBaseResponse<CategoryGetResponse>>();
                }

                return baseResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<BaseResponse?> CreateCategories(CategoryCreate request)
        {
            var baseResponse = new BaseResponse();
            var response = await _apiService.PostAsync(ApiEndpoints.System.CategoryCreate, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
            }

            return baseResponse;

        }
    }
}
