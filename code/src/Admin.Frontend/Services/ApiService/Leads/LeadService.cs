using IApply.Frontend.Common.Constants;
using IApply.Frontend.Models;
using IApply.Frontend.Services;
using IApply.Frontend.Models.Lead;

namespace IApply.Frontend.Services.ApiService.Leads
{
    public class LeadService : ILeadService
    {
        private readonly ApiService _apiService;
        private readonly AlertService _alertService;
        private readonly HttpClient _httpClient;

        public LeadService(ApiService apiService, AlertService alertService, HttpClient httpClient)
        {
            _apiService = apiService;
            _alertService = alertService;
            _httpClient = httpClient;
        }
        public async Task<ListingBaseResponse<Lead>?> GetLead(LeadGetRequest request)
        {
            var baseResponse = new ListingBaseResponse<Lead>();

            // Build query parameters dynamically
            var queryParams = new List<string>();

            // Add LeadId filter
            if (request.LeadId > 0)
            {
                queryParams.Add($"LeadId={request.LeadId}");
            }

            // Add FullName filter
            if (!string.IsNullOrWhiteSpace(request.FullName))
            {
                queryParams.Add($"FullName={Uri.EscapeDataString(request.FullName)}");
            }

            // Add CompanyName filter
            if (!string.IsNullOrWhiteSpace(request.CompanyName))
            {
                queryParams.Add($"CompanyName={Uri.EscapeDataString(request.CompanyName)}");
            }

            // Add LeadSourceId filter
            if (request.LeadSourceId > 0)
            {
                queryParams.Add($"LeadSourceId={request.LeadSourceId}");
            }

            // Add LeadStatusId filter
            if (request.LeadStatusId > 0)
            {
                queryParams.Add($"LeadStatusId={request.LeadStatusId}");
            }

            // Add IndustryId filter
            if (request.IndustryId > 0)
            {
                queryParams.Add($"IndustryId={request.IndustryId}");
            }

            // Add pagination filters
            queryParams.Add($"PageNumber={request.PageNumber}");
            queryParams.Add($"PageSize={request.PageSize}");

            // Build the query string
            var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;

            // Construct the API URL
            var apiUrl = ApiEndpoints.Lead.GetLeads + queryString;

            Console.WriteLine("API Request URL: " + apiUrl); // Debugging

            // Make the API request
            var response = await _apiService.GetAsync(apiUrl);

            // Check if the response is successful
            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<ListingBaseResponse<Lead>>();
            }

            return baseResponse;
        }
        public async Task<BaseResponse?> UpdateLead(LeadCreateRequest request)
        {
            var baseResponse = new BaseResponse();
            var response = await _apiService.PutAsync(ApiEndpoints.Lead.LeadUpdate, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
            }

            return baseResponse;

        }
        public async Task<BaseResponse?> CreateLead(LeadCreateRequest request)
        {
            var baseResponse = new BaseResponse();
            var response = await _apiService.PostAsync(ApiEndpoints.Lead.LeadCreate, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
            }

            return baseResponse;

        }

    }
}
