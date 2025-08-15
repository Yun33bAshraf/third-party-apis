using IApply.Frontend.Common.Constants;
using IApply.Frontend.Models;
using IApply.Frontend.Services;
using IApply.Frontend.Models.PublicHoliday;

namespace IApply.Frontend.Services.ApiService.Holiday
{
    public class HolidayService : IHolidayService
    {
        private readonly ApiService _apiService;
        private readonly AlertService _alertService;
        private readonly HttpClient _httpClient;

        public HolidayService(ApiService apiService, AlertService alertService, HttpClient httpClient)
        {
            _apiService = apiService;
            _alertService = alertService;
            _httpClient = httpClient;
        }
        public async Task<ListingBaseResponse<Holidays>?> GetHolidays(HolidayGet request)
        {
            var baseResponse = new ListingBaseResponse<Holidays>();

            // Build query parameters dynamically
            var queryParams = new List<string>();

            // Check for filters and add to the query parameters
            if (request.HolidayId > 0)
            {
                queryParams.Add($"HolidayId={request.HolidayId}");
            }

            if (!string.IsNullOrWhiteSpace(request.HolidayName))
            {
                queryParams.Add($"HolidayName={Uri.EscapeDataString(request.HolidayName)}");
            }
            if (request.HolidayDate != default)
            {
                queryParams.Add($"HolidayDate={request.HolidayDate:yyyy-MM-dd}");
            }

            if (request.IsWorkingDay)
            {
                queryParams.Add($"IsWorkingDay={request.IsWorkingDay.ToString()}");
            }

            if (request.StartDate != default)
            {
                queryParams.Add($"StartDate={request.StartDate:yyyy-MM-dd}");
            }

            // Use EndDate if it's not the default value (e.g., null or a specific date)
            if (request.EndDate != null && request.EndDate != default)
            {
                queryParams.Add($"EndDate={request.EndDate:yyyy-MM-dd}");
            }

            queryParams.Add($"PageNumber={request.PageNumber}");
            queryParams.Add($"PageSize={request.PageSize}");

            // Build the query string
            var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
            var apiUrl = ApiEndpoints.Holidays.GetHolidays + queryString;

            Console.WriteLine("API Request URL: " + apiUrl); // Debugging

            var response = await _apiService.GetAsync(apiUrl);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<ListingBaseResponse<Holidays>>();
            }

            return baseResponse;
        }
        public async Task<BaseResponse?> UpdateHoliday(HolidayCreate request)
        {
            var baseResponse = new BaseResponse();
            var response = await _apiService.PutAsync(ApiEndpoints.Holidays.UpdateHolidays, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
            }

            return baseResponse;

        }
        public async Task<BaseResponse?> CreateHoliday(HolidayCreate request)
        {
            var baseResponse = new BaseResponse();
            var response = await _apiService.PostAsync(ApiEndpoints.Holidays.CreateHolidays, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
            }

            return baseResponse;

        }
    }
}
