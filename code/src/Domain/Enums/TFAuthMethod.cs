using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenProfileAPI.Domain.Enums;
public enum TFAuthMethod
{
    None = 0,
    Authenticator = 2001,
    SMS = 2002,
    Email = 2003
}
