using System.ComponentModel;

namespace IApply.Frontend.Common.Enums;

public enum UserType
{
    [Description("Admin")]
    Admin = 1,
    [Description("User")]
    User = 2,
    [Description("Guest")]
    Guest = 3
}

