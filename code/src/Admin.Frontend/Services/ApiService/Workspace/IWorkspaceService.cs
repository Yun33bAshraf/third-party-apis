using IApply.Frontend.Models;
using IApply.Frontend.Models.Workspace;

namespace IApply.Frontend.Services.ApiService.Workspace;

public interface IWorkspaceService
{
    Task<BaseResponse<WorkspaceUserResponse>?> UserWorkspaceGet(int pageNumber, int pageSize);
}
