using IApply.Frontend.Common.Constants;
using IApply.Frontend.Models;
using IApply.Frontend.Models.Business;

namespace IApply.Frontend.Services.ApiService.Business
{
    public class BusinessService : IBusinessService
    {
        private readonly ApiService _apiService;
        private readonly AlertService _alertService;
        private readonly HttpClient _httpClient;

        public BusinessService(ApiService apiService, AlertService alertService, HttpClient httpClient)
        {
            _apiService = apiService;
            _alertService = alertService;
            _httpClient = httpClient;
        }
        public async Task<BaseResponse<CreateBusinessResponse>?> CreateUpdateBusiness(CreateBusinessRequest request)
        {
            var baseResponse = new BaseResponse<CreateBusinessResponse>();

            string url = $"{ApiEndpoints.Business.CreateUpdateBusiness}";

            var response = await _apiService.PostAsync(url, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse<CreateBusinessResponse>>();
            }

            return baseResponse;
        }
        public async Task<BaseResponseListing<BusinessListing>?> GetBusinessListing(GetBusinessListingRequest request)
        {
            var baseResponse = new BaseResponseListing<BusinessListing>();


            string url = $"{ApiEndpoints.Business.List}";

            var response = await _apiService.PostAsync(url, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponseListing<BusinessListing>>();
            }

            return baseResponse;
        }
        public async Task<BaseResponseListing<GetAllCurrency>?> GetAllCurrency(int currencyId)
        {
            var baseResponse = new BaseResponseListing<GetAllCurrency>();

            string url = ApiEndpoints.Business.GetAllCurrency;
            if(currencyId > 0)
            {
                url = $"{url}?currencyId={currencyId}";
            }
            var response = await _apiService.GetAsync(url);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponseListing<GetAllCurrency>>();
            }

            return baseResponse;
        }

        //public async Task<ListingBaseResponse<Attendance>?> GetAttendance()
        //{
        //    var baseResponse = new ListingBaseResponse<Attendance>();
        //    var response = await _apiService.GetAsync(ApiEndpoints.Attendances.GetAttendances);

        //    if (response.StatusCode == 200)
        //    {
        //        baseResponse = await response.Response.ReadFromJsonAsync<ListingBaseResponse<Attendance>>();
        //    }
        //    return baseResponse;
        //}
    }
}
