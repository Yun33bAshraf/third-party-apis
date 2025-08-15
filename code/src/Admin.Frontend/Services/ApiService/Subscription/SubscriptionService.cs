using IApply.Frontend.Common.Constants;
using IApply.Frontend.Models;
using IApply.Frontend.Models.Business;
using IApply.Frontend.Models.Subscription;

namespace IApply.Frontend.Services.ApiService.Subscription
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ApiService _apiService;
        private readonly AlertService _alertService;
        private readonly HttpClient _httpClient;

        public SubscriptionService(ApiService apiService, AlertService alertService, HttpClient httpClient)
        {
            _apiService = apiService;
            _alertService = alertService;
            _httpClient = httpClient;
        }
        public async Task<BaseResponseListing<SubscriptionListing>?> GetSubscriptionListing(GetSubscriptionListingRequest request)
        {
            var baseResponse = new BaseResponseListing<SubscriptionListing>();


            string url = $"{ApiEndpoints.Subscription.List}";

            var response = await _apiService.PostAsync(url, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponseListing<SubscriptionListing>>();
            }

            return baseResponse;
        }        
        public async Task<BaseResponseListing<SubscribedBusinessListingModel>?> GetSubscribedBusinessListing(GetSubscribedBusinessListingRequest request)
        {
            var baseResponse = new BaseResponseListing<SubscribedBusinessListingModel>();


            string url = $"{ApiEndpoints.Subscription.GetSubscribedBusiness}";

            var response = await _apiService.PostAsync(url, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponseListing<SubscribedBusinessListingModel>>();
            }

            return baseResponse;
        }
        public async Task<BaseResponse<CreateSubscriptionResponse>?> CreateUpdateSubscription(CreateSubscriptionRequest request)
        {
            var baseResponse = new BaseResponse<CreateSubscriptionResponse>();


            string url = $"{ApiEndpoints.Subscription.CreateUpdateSubscription}";

            var response = await _apiService.PostAsync(url, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse<CreateSubscriptionResponse>>();
            }

            return baseResponse;
        }        
        public async Task<BaseResponse<CreateUpdateBusinessSubscriptionResponse>?> CreateUpdateBusinessSubscription(CreateUpdateBusinessSubscriptionRequest request)
        {
            var baseResponse = new BaseResponse<CreateUpdateBusinessSubscriptionResponse>();


            string url = $"{ApiEndpoints.Subscription.CreateUpdateBusinessSubscription}";

            var response = await _apiService.PostAsync(url, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse<CreateUpdateBusinessSubscriptionResponse>>();
            }

            return baseResponse;
        }

    }
}
