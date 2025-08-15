using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IApply.Frontend.Models.Subscription;


public class CreateSubscriptionRequest
{
    [Display(Name = "Subscription ID")]
    public int Id { get; set; }

    [Display(Name = "Subscription Name")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "DurationInDays")]
    public int DurationInDays { get; set; }

    [Display(Name = "Price")]
    [Column(TypeName = "decimal(20,2)")]
    public decimal Price { get; set; }

    [Display(Name = "Status")]
    public bool Status { get; set; } = true;

    [Display(Name = "Description")]
    public string? Description { get; set; }
}


