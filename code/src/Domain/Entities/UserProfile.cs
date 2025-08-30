
namespace OpenProfileAPI.Domain.Entities;

public class UserProfile : BaseAuditableEntity
{
    public int UserId { get; set; }

    public string DisplayName { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public DateOnly? DateOfBirth { get; set; }

    public int? CityId { get; set; }
    public virtual Category? City { get; set; }
    public int? StateId { get; set; }
    public virtual Category? State { get; set; }
    public int? CountryId { get; set; }
    public virtual Category? Country { get; set; }
    public string? PostalCode { get; set; } = string.Empty;

    public int? GenderId { get; set; }
    public virtual Category? Gender { get; set; }
    public string? NationalId { get; set; }
    public int? MaritalStatusId { get; set; }
    public virtual Category? MaritalStatus { get; set; }
    public string? Occupation { get; set; }
    public string? LinkedInProfile { get; set; }
    public string? UserBio { get; set; }

    // Navigation to User
    public virtual User User { get; set; } = default!;
}
