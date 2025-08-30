namespace OpenProfileAPI.Frontend.Models.Assets;

public class AssetsRequest
{
    public int AssetId { get; set; }
    public string AssetName { get; set; } = string.Empty;
    public int? CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public int? StatusId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
