using IApply.Frontend.Common.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.Cities
{
    public class City
    {
        [IgnoreInTable]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Country can only contain letters and spaces.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Region is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Region can only contain letters and spaces.")]
        public string Region { get; set; }

        [Display(Name = "Currency Code")]
        [Required(ErrorMessage = "Currency Code is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Currency Code can only contain letters and spaces.")]
        public string CurrencyCode { get; set; }

        [Display(Name = "Timezone Offset")]
        [Required(ErrorMessage = "Timezone Offset is required.")]
        [Range(-12, 14, ErrorMessage = "Timezone Offset must be between -12 and 14.")]
        public int TimeZoneOffset { get; set; }

        [Display(Name = "Tax Rate")]
        [Required(ErrorMessage = "Tax Rate is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Tax Rate must be 0 or higher.")]
        public int TaxRate { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Status is required.")]
        public bool IsActive { get; set; }
    }
}
