namespace OpenProfileAPI.Frontend.Models.Profile.AddExperience;

public class ExperienceCreateRequest
{
    public List<ExperienceDto> Experiences { get; set; } = [];
}

public class ExperienceDto
{
    public string CompanyName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public bool IsCurrent { get; set; }
    public DateTime? EndDate { get; set; }
}
