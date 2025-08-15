using System.ComponentModel.DataAnnotations;
using IApply.Frontend.Common.CustomAttributes;

namespace IApply.Frontend.Models.User.UserProfile;

public class UserProfileGetResponse
{
    public int Id { get; set; }

    [IgnoreInTable]
    public int UserId { get; set; }

    [IgnoreInTable]
    [Display(Name = "Display Name")]
    public string DisplayName { get; set; } = string.Empty;
    
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;
    
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;
    
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;
    
    [Display(Name = "Mobile Number")]
    public string MobileNumber { get; set; } = string.Empty;
    
    [Display(Name = "Date Of Birth")]
    public DateOnly DateOfBirth { get; set; }
    
    [IgnoreInTable] 
    public int WorkspaceId { get; set; }
    
    [IgnoreInTable] 
    public string WorkspaceName { get; set; } = string.Empty;
    
    [IgnoreInTable] 
    public int RoleId { get; set; }
    
    [Display(Name = "Role")]
    public string RoleName { get; set; } = string.Empty;
    
    [Display(Name = "Address")]
    public string Address { get; set; } = string.Empty;
    
    [IgnoreInTable] 
    public int Gender { get; set; }
    
    [Display(Name = "Created Date")]
    public DateTimeOffset Created { get; set; }
    
    [IgnoreInTable] 
    public int? CreatedBy { get; set; }
    
    [IgnoreInTable] 
    public DateTimeOffset LastModified { get; set; }
    
    [IgnoreInTable] 
    public int? LastModifiedBy { get; set; }
    
    [IgnoreInTable] 
    public List<UserRightsResponse>? UserRights { get; set; } = [];
}

public class UserRightsResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
