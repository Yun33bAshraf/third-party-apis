using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenProfileAPI.Domain.Entities;

public class UserRight : BaseAuditableEntity
{
    public int UserId { get; set; }
    public int RightId { get; set; }
    public virtual User User { get; set; } = default!;
    public virtual Right Right { get; set; } = default!;
}
