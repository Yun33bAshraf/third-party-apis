namespace OpenProfileAPI.Frontend.Models.Settings.Configurations;

public class CategoryGetRequest
{
    public int EntityTypeId { get; set; }
    public int ParentCategoryId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
