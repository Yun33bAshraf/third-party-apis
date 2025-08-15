using System.ComponentModel.DataAnnotations;
using IApply.Frontend.Common.CustomAttributes;

namespace IApply.Frontend.Models.Attendances;

public class Attendance
{
    [IgnoreInTable]
    [ColumnWidth("w-1")]
    [Display(Name = "ID")]
    public int AttendanceLogId { get; set; }

    [Display(Name = "Device ID")]
    public int DeviceId { get; set; }

    [Display(Name = "Employee ID")]
    public int? EmployeeId { get; set; }

    [Display(Name = "Name")]
    public string DisplayEmployeeName => string.IsNullOrWhiteSpace(EmployeeName) ? "Unknown" : EmployeeName;

    [IgnoreInTable]
    public string EmployeeName { get; set; } = string.Empty;

    [Display(Name = "Punch Date Time")]
    public DateTime? PunchDateTime { get; set; }

    [IgnoreInTable]
    [Display(Name = "Punch Time")]
    public string? PunchTime { get; set; }
}
