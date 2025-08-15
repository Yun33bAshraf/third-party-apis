using System.Net.Http.Headers;
using IApply.Frontend.Common.Constants;
using IApply.Frontend.Models;
using IApply.Frontend.Services;
using IApply.Frontend.Models.Assets;

namespace IApply.Frontend.Services.ApiService.Asset
{
    public class AssetService : IAssetService
    {
        private readonly ApiService _apiService;
        private readonly AlertService _alertService;
        private readonly HttpClient _httpClient;

        public AssetService(ApiService apiService, AlertService alertService, HttpClient httpClient)
        {
            _apiService = apiService;
            _alertService = alertService;
            _httpClient = httpClient;
        }
        public async Task<ListingBaseResponse<Assets>?> GetAssets(AssetsRequest request)
        {
            try
            {
                var baseResponse = new ListingBaseResponse<Assets>();

                var queryParams = new List<string>();

                if (request.AssetId > 0 || request.AssetId != null)
                {
                    queryParams.Add($"AssetId={request.AssetId}");
                }

                if (!string.IsNullOrWhiteSpace(request.AssetName))
                {
                    queryParams.Add($"AssetName={Uri.EscapeDataString(request.AssetName)}");
                }

                if (request.CategoryId.HasValue && request.CategoryId > 0)
                {
                    queryParams.Add($"CategoryId={request.CategoryId}");
                }

                if (request.SubCategoryId.HasValue && request.SubCategoryId > 0)
                {
                    queryParams.Add($"SubCategoryId={request.SubCategoryId}");
                }

                if (request.StatusId.HasValue && request.StatusId > 0)
                {
                    queryParams.Add($"StatusId={request.StatusId}");
                }

                if (request.EndDate != null)
                {
                    var endDate = request.EndDate == default ? DateTime.UtcNow : request.EndDate;
                    queryParams.Add($"EndDate={endDate:yyyy-MM-dd}");
                }
                queryParams.Add($"PageNumber={request.PageNumber}");
                queryParams.Add($"PageSize={request.PageSize}");

                var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
                var apiUrl = ApiEndpoints.Assets.GetAssets + queryString;

                Console.WriteLine("API Request URL: " + apiUrl); // Debugging

                var response = await _apiService.GetAsync(apiUrl);

                if (response.StatusCode == 200)
                {
                    baseResponse = await response.Response.ReadFromJsonAsync<ListingBaseResponse<Assets>>();
                }

                return baseResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<BaseResponse?> UpdateAsset(CreateAssetRequest request)
        {
            var baseResponse = new BaseResponse();
            var response = await _apiService.PutAsync(ApiEndpoints.Assets.UpdateAssets, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
            }

            return baseResponse;

        }
        public async Task<BaseResponse?> AssetFileUpload(AssetFileUploadRequest request)
        {
            var baseResponse = new BaseResponse();

            using var content = new MultipartFormDataContent();

            // Add AssetId as a form field
            content.Add(new StringContent(request.AssetId.ToString()), "AssetId");

            if (request.AssetFile != null)
            {
                int count = 0;
                foreach (var fileModel in request.AssetFile)
                {
                    // Add CategoryId and SubCategoryId as form fields
                    content.Add(new StringContent(fileModel.CategoryId.ToString()), $"AssetFile[{count}].CategoryId");
                    if (fileModel.SubCategoryId.HasValue)
                    {
                        content.Add(new StringContent(fileModel.SubCategoryId.Value.ToString()), $"AssetFile[{count}].SubCategoryId");
                    }

                    // Add files
                    var file = fileModel.Files;
                    var fileContent = new StreamContent(file.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                    content.Add(fileContent, $"AssetFile[{count}].Files", file.Name);
                    count++;
                }
            }

            var response = await _apiService.PostMultipartAsync(ApiEndpoints.Assets.AssetFileUpload, content);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
            }

            return baseResponse;
        }
        public async Task<BaseResponse?> CreateBackendAsset(CreateAssetRequest request)
        {
            var baseResponse = new BaseResponse();
            var response = await _apiService.PostAsync(ApiEndpoints.Assets.PostAssets, request);

            if (response.StatusCode == 200)
            {
                baseResponse = await response.Response.ReadFromJsonAsync<BaseResponse>();
            }

            return baseResponse;

        }
    }
}
