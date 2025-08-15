using IApply.Frontend.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IApply.Frontend.Models.Subscription;


public class CreateUpdateBusinessSubscriptionRequest
{
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Display(Name = "Business Id")]
    public int BusinessId { get; set; }

    [Display(Name = "Business Name")]
    public string? BusinessName { get; set; }

    [Display(Name = "Subscription Id")]
    public int SubscriptionId { get; set; }
    [Display(Name = "Subscription Name")]
    public string? SubscriptionName { get; set; }
    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }
    [Display(Name = "End Date")]
    public DateTime EndDate { get; set; }
    [Display(Name = "Paid Amount")]
    public decimal PaidAmount { get; set; }
    [Display(Name = "Payment Method")]
    public string PaymentMethod { get; set; } = string.Empty;
    [Display(Name = "Payment Status")]
    public PaymentStatus PaymentStatus { get; set; }

    [Display(Name = "Active")]
    public bool IsActive { get; set; }
}


