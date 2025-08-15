using IApply.Frontend.Common.Constants;
using IApply.Frontend.Models;
using IApply.Frontend.Services;
using IApply.Frontend.Models.Attendances;

namespace IApply.Frontend.Services.ApiService.AttendenceMonthly
{
    public class AttendenceService : IAttendenceService
    {
        private readonly ApiService _apiService;
        private readonly AlertService _alertService;
        private readonly HttpClient _httpClient;

        public AttendenceService(ApiService apiService, AlertService alertService, HttpClient httpClient)
        {
            _apiService = apiService;
            _alertService = alertService;
            _httpClient = httpClient;
        }
        public async Task<ListingBaseResponse<AttendenceDailySummary>?> GetAttendanceDailySummary(AttendanceGetDailyReq request)
        {
            var baseResponse = new ListingBaseResponse<AttendenceDailySummary>();
            string url = ApiEndpoints.Attendances.GetAttendanceDailySummary;
            url = $"{url}?employeeId={request.EmployeeId}&startDate={request.StartDate}&endDate={request.EndDate}";
            // Assuming you're sending the request in some form (maybe as query parameters or JSON body)
            var response = await _apiService.GetAsync(url);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<ListingBaseResponse<AttendenceDailySummary>>();
            }

            return baseResponse;
        }
        public async Task<ListingBaseResponse<AttendenceMonthlySummary>?> GetAttendanceSummary(DateTime? selectMonthYear)
        {
            var baseResponse = new ListingBaseResponse<AttendenceMonthlySummary>();

            // Default to current month and year if not provided
            int month = selectMonthYear?.Month ?? DateTime.Now.Month;
            int year = selectMonthYear?.Year ?? DateTime.Now.Year;

            // Construct the date as the first day of the selected month
            DateOnly effectiveDate = new DateOnly(year, month, 1);

            // Format the date correctly for API request
            string formattedDate = effectiveDate.ToString("yyyy-MM-dd");

            string url = $"{ApiEndpoints.Attendances.GetAttendanceSummary}?selectedDate={formattedDate}";

            var response = await _apiService.GetAsync(url);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<ListingBaseResponse<AttendenceMonthlySummary>>();
            }

            return baseResponse;
        }
        public async Task<ListingBaseResponse<GetMonthYear>?> GetMonthYearForMonthlyAttendanceFilter()
        {
            var baseResponse = new ListingBaseResponse<GetMonthYear>();

            string url = ApiEndpoints.System.GetMonthYear;

            var response = await _apiService.GetAsync(url);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<ListingBaseResponse<GetMonthYear>>();
            }

            return baseResponse;
        }
        public async Task<ListingBaseResponse<Attendance>?> GetAttendance()
        {
            var baseResponse = new ListingBaseResponse<Attendance>();
            var response = await _apiService.GetAsync(ApiEndpoints.Attendances.GetAttendances);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<ListingBaseResponse<Attendance>>();
            }
            return baseResponse;
        }
    }
}
