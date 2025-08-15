using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThirdPartyAPIs.Domain.Entities;

public class EntityType : BaseAuditableEntity
{
    [StringLength(200)]
    public string? Name { get; set; }
    public bool IsActive { get; set; }
    public int? EntityTypeParentId { get; set; }
}
