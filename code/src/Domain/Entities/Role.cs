﻿using System.ComponentModel.DataAnnotations;

namespace ThirdPartyAPIs.Domain.Entities;

public class Role : BaseAuditableEntity
{
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }
    //public int TypeId { get; set; }

    public virtual ICollection<RoleRight> RoleRights { get; set; } = new List<RoleRight>();
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
