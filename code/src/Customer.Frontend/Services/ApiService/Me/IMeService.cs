using OpenProfileAPI.Frontend.Models;
using OpenProfileAPI.Frontend.Models.Profile;
using OpenProfileAPI.Frontend.Models.Rights.GetRights;

namespace OpenProfileAPI.Frontend.Services.ApiService.Me
{
    public interface IMeService
    {
        //Task<GetProfileResponse?> GetProfile();
        Task<BaseResponse?> UpdateProfile(ChangeProfileRequest request);
        Task<GetRightsResponse?> GetMyRights(string Token);
        Task<List<dynamic>?> FindDrivers(string Latitude, string Longitude, string Radius);

    }
}
