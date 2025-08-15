using System.ComponentModel;

namespace ThirdPartyAPIs.Domain.Enums;
public enum UserType
{
    [Description("Admin")]
    Admin = 1,
    [Description("Customer")]
    Customer = 2,
}
