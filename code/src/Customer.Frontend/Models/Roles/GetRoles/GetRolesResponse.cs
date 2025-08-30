using System.ComponentModel.DataAnnotations;
using IApply.Frontend.Common.CustomAttributes;
using OpenProfileAPI.Frontend.Models;

namespace OpenProfileAPI.Frontend.Models.Roles.GetRoles;

public class GetRolesResponse : BaseResponse
{
    public List<Role> Roles { get; set; }
}

public class Role
{
    [IgnoreInTable]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    [BoolDisplay("Active", "InActive")]
    [Display(Name = "Status")]
    public bool IsActive { get; set; } = false;
}

public class RoleModel
{
    [Display(Name = "ID")]
    [ColumnWidth("w-1")]
    public int RoleId { get; set; }

    [Display(Name = "Role Name")]
    public string RoleName { get; set; }

    [IgnoreInTable]
    public string? RightsName { get; set; } // Store full value if needed

    [Display(Name = "Rights")]
    public string? LimitedLengthRightsName
    {
        get => RightsName?.Length > 30 ? string.Concat(RightsName.AsSpan(0, 30), "...") : RightsName;
    }

    [Display(Name = "Created Date")]
    public DateTime? CreatedAt { get; set; }

    [IgnoreInTable]
    [Display(Name = "RoleRights")]
    public List<RoleRights> Rights { get; set; } = [];
}

public class RoleRights
{
    public int RightId { get; set; }
    public string RightName { get; set; }
}
