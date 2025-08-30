namespace OpenProfileAPI.Domain.Entities;
public class UserFile : BaseAuditableEntity
{
    public int UserId { get; set; }
    public int FileStoreId { get; set; }
    public int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }

    public virtual User User { get; set; } = default!;
    public virtual FileStore FileStore { get; set; } = default!;
}
