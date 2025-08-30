namespace OpenProfileAPI.Frontend.Models.Profile.GetProfile;

public class ProfileGetResponseModel
{
    public int UserId { get; set; }
    public int UserProfileId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public DateOnly? DateOfBirth { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public int UserTypeId { get; set; }
    public string UserType { get; set; } = string.Empty;
    public bool IsProfileCompleted { get; set; }
    public int? CityId { get; set; }
    public string? City { get; set; } = string.Empty;
    public int? StateId { get; set; }
    public string? State { get; set; } = string.Empty;
    public int? CountryId { get; set; }
    public string? Country { get; set; } = string.Empty;
    public string? PostalCode { get; set; } = string.Empty;
    public int? GenderId { get; set; }
    public string? Gender { get; set; } = string.Empty;
    public string? NationalId { get; set; }
    public int? MaritalStatusId { get; set; }
    public string? MaritalStatus { get; set; } = string.Empty;
    public string? Occupation { get; set; }
    public string? LinkedInProfile { get; set; }
    public string? UserBio { get; set; }
    public List<GetCurrentUserExperience> Experiences { get; set; } = [];
    public List<GetCurrentUserEducation> Educations { get; set; } = [];
    public List<GetCurrentUserSkills> Skills { get; set; } = [];
}

public class GetCurrentUserExperience
{
    public string CompanyName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrent { get; set; }
    public string CurrentlyEmployed { get; set; } = string.Empty; // e.g., "Yes" or "No"
}

public class GetCurrentUserEducation
{
    public string Degree { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string FieldOfStudy { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class GetCurrentUserSkills
{
    public int SkillId { get; set; }
    public int ProficiencyLevelId { get; set; } // e.g., Beginner, Intermediate, Expert
    public string ProficiencyLevel { get; set; } = string.Empty;
}
