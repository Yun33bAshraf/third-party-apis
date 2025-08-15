using IApply.Frontend.Models.Auth._2Factor;
using IApply.Frontend.Common.Constants;
using IApply.Frontend.Models;
using IApply.Frontend.Services;
using IApply.Frontend.Models.Profile;
using IApply.Frontend.Models.Rights.GetRights;

namespace IApply.Frontend.Services.ApiService.Me
{
    public class MeService : IMeService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiService _apiService;
        private readonly AlertService _alertService;
        public MeService(HttpClient httpClient, ApiService apiService, AlertService alertService)
        {
            _httpClient = httpClient;
            _apiService = apiService;
            _alertService = alertService;
        }

        //public async Task<GetProfileResponse?> GetProfile()
        //{
        //    var baseResponse = new GetProfileResponse();
        //    var response = await _apiService.GetAsync(ApiEndpoints.User.MyProfile);

        //    if (response.StatusCode == 200)
        //    {
        //        baseResponse = await response.Response.ReadFromJsonAsync<GetProfileResponse>();
        //    }
        //    return baseResponse;
        //}

        public async Task<BaseResponse?> UpdateProfile(ChangeProfileRequest request)
        {
            var baseResponse = new BaseResponse();
            // Create MultipartFormDataContent
            var content = new MultipartFormDataContent
            {
                { new StringContent(request.UserProfile.DisplayName ?? string.Empty), "DisplayName" },
                { new StringContent(request.UserProfile.City ?? string.Empty), "City" },
                { new StringContent(request.UserProfile.Address ?? string.Empty), "Address" },
                { new StringContent(request.UserProfile.Email ?? string.Empty), "Email" },
                { new StringContent(request.UserProfile.Phone ?? string.Empty), "Phone" },
                //{ new StringContent(string.Empty), "UserProfile.DateOfBirth" },
                //{ new StringContent(((int)request.UserProfile.GenderType).ToString() ?? string.Empty), "UserProfile.GenderType" },
                //{ new StringContent(((int)request.UserProfile.Lanugage).ToString()  ?? string.Empty), "UserProfile.Lanugage" },
                //{ new StringContent(((int)request.UserProfile.City).ToString()  ?? string.Empty), "UserProfile.City" },
                //{ new StringContent(request.UserProfile.NationalId.ToString() ?? string.Empty), "UserProfile.NationalId" },
                //{ new StringContent(((int)request.UserProfile.RidePreference).ToString() ?? string.Empty), "UserProfile.RidePreference" }
            };

            //// Add file streams if they exist
            //if (request.UserProfile.Photo != null)
            //    content.Add(new StreamContent(request.UserProfile.Photo), "UserProfile.Photo", "photo.jpg");

            //if (request.UserProfile.DriverLicense != null)
            //    content.Add(new StreamContent(request.UserProfile.DriverLicense), "UserProfile.DriverLicense", "license.jpg");

            var response = await _apiService.PutMultipartAsync(ApiEndpoints.User.UpdateProfile, content);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
            }
            return baseResponse;
        }

        public async Task<GetRightsResponse?> GetMyRights(string Token)
        {
            var baseResponse = new GetRightsResponse();
            if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
            }
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
            baseResponse = await _httpClient.GetFromJsonAsync<GetRightsResponse>(_apiService._baseApiUrl + ApiEndpoints.Me.GetMyRights);
            return baseResponse;
        }

        public async Task<List<dynamic>?> FindDrivers(string Latitude, string Longitude, string Radius)
        {
            var baseResponse = new List<dynamic>();
            // Create MultipartFormDataContent
            var content = new MultipartFormDataContent
            {
                { new StringContent(Latitude ?? string.Empty), "Latitude" },
                { new StringContent(Longitude ?? string.Empty), "Longitude" },
                { new StringContent(Radius ?? string.Empty), "RadiusInKm" }
            };
            var response = await _apiService.PostMultipartAsync(ApiEndpoints.Me.FindDrivers, content);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<List<dynamic>>();
            }
            return baseResponse;
        }
    }
}
