namespace OpenProfileAPI.Frontend.Models.Profile.AddEducation;

public class EducationCreateRequest
{
    public List<EducationDto> Educations { get; set; } = [];
}

public class EducationDto
{
    public int Id { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string FieldOfStudy { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
