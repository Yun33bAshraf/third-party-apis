using IApply.Frontend.Models;
using IApply.Frontend.Models.Business;
using IApply.Frontend.Models.Subscription;

namespace IApply.Frontend.Services.ApiService.Subscription
{
    public interface ISubscriptionService
    {
        //Task<ListingBaseResponse<AttendenceDailySummary>?> GetAttendanceDailySummary(AttendanceGetDailyReq request);
        Task<BaseResponseListing<SubscriptionListing>?> GetSubscriptionListing(GetSubscriptionListingRequest request);
        Task<BaseResponseListing<SubscribedBusinessListingModel>?> GetSubscribedBusinessListing(GetSubscribedBusinessListingRequest request);
        Task<BaseResponse<CreateSubscriptionResponse>?> CreateUpdateSubscription(CreateSubscriptionRequest request);
        Task<BaseResponse<CreateUpdateBusinessSubscriptionResponse>?> CreateUpdateBusinessSubscription(CreateUpdateBusinessSubscriptionRequest request);

        //Task<ListingBaseResponse<GetMonthYear>?> GetMonthYearForMonthlyAttendanceFilter();
        //Task<ListingBaseResponse<Attendance>?> GetAttendance();


    }
}
