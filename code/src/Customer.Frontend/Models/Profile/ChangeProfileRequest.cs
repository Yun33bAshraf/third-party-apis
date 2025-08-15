using IApply.Frontend.Models.Auth;
using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.Profile;

public class ChangeProfileRequest
{
    public GetUsersProfile UserProfile { get; set; } = new GetUsersProfile();
}

public class MyProfile
{
    [Required]
    [StringLength(50)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Required]
    [StringLength(50)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Address")]
    public string Address { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [StringLength(15)]
    [Display(Name = "Mobile Number")]
    public string MobileNumber { get; set; }
}
