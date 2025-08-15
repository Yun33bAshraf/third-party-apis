using IApply.Frontend.Models;
using IApply.Frontend.Models.Project;
using IApply.Frontend.Models.Project.UserProject;

namespace IApply.Frontend.Services.ApiService.Project;

public interface IProjectService
{
    Task<BaseResponseListing<ProjectResponse>?> GetProjectAsync(int workspaceId, string? projectName, int projectId, string? ticketTitle, int priorityId, int statusId, int categoryId, int assignedToId, int pageNumber, int pageSize);
    Task<BaseResponseListing<UserProjectGetResponse>?> GetUserProjectAsync(int pageNumber, int pageSize);
}
