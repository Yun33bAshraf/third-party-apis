namespace OpenProfileAPI.Frontend.Models.Profile.ProfileUpdate;

public class ProfileUpdateRequest
{
    public string? Address { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public DateOnly? DateOfBirth { get; set; }

    public int? CityId { get; set; }
    public int? StateId { get; set; }
    public int? CountryId { get; set; }
    public string? PostalCode { get; set; } = string.Empty;

    public int GenderId { get; set; }
    public string? NationalId { get; set; }
    public int? MaritalStatusId { get; set; }
    public string? Occupation { get; set; }
    public string? LinkedInProfile { get; set; }
    public string? UserBio { get; set; }
}
