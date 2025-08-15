using IApply.Frontend.Models;
using IApply.Frontend.Models.Attendances;

namespace IApply.Frontend.Services.ApiService.AttendenceMonthly
{
    public interface IAttendenceService
    {
        Task<ListingBaseResponse<AttendenceDailySummary>?> GetAttendanceDailySummary(AttendanceGetDailyReq request);
        Task<ListingBaseResponse<AttendenceMonthlySummary>?> GetAttendanceSummary(DateTime? selectMonthYear);
        Task<ListingBaseResponse<GetMonthYear>?> GetMonthYearForMonthlyAttendanceFilter();
        Task<ListingBaseResponse<Attendance>?> GetAttendance();


    }
}
