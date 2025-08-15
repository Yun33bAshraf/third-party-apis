using System.ComponentModel.DataAnnotations;
using IApply.Frontend.Common.CustomAttributes;

namespace IApply.Frontend.Models.EmployeeLeave
{
    public class EmployeeLeave
    {
        [Display(Name = "Id")]
        [ColumnWidth("w-1")]
        public int EmployeeLeaveId { get; set; }

        [IgnoreInTable]
        public int EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string? EmployeeName { get; set; }

        [IgnoreInTable]
        public int LeaveTypeId { get; set; }

        [Display(Name = "Leave Status")]
        [ColumnWidth("w-1")]
        public string? LeaveStatus { get; set; } = null;


        [Display(Name = "Leave Type")]
        [ColumnWidth("w-1")]
        public string? LeaveType { get; set; } = null;

        [IgnoreInTable]
        public int LeaveStatusId { get; set; }

        [IgnoreInTable]
        public DateTime LeaveDate { get; set; }
        
        [IgnoreInTable]
        public string? Comment { get; set; }=string.Empty;

        [Display(Name = "Leave Date")]
        public string LeaveDateFormat => LeaveDate.ToString("dd/MM/yyyy") ?? string.Empty;

        [Display(Name = "Created Date")]
        public string? CreatedAt { get; set; } = string.Empty;
        
        [IgnoreInTable]
        public string? UpdatedAt { get; set; } = string.Empty;
    }
}
