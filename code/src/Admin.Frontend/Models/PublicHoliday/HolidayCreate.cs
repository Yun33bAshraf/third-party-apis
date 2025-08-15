using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.PublicHoliday
{
    public class HolidayCreate
    {
        public int HolidayId { get; set; }

        [Required(ErrorMessage = "Holiday Date is required")]
        [DataType(DataType.Date)]
        public DateTime? HolidayDate { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        [RegularExpression(@"^[\p{L}\p{N} _-]+$", ErrorMessage = "Invalid asset name.")]
        public string HolidayName { get; set; } = string.Empty;
        public bool IsWorkingDay { get; set; } 
    }
}

