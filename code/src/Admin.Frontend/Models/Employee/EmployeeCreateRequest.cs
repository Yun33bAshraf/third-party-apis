using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.Employee
{
    public class EmployeeCreateRequest
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(30, ErrorMessage = "First name can't be longer than 30 characters.")]
        [RegularExpression(@"^[A-Za-z\s/_-]*$", ErrorMessage = "First name must contain letters")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(30, ErrorMessage = "Last name can't be longer than 30 characters.")]
        [RegularExpression(@"^[A-Za-z\s_-]*$", ErrorMessage = "Last name must contain only letters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(30, ErrorMessage = "Email is incorrect")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile number is required.")]
        [StringLength(11, ErrorMessage = "Mobile Number can't be longer than 11 characters.")]
        [RegularExpression(@"^(\d[\s\-]?)?\d{9,10}$", ErrorMessage = "Invalid mobile number.")]
        public string MobileNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateOnly? DateOfBirth { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Gender is required.")]
        public int Gender { get; set; }

        [Required(ErrorMessage = "Marital status is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Marital status is required.")]
        public int MaritalStatusId { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? UserId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Status is required.")]
        public int StatusId { get; set; }

        // Unused Email property (PersonalEmail) - comment out or remove if necessary
        public string PersonalEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "City is required.")]
        public int CityId { get; set; }

        // Unused PermanentAddress property - comment out or remove if necessary
        public string PermanentAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Current Address is required.")]
        [StringLength(100, ErrorMessage = "Current Address can't be longer than 100 characters.")]
        public string CurrentAddress { get; set; } = string.Empty;

        // Unused LandLineNumber property - comment out or remove if necessary
        public string LandLineNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of Joining is required.")]
        public DateTime? DateOfJoining { get; set; }

        [Required(ErrorMessage = "Father / Husband name is required.")]
        [StringLength(30, ErrorMessage = "Father / Husband name can't be longer than 30 characters.")]
        [RegularExpression(@"^[A-Za-z\s/_-]*$", ErrorMessage = "Father / Husband name must contain letters")]
        public string FatherHusbandName { get; set; } = string.Empty;


        [Required(ErrorMessage = "CNIC is required.")]
        [RegularExpression(@"^\d{5}-\d{7}-\d{1}$", ErrorMessage = "CNIC must be in the format XXXXX-XXXXXXX-X.")]
        public string Cnic { get; set; } = string.Empty;

        // Unused Designation property - comment out or remove if necessary
        public string Designation { get; set; } = string.Empty;

        public DateTime? ResignDate { get; set; }
        public DateTime? LastWorkingDate { get; set; }
        public string? Comment { get; set; }
    }
}
