namespace IApply.Frontend.Models.Project.UserProject;

public class UserProjectGetResponse
{
    public int ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int WorkSpaceId { get; set; }
    public string WorkSpaceName { get; set; } = string.Empty;
    public DateTimeOffset CreatedDate { get; set; }
    public List<MyTicketsResponse> MyTickets { get; set; } = [];
    public List<OtherProjectTicketsResponse> OtherProjectTickets { get; set; } = [];
}

public class MyTicketsResponse
{
    public int TicketId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreatedDate { get; set; }
}

public class OtherProjectTicketsResponse
{
    public int TicketId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreatedDate { get; set; }
}
