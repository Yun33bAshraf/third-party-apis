using IApply.Frontend.Models;
using IApply.Frontend.Models.Business;
using IApply.Frontend.Models.Subscription;

namespace IApply.Frontend.Services.ApiService.Business
{
    public interface IBusinessService
    {
        //Task<ListingBaseResponse<AttendenceDailySummary>?> GetAttendanceDailySummary(AttendanceGetDailyReq request);
        Task<BaseResponseListing<BusinessListing>?> GetBusinessListing(GetBusinessListingRequest request);
        Task<BaseResponseListing<GetAllCurrency>?> GetAllCurrency(int currencyId); 
        Task<BaseResponse<CreateBusinessResponse>?> CreateUpdateBusiness(CreateBusinessRequest request);

        //Task<ListingBaseResponse<GetMonthYear>?> GetMonthYearForMonthlyAttendanceFilter();
        //Task<ListingBaseResponse<Attendance>?> GetAttendance();


    }
}
