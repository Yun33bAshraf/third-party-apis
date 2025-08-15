using IApply.Frontend.Common.Constants;
using IApply.Frontend.Models;
using IApply.Frontend.Models.Project;
using IApply.Frontend.Models.Project.UserProject;

namespace IApply.Frontend.Services.ApiService.Project;

public class ProjectService : IProjectService
{
    private readonly ApiService _apiService;
    private readonly AlertService _alertService;
    private readonly HttpClient _httpClient;

    public ProjectService(ApiService apiService, AlertService alertService, HttpClient httpClient)
    {
        _apiService = apiService;
        _alertService = alertService;
        _httpClient = httpClient;
    }

    public async Task<BaseResponseListing<ProjectResponse>?> GetProjectAsync(int workspaceId, string? projectName, int projectId, string? ticketTitle, int priorityId, int statusId, int categoryId, int assignedToId, int pageNumber, int pageSize)
    {
        try
        {
            var queryParams = new List<string>();

            if (workspaceId > 0)
                queryParams.Add($"WorkspaceId={workspaceId}");

            if (!string.IsNullOrWhiteSpace(projectName))
                queryParams.Add($"ProjectName={Uri.EscapeDataString(projectName)}");

            if (projectId > 0)
                queryParams.Add($"ProjectId={projectId}");

            if (workspaceId > 0)
                queryParams.Add($"WorkspaceId={workspaceId}");

            if (!string.IsNullOrWhiteSpace(ticketTitle))
                queryParams.Add($"TicketTitle={Uri.EscapeDataString(ticketTitle)}");

            if (priorityId > 0)
                queryParams.Add($"PriorityId={priorityId}");

            if (statusId > 0)
                queryParams.Add($"StatusId={statusId}");

            if (categoryId > 0)
                queryParams.Add($"CategoryId={categoryId}");

            if (assignedToId > 0)
                queryParams.Add($"AssignedToId={assignedToId}");

            queryParams.Add($"PageNumber={pageNumber}");
            queryParams.Add($"PageSize={pageSize}");

            var queryString = queryParams.Count != 0 ? "?" + string.Join("&", queryParams) : string.Empty;
            var requestUrl = $"{ApiEndpoints.Project.GetProject}{queryString}";

            Console.WriteLine($"API Request URL: {requestUrl}");

            var response = await _apiService.GetAsync(requestUrl);

            if (response.StatusCode == 200 && response.Response != null)
            {
                return await response.Response.ReadFromJsonAsync<BaseResponseListing<ProjectResponse>>();
            }

            return new BaseResponseListing<ProjectResponse>
            {
                Status = false,
                Message = $"API call failed with status code {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            return new BaseResponseListing<ProjectResponse>
            {
                Status = false,
                Message = $"An error occurred: {ex.Message}"
            };
        }
    }

    public async Task<BaseResponseListing<UserProjectGetResponse>?> GetUserProjectAsync(int pageNumber, int pageSize)
    {
        try
        {
            var queryParams = new List<string>();

            queryParams.Add($"PageNumber={pageNumber}");
            queryParams.Add($"PageSize={pageSize}");

            var queryString = queryParams.Count != 0 ? "?" + string.Join("&", queryParams) : string.Empty;
            var requestUrl = $"{ApiEndpoints.Project.GetUserProject}{queryString}";

            Console.WriteLine($"API Request URL: {requestUrl}");

            var response = await _apiService.GetAsync(requestUrl);

            if (response.StatusCode == 200 && response.Response != null)
            {
                return await response.Response.ReadFromJsonAsync<BaseResponseListing<UserProjectGetResponse>>();
            }

            return new BaseResponseListing<UserProjectGetResponse>
            {
                Status = false,
                Message = $"API call failed with status code {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            return new BaseResponseListing<UserProjectGetResponse>
            {
                Status = false,
                Message = $"An error occurred: {ex.Message}"
            };
        }
    }
}
