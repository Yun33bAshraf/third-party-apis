using IApply.Frontend.Common.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using OpenProfileAPI.Frontend.Common.Enums;

namespace OpenProfileAPI.Frontend.Models.AuthPolicies
{
    public class AuthPolicy : IValidatableObject
    {
        [Required]
        [Display(Name = "GetUsersProfile Type")]
        public UserType UserType { get; set; }

        [BoolDisplay("Yes", "No")]
        [Display(Name = "Enforce 2-Factor Verification")]
        public bool Enforce2FactorVerification { get; set; }

        [BoolDisplay("Yes", "No")]
        [Display(Name = "Enforce Password Change on First Login")]
        public bool EnforcePasswordChangeOnFirstLogin { get; set; }

        [BoolDisplay("Yes", "No")]
        [Display(Name = "Enforce Backend Activation")]
        public bool EnforceBackendActivation { get; set; }

        [BoolDisplay("Yes", "No")]
        [Display(Name = "Enforce Email Confirmation")]
        public bool EnforceEmailConfirmation { get; set; }

        [BoolDisplay("Yes", "No")]
        [Display(Name = "Enforce Mobile Confirmation")]
        public bool EnforceMobileConfirmation { get; set; }

        [BoolDisplay("Yes", "No")]
        [Display(Name = "Enforce Profile Completion")]
        public bool EnforceProfileCompletion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Base Token Duration Minutes must be greater than zero.")]
        [Display(Name = "Base Token Duration (Minutes)")]
        public int BaseTokenDurationMinutes { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Full Token Duration Minutes must be greater than zero.")]
        [Display(Name = "Full Token Duration (Minutes)")]
        public int FullTokenDurationMinutes { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Refresh Token Duration Minutes must be greater than zero.")]
        [Display(Name = "Refresh Token Duration (Minutes)")]
        public int RefreshTokenDurationMinutes { get; set; }

        // Custom validation logic
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (RefreshTokenDurationMinutes <= FullTokenDurationMinutes)
            {
                yield return new ValidationResult(
                    "Refresh Token Duration Minutes must be greater than Full Token Duration Minutes.",
                    new[] { nameof(RefreshTokenDurationMinutes) }
                );
            }
        }
    }
}
