namespace IApply.Frontend.Models.Workspace;

public class WorkspaceUserResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public List<UserWorkspaces> UserWorkspaces { get; set; } = [];
}

public class UserWorkspaces
{
    public int WorkspaceId { get; set; }
    public string WorkspaceName { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public List<WorkspaceProjects> WorkspaceProjects { get; set; } = [];

}

public class WorkspaceProjects
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
}
