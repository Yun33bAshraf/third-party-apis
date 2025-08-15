using System.ComponentModel.DataAnnotations;

public class CreateUserRequest
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(15, ErrorMessage = "First name can't be longer than 15 characters.")]
    [RegularExpression(@"^[A-Za-z\s_-]*$", ErrorMessage = "First name must contain only letters.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(15, ErrorMessage = "Last name can't be longer than 15 characters.")]
    [RegularExpression(@"^[A-Za-z\s_-]*$", ErrorMessage = "Last name must contain only letters.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date of Birth is required.")]
    public DateOnly? DateOfBirth { get; set; }

    [Required(ErrorMessage = "Mobile Number is required.")]
    //[Phone(ErrorMessage = "Invalid Mobile Number.")]
    public string MobileNumber { get; set; } = string.Empty;

    public DateTime? LastLoginDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? LandLineNumber { get; set; }
    public string? PermanentAddress { get; set; }
    public DateTime? CreatedAt { get; set; }

}
