using System.ComponentModel.DataAnnotations;
using IApply.Frontend.Common.CustomAttributes;

namespace OpenProfileAPI.Frontend.Models.Settings.Configurations;

public class CategoryGetResponse
{
    [Display(Name = "ID")]
    public int CategoryId { get; set; }
    
    [Display(Name = "Name")]
    public string CategoryName { get; set; } = string.Empty;

    [IgnoreInTable]
    public int? ParentCategoryId { get; set; }
    
    [Display(Name = "Parent Category")]
    public string ParentCategoryName { get; set; } = string.Empty;
    
    [IgnoreInTable]
    public int EntityTypeId { get; set; }
    
    [Display(Name = "Entity Type")]
    public string EntityTypeName { get; set; } = string.Empty;

    [Display(Name = "Created Date")]
    public string CreatedAt { get; set; } = string.Empty;

    [IgnoreInTable]
    [Display(Name = "Updated Date")]
    public string? UpdatedAt { get; set; } = string.Empty;
}
