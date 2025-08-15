using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.Business;

public class GetBusinessListingRequest
{
    [Required]
    public int PageNo { get; set; } = 0;

    [Required]
    public int PageSize { get; set; } = 0;
    public int BusinessId { get; set; } = 0;

}
