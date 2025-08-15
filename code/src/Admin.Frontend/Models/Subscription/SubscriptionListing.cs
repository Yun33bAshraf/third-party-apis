using IApply.Frontend.Common.CustomAttributes;
using IApply.Frontend.Models.Business;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IApply.Frontend.Models.Subscription;


public class SubscriptionListing
{
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "DurationInDays")]
    public int DurationInDays { get; set; }

    [Display(Name = "Price")]
    [Column(TypeName = "decimal(20,2)")]
    public decimal Price { get; set; }

    [Display(Name = "Status")]
    public bool Status { get; set; }

    [Display(Name = "Description")]
    public string? Description { get; set; }

    [IgnoreInTable]
    [Display(Name = "Created By")]
    public int CreatedBy { get; set; }

    [IgnoreInTable]
    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; }

    [IgnoreInTable]
    [Display(Name = "Modified By")]
    public int ModifiedBy { get; set; }

    [IgnoreInTable]
    [Display(Name = "Modified At")]
    public DateTime ModifiedAt { get; set; }

    [IgnoreInTable]
    [Display(Name = "Comment")]
    public string? Comment { get; set; }
}
