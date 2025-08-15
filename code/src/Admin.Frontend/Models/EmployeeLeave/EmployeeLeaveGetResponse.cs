using System.ComponentModel.DataAnnotations;
using IApply.Frontend.Common.CustomAttributes;
using IApply.Frontend.Common.Enums;
using IApply.Frontend.Common.Utilities;

namespace IApply.Frontend.Models.EmployeeLeave;

public class EmployeeLeaveGetResponse
{
    [Display(Name = "Id")]
    public int EmployeeLeaveId { get; set; }

    [Display(Name = "Employee Id")]
    public int EmployeeId { get; set; }

    [Display(Name = "Name")]
    public string EmployeeName { get; set; } = string.Empty;

    [IgnoreInTable]
    public int LeaveTypeId { get; set; }

    [Display(Name = "Leave Type")]
    public string LeaveType { get; set; } = string.Empty;
    //{
    //    get => Enum.IsDefined(typeof(LeaveType), LeaveTypeId)
    //        ? ((LeaveType)LeaveTypeId).GetDescription()
    //        : LeaveType.Other.GetDescription();
    //}

    [IgnoreInTable]
    public int LeaveStatusId { get; set; }

    [Display(Name = "Leave Status")]
    public string LeaveStatus { get; set; } = string.Empty;

    [IgnoreInTable]
    public DateTime LeaveDate { get; set; }

    [Display(Name = "Leave Date")]
    public string LeaveDateFormatted => LeaveDate.ToString("yyyy-MM-dd");

}
