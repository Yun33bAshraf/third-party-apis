using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.Subscription;

public class GetSubscriptionListingRequest
{
    [Required]
    public int PageNo { get; set; } = 0;

    [Required]
    public int PageSize { get; set; } = 0;
    public int SubscriptionId { get; set; } = 0;

}
