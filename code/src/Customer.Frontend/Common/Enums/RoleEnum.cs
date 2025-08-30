using System.ComponentModel;

namespace OpenProfileAPI.Frontend.Common.Enums;

public enum RoleEnum
{
    Administrator = 1,
    HR = 2,
    Employee = 3,
}

public enum RoleType
{
    [Description("Workspace")]
    Workspace = 1,
    [Description("Project")]
    Project = 2
}

public enum ApplicationRole
{
    [Description("Workspace Admin")]
    WorkspaceAdmin = 1,

    [Description("Workspace Member")]
    WorkspaceMember = 2,

    [Description("Workspace Guest")]
    WorkspaceGuest = 3,

    [Description("Project Admin")]
    ProjectAdmin = 4,

    [Description("Project Member")]
    ProjectMember = 5,

    [Description("Project Guest")]
    ProjectGuest = 6
}
