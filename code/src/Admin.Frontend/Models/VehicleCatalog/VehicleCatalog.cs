using IApply.Frontend.Common.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.VehicleCatalog
{
    public class VehicleCatalog
    {
        [IgnoreInTable]
        public Guid Id { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Make can only contain letters and spaces.")]
        [Required(ErrorMessage = "Make is required.")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; set; }

        [Display(Name = "Minimum Year")]
        [Required(ErrorMessage = "MinimumYear is required.")]
        [Range(1886, 9999, ErrorMessage = "MinimumYear must be a valid year.")]
        [CurrentYear]
        public int MinimumYear { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [Display(Name = "Status")]
        public bool IsActive { get; set; }
    }

    public class CurrentYearAttribute : ValidationAttribute
    {
        public CurrentYearAttribute()
        {
            ErrorMessage = $"The year cannot be greater than the current year.";
        }

        public CurrentYearAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public override bool IsValid(object value)
        {
            if (value is int year)
            {
                return year <= DateTime.Now.Year;
            }

            return true;
        }
    }

}
