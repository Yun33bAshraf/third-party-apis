using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.Roles;

public class RolesCreateUpdateRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string RoleName { get; set; }
    public int RoleId { get; set; }

    public List<CreateRoleRightModel>? Rights { get; set; }
}

public class CreateRoleRightModel
{
    public int RightId { get; set; }
}

