using System.ComponentModel.DataAnnotations;
using IApply.Frontend.Common.CustomAttributes;

namespace IApply.Frontend.Models.Project;

public class ProjectResponse
{
    [Display(Name = "ID")] 
    public int ProjectId { get; set; }
    
    [Display(Name = "NAME")] 
    public string ProjectName { get; set; } = string.Empty;
    
    [Display(Name = "DESCRIPTION")] 
    public string Description { get; set; } = string.Empty;
    
    [IgnoreInTable]
    public int WorkspaceId { get; set; }
    
    [IgnoreInTable]
    public string WorkspaceName { get; set; } = string.Empty;
    
    [Display(Name = "TOTAL TICKETS")] 
    public int TicketCount { get; set; }
    
    [Display(Name = "CREATED DATE")] 
    public string CreatedAt { get; set; } = string.Empty;
    
    [IgnoreInTable]
    public string UpdatedAt { get; set; } = string.Empty;
    
    [IgnoreInTable]
    public List<ProjectTicketsResponse> Tickets { get; set; } = [];
    
    [IgnoreInTable]
    public List<ProjectUserResponse> Users { get; set; } = [];
}

public class ProjectTicketsResponse
{
    [Display(Name = "ID")]
    public int TicketId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    [IgnoreInTable]
    public int StatusId { get; set; }

    [Display(Name = "STATUS")]
    public string Status { get; set; } = string.Empty;
    
    [IgnoreInTable]
    public int PriorityId { get; set; }
    
    [Display(Name = "PRIORITY")]
    public string Priority { get; set; } = string.Empty;
    
    [Display(Name = "INDEX")]
    public int IndexId { get; set; }
    
    [IgnoreInTable]
    public int? AssignedToId { get; set; }
    
    [Display(Name = "ASSIGNED TO")]
    public string AssignedTo { get; set; } = string.Empty;
    
    [IgnoreInTable]
    public int CategoryId { get; set; }
    
    [Display(Name = "CATEGORY")]
    public string Category { get; set; } = string.Empty;
    
    [IgnoreInTable]
    public int? CreatedById { get; set; }
    
    [Display(Name = "CREATED BY")]
    public string CreatedBy { get; set; } = string.Empty;
    
    [Display(Name = "CREATED DATE")]
    public string? CreatedDate { get; set; } = string.Empty;
}

public class ProjectUserResponse
{
    public int UserId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
}
