using IApply.Frontend.Common.Constants;
using IApply.Frontend.Models;
using IApply.Frontend.Models.Profile.AddEducation;
using IApply.Frontend.Models.Profile.AddExperience;
using IApply.Frontend.Models.Profile.GetProfile;
using IApply.Frontend.Models.Profile.ProfileUpdate;
using IApply.Frontend.Models.Profile.UpdateEducation;

namespace IApply.Frontend.Services.ApiService.User;

public class UserService(ApiService apiService, AlertService alertService, HttpClient httpClient) : IUserService
{
    public async Task<BaseResponse<ProfileGetResponseModel>?> ProfileGet()
    {
        try
        {
            var baseResponse = new BaseResponse<ProfileGetResponseModel>();

            var apiUrl = ApiEndpoints.User.ProfileGet;

            Console.WriteLine("API Request URL: " + apiUrl);

            var response = await apiService.GetAsync(apiUrl);

            if (response != null && response.Response != null && response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse<ProfileGetResponseModel>>();
            }

            return baseResponse;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<BaseResponse<ProfileUpdateResponse>> UpdateProfile(ProfileUpdateRequest request)
    {
        var baseResponse = new BaseResponse<ProfileUpdateResponse>();

        var response = await apiService.PutAsync(ApiEndpoints.User.ProfileUpdate, request);

        if (response.Response is not null && response.StatusCode == 200)
        {
            var result = await response.Response.ReadFromJsonAsync<BaseResponse<ProfileUpdateResponse>>();
            if (result is not null)
            {
                baseResponse = result;
            }
        }

        return baseResponse;
    }

    public async Task<BaseResponse<ExperienceCreateResponse>> AddExperiencesAsync(ExperienceCreateRequest request)
    {
        var baseResponse = new BaseResponse<ExperienceCreateResponse>();

        var response = await apiService.PostAsync(ApiEndpoints.Experience.ExperienceCreate, request);

        if (response.Response is not null && response.StatusCode == 200)
        {
            var result = await response.Response.ReadFromJsonAsync<BaseResponse<ExperienceCreateResponse>>();
            if (result is not null)
            {
                baseResponse = result;
            }
        }

        return baseResponse;
    }
    public async Task<BaseResponse<EductionCreateResponse>> AddEductionsAsync(EducationCreateRequest request)
    {
        var baseResponse = new BaseResponse<EductionCreateResponse>();

        var response = await apiService.PostAsync(ApiEndpoints.Education.EducationCreate, request);

        if (response.Response is not null && response.StatusCode == 200)
        {
            var result = await response.Response.ReadFromJsonAsync<BaseResponse<EductionCreateResponse>>();
            if (result is not null)
            {
                baseResponse = result;
            }
        }

        return baseResponse;
    }
    public async Task<BaseResponse<EducationUpdateResponse>> UpdateEducationAsync(EducationUpdateRequest request)
    {
        var baseResponse = new BaseResponse<EducationUpdateResponse>();
        var response = await apiService.PutAsync(ApiEndpoints.Education.EducationUpdate, request);

        if (response.Response is not null && response.StatusCode == 200)
        {
            var result = await response.Response.ReadFromJsonAsync<BaseResponse<EducationUpdateResponse>>();
            if (result is not null)
            {
                baseResponse = result;
            }
        }

        return baseResponse;
    }
    public async Task<ProfileGetResponseModel?> GetUserProfileAsync()
    {
        var response = await apiService.GetAsync(ApiEndpoints.User.ProfileGet);
        if (response.Response != null && response.StatusCode == 200)
        {
            return await response.Response.ReadFromJsonAsync<ProfileGetResponseModel>();
        }

        return null;
    }


}
