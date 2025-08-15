namespace IApply.Frontend.Models.User.ERPUsers;

public class ERPUsersRequest
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
