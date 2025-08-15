using IApply.Frontend.Common.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.VehicleTypes
{
    public class VehicleType
    {
        [IgnoreInTable]
        public Guid Id { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name can only contain letters and spaces.")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [Display(Name = "Status")]
        public bool IsActive { get; set; }
    }
}
