using IApply.Frontend.Common.CustomAttributes;
using System.ComponentModel.DataAnnotations;
using IApply.Frontend.Common.Enums;

namespace IApply.Frontend.Models.RequiredDocuments
{
    public class RequiredDocument
    {
        [IgnoreInTable]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [RegularExpression(@"^(?=.*[a-zA-Z]).+$", ErrorMessage = "The Name must contain at least one alphabet character.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        [Display(Name = "Entity Type")]
        [Required(ErrorMessage = "The EntityType field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The EntityType field is required.")]
        public EntityType EntityType { get; set; }

        [Display(Name = "Required")]
        [BoolDisplay("Yes", "No")]
        public bool IsRequired { get; set; } = false;

        [Display(Name = "Expiry Required")]
        [BoolDisplay("Yes", "No")]
        public bool IsExpiryRequired { get; set; } = false;

        [Display(Name = "Status")]
        public bool IsActive { get; set; } = false;
        [IgnoreInTable]
        public bool AllowModification { get; set; } = false;

    }
}
