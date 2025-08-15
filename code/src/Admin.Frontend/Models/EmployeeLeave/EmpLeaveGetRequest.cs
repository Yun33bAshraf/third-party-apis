namespace IApply.Frontend.Models.EmployeeLeave;

public class EmpLeaveGetRequest
{
    public int LeaveId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
    public int? LeaveStatus { get; set; }
    public int? LeaveType { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

