using IApply.Frontend.Common.Constants;
using IApply.Frontend.Models;
using IApply.Frontend.Models.Workspace;

namespace IApply.Frontend.Services.ApiService.Workspace;

public class WorkspaceService : IWorkspaceService
{
    private readonly ApiService _apiService;
    private readonly AlertService _alertService;
    private readonly HttpClient _httpClient;

    public WorkspaceService(ApiService apiService, AlertService alertService, HttpClient httpClient)
    {
        _apiService = apiService;
        _alertService = alertService;
        _httpClient = httpClient;
    }

    public async Task<BaseResponse<WorkspaceUserResponse>?> UserWorkspaceGet(int pageNumber, int pageSize)
    {
        try
        {
            var baseResponse = new BaseResponse<WorkspaceUserResponse>();

            var queryParams = new List<string>();

            queryParams.Add($"PageNumber={pageNumber}");
            queryParams.Add($"PageSize={pageSize}");

            var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
            var apiUrl = ApiEndpoints.Workspace.UserWorkspaceGet + queryString;

            Console.WriteLine("API Request URL: " + apiUrl);

            var response = await _apiService.GetAsync(apiUrl);

            if (response.Response != null && response.StatusCode == 200)
            {
                baseResponse = await response.Response
                    .ReadFromJsonAsync<BaseResponse<WorkspaceUserResponse>>();
            }

            return baseResponse;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
