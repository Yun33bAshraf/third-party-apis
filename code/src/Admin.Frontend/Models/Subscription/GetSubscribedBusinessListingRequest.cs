using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.Subscription;

public class GetSubscribedBusinessListingRequest
{
    [Required]
    public int PageNo { get; set; } = 0;

    [Required]
    public int PageSize { get; set; } = 0;
    public int? BusinessId { get; set; }
    public int? SubscriptionId { get; set; }

}
