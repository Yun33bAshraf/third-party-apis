namespace IApply.Frontend.Models.Employee
{
    public class GetCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int? ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; } = string.Empty;
        public int EntityTypeId { get; set; }
        public string EntityTypeName { get; set; } = string.Empty;
    }
}
