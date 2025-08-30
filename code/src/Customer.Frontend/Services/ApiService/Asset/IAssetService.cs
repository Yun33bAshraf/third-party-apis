using OpenProfileAPI.Frontend.Models;
using OpenProfileAPI.Frontend.Models.Assets;

namespace OpenProfileAPI.Frontend.Services.ApiService.Asset
{
    public interface IAssetService
    {
        Task<ListingBaseResponse<Assets>?> GetAssets(AssetsRequest request);
        Task<BaseResponse?> UpdateAsset(CreateAssetRequest request);
        Task<BaseResponse?> AssetFileUpload(AssetFileUploadRequest request);
        Task<BaseResponse?> CreateBackendAsset(CreateAssetRequest request);
    }
}
