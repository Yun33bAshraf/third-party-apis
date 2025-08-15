using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.EmployeeLeave;

public class EmpLeaveCreateUpdateRequest
{
    public int LeaveId { get; set; }

    //[Range(1, int.MaxValue, ErrorMessage = "Employee is required.")]
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "Date is required.")]
    public DateTime? LeaveDate { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Type is required.")]
    public int LeaveType { get; set; }

    //[Range(1, int.MaxValue, ErrorMessage = "Status is required.")]
    public int LeaveStatus { get; set; }
    public string Comment { get; set; }=string.Empty;
}