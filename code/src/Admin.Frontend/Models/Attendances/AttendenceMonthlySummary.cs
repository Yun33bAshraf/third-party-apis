using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;
using IApply.Frontend.Common.CustomAttributes;

namespace IApply.Frontend.Models.Attendances
{
    public class AttendenceMonthlySummary
    {
        [Display(Name = "ID")]
        [ColumnWidth("w-1")]
        public int EmployeeId { get; set; }

        [Display(Name = "Name")]
        public string Name => $"{FirstName} {LastName}".Trim();

        [IgnoreInTable]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [IgnoreInTable]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Working Days")]
        public int WorkingDays { get; set; }

        [Display(Name = "Present Days")]
        public int PresentDays { get; set; }

        [Display(Name = "Hours Worked")]
        public string HoursWorked { get; set; } = string.Empty;

        [Display(Name = "Allowed Hours")]
        public string AllowedHours { get; set; } = string.Empty;

        [Display(Name = "Allowed Hours After Leaves")]
        public string AllowedHoursAfterLeaves { get; set; } = string.Empty;

        [Display(Name = "Total Hours")]
        public int TotalHours { get; set; }

        [Display(Name = "Hours Difference")]
        public string HoursDifference { get; set; } = string.Empty;

        [Display(Name = "Leave Days")]
        public int LeaveDays { get; set; }
    }
}
