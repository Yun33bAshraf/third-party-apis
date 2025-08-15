using IApply.Frontend.Models;
using IApply.Frontend.Models.Profile;
using IApply.Frontend.Models.Rights.GetRights;

namespace IApply.Frontend.Services.ApiService.Me
{
    public interface IMeService
    {
        //Task<GetProfileResponse?> GetProfile();
        Task<BaseResponse?> UpdateProfile(ChangeProfileRequest request);
        Task<GetRightsResponse?> GetMyRights(string Token);
        Task<List<dynamic>?> FindDrivers(string Latitude, string Longitude, string Radius);

    }
}
