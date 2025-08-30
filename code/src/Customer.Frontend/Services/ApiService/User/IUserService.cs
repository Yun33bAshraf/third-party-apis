using OpenProfileAPI.Frontend.Models;
using OpenProfileAPI.Frontend.Models.Profile.AddEducation;
using OpenProfileAPI.Frontend.Models.Profile.AddExperience;
using OpenProfileAPI.Frontend.Models.Profile.GetProfile;
using OpenProfileAPI.Frontend.Models.Profile.ProfileUpdate;
using OpenProfileAPI.Frontend.Models.Profile.UpdateEducation;

namespace OpenProfileAPI.Frontend.Services.ApiService.User;

public interface IUserService
{
    Task<BaseResponse<ProfileGetResponseModel>?> ProfileGet();
    Task<BaseResponse<ProfileUpdateResponse>> UpdateProfile(ProfileUpdateRequest request);
    Task<BaseResponse<ExperienceCreateResponse>> AddExperiencesAsync(ExperienceCreateRequest request);
    Task<BaseResponse<EductionCreateResponse>> AddEductionsAsync(EducationCreateRequest request);
    Task<BaseResponse<EducationUpdateResponse>> UpdateEducationAsync(EducationUpdateRequest request);
    Task<ProfileGetResponseModel?> GetUserProfileAsync();
}
