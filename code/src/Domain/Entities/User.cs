using Microsoft.AspNetCore.Identity;

namespace OpenProfileAPI.Domain.Entities;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? LastLoginDate { get; set; }
    public DateTime? PasswordChangedAt { get; set; }
    public int UserTypeId { get; set; }
    public string AuthKey { get; set; } = string.Empty;
    public string SmsKey { get; set; } = string.Empty;
    public string EmailKey { get; set; } = string.Empty;
    public bool IsProfileCompleted { get; set; }
    public int? LastLoginAttempt { get; set; }

    // Navigation properties
    public virtual UserProfile UserProfile { get; set; } = default!;
    public virtual ICollection<UserRight>? UserRights { get; set; } = [];
    public virtual ICollection<UserRole>? UserRoles { get; set; } = [];
    public virtual ICollection<UserFile>? UserFiles { get; set; } = [];
    public virtual ICollection<Skill>? Skills { get; set; } = []; 

    #region BASE ENTITY
    public DateTimeOffset Created { get; set; }
    public int? CreatedBy { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public int? LastModifiedBy { get; set; }
    #endregion
}
