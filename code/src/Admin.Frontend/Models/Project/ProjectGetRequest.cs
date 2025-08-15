namespace IApply.Frontend.Models.Project;

public class ProjectGetRequest
{
    public int WorkspaceId { get; set; }
    public string? ProjectName { get; set; }
    public int ProjectId { get; set; }
    public string? TicketTitle { get; set; }
    public int StatusId { get; set; }
    public int PriorityId { get; set; }
    public int CategoryId { get; set; }
    public int AssignedToId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
