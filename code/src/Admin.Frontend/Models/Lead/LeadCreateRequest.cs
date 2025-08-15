using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.Lead
{
    public class LeadCreateRequest
    {
        public int LeadId { get; set; }
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(30, ErrorMessage = "Full Name can't be longer than 30 characters.")]
        [RegularExpression(@"^[A-Za-z\s/_-]*$", ErrorMessage = "Full Name must contain letters")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(30, ErrorMessage = "Email is incorrect")]
        public string? Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "phone number is required.")]
        [StringLength(11, ErrorMessage = "phone Number can't be longer than 11 characters.")]
        [RegularExpression(@"^(\d[\s\-]?)?\d{9,10}$", ErrorMessage = "Invalid phone number.")]
        public string? PhoneNumber { get; set; } = string.Empty;

        public string? CompanyName { get; set; } = string.Empty;

        public string? JobTitleRole { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lead Source is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Lead Source")]
        public int LeadSourceId { get; set; }

        [Required(ErrorMessage = "Lead Status is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Lead Status")]
        public int LeadStatusId { get; set; }

        [Required(ErrorMessage = "Industry is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Industry")]
        public int IndustryId { get; set; }

        public string? Comment { get; set; } = string.Empty;
    }

}

