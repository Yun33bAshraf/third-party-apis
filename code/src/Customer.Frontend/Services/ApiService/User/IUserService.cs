using IApply.Frontend.Models;
using IApply.Frontend.Models.Profile.AddEducation;
using IApply.Frontend.Models.Profile.AddExperience;
using IApply.Frontend.Models.Profile.GetProfile;
using IApply.Frontend.Models.Profile.ProfileUpdate;
using IApply.Frontend.Models.Profile.UpdateEducation;

namespace IApply.Frontend.Services.ApiService.User;

public interface IUserService
{
    Task<BaseResponse<ProfileGetResponseModel>?> ProfileGet();
    Task<BaseResponse<ProfileUpdateResponse>> UpdateProfile(ProfileUpdateRequest request);
    Task<BaseResponse<ExperienceCreateResponse>> AddExperiencesAsync(ExperienceCreateRequest request);
    Task<BaseResponse<EductionCreateResponse>> AddEductionsAsync(EducationCreateRequest request);
    Task<BaseResponse<EducationUpdateResponse>> UpdateEducationAsync(EducationUpdateRequest request);
    Task<ProfileGetResponseModel?> GetUserProfileAsync();
}
