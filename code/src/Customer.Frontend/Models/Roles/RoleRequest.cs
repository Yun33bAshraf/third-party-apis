using System.ComponentModel.DataAnnotations;

namespace OpenProfileAPI.Frontend.Models.Roles;

public class RoleRequest
{
    public Guid RecordId { get; set; }
    [Required]
    [RegularExpression(@"^(?=.*[a-zA-Z]).+$", ErrorMessage = "The Name must contain at least one alphabet character.")]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
}

public class RolesGetRequest
{
    public int RoleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
