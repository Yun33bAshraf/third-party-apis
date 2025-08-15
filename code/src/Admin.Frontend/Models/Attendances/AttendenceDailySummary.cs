using System.ComponentModel.DataAnnotations;
using IApply.Frontend.Common.CustomAttributes;

namespace IApply.Frontend.Models.Attendances
{
    public class AttendenceDailySummary
    {
        [Display(Name = "Name")]
        public string EmployeeName { get; set; } = string.Empty;

        [Display(Name = "Punch Date")]
        public string PunchDateOnly => PunchDate.ToString("dd/MM/yyyy");

        [IgnoreInTable]
        public DateTime PunchDate { get; set; }

        [Display(Name = "Check-In")]
        public string CheckInTime { get; set; } = string.Empty;

        [Display(Name = "Check-Out")]
        public string CheckOutTime { get; set; } = string.Empty;

        [Display(Name = "Punch Count")]
        public int PunchCount { get; set; }

        [Display(Name = "Total Hours")]
        public string TotalHours { get; set; } = string.Empty;

        [Display(Name = "Leave Type")]
        public string LeaveType { get; set; } = string.Empty;
    }
}


