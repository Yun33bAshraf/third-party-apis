using IApply.Frontend.Models;
using IApply.Frontend.Models.Settings.Configurations;
using IApply.Frontend.Models.Employee;

namespace IApply.Frontend.Services.ApiService.Category
{
    public interface ICategoryService
    {
        Task<ListingBaseResponse<GetCategory>?> GetCategory(GetCategory request);
        Task<ListingBaseResponse<EntityGetRequest>?> GetEntity(EntityGetRequest request);
        Task<ListingBaseResponse<CategoryGetResponse>?> GetCategories(CategoryGetRequest request);
        Task<BaseResponse?> CreateCategories(CategoryCreate request);
    }
}
