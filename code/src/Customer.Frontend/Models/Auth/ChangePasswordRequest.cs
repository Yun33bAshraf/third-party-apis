using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.Auth;

public class ChangePasswordRequest
{
    [Required]
    [Display(Name = "Current Password")]
    [MinLength(8)]
    public string OldPassword { get; set; }

    [Required]
    [Display(Name = "New Password")]
    [MinLength(8)]
    [PasswordNotEqualToCurrent]
    public string NewPassword { get; set; }
}

public class PasswordNotEqualToCurrentAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var changePasswordRequest = (ChangePasswordRequest)validationContext.ObjectInstance;

        if (value != null && value.ToString() == changePasswordRequest.OldPassword)
        {
            var memberNames = new[] { validationContext.MemberName ?? validationContext.DisplayName };
            return new ValidationResult("New password cannot be the same as the current password.", memberNames);
        }

        return ValidationResult.Success;
    }
}
