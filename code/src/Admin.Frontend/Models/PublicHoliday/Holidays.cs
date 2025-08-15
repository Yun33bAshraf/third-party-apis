using System.ComponentModel.DataAnnotations;
using IApply.Frontend.Common.CustomAttributes;

namespace IApply.Frontend.Models.PublicHoliday;

public class Holidays
{
    [Display(Name = "ID")]
    [ColumnWidth("w-1")]
    public int HolidayId { get; set; }

    [Display(Name = "Holiday Name")]
    public string HolidayName { get; set; } = string.Empty;

    [IgnoreInTable]
    public bool IsWorkingDay { get; set; }

    [Display(Name = "Working Day")]
    [ColumnWidth("w-1")]
    public string WorkingDayDisplay => IsWorkingDay ? "Yes" : "No";

    [IgnoreInTable]
    public DateTime HolidayDate { get; set; }

    [Display(Name = "Holiday Date")]
    public string HolidayDateFormat => HolidayDate.ToString("dd/MM/yyyy") ?? string.Empty;

    [Display(Name = "Created Date")]
    public string CreatedAt { get; set; }

    [IgnoreInTable]
    public string? UpdatedAt { get; set; }
}