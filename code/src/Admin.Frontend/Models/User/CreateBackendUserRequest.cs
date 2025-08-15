using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.User;

public class CreateBackendUserRequest
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Password")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    public string Password { get; set; }

    [Required]
    [Display(Name = "Mobile Number")]
    [RegularExpression(@"^\+?[0-9]\d{1,14}$", ErrorMessage = "Invalid mobile number format.")]
    public string MobileNumber { get; set; }
}
