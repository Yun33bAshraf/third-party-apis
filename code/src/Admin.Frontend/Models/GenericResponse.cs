namespace IApply.Frontend.Models;

public class GenericResponse
{
    public int StatusCode { get; set; }
    public HttpContent? Response { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
    public string? ErrorMessage { get; set; } = string.Empty;
    public string? ExtraInformation { get; set; } = string.Empty;
}
