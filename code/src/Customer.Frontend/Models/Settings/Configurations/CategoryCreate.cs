
using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.Settings.Configurations;
public class CategoryCreate
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Entity Type is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Entity Type must be selected.")]
    public int EntityTypeId { get; set; }

    //[Required(ErrorMessage = "Category Type is required.")]
    //[Range(1, int.MaxValue, ErrorMessage = "Category Type must be selected.")]
    public int? ParentCategoryId { get; set; }
}

