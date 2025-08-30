using System.ComponentModel;

namespace OpenProfileAPI.Frontend.Models.Enum
{
    public enum MaritalStatus
    {
        [Description("Single")]
        Single = 1,
        [Description("Married")]
        Married = 2,
        [Description("Divorced")]
        Divorced = 3,
        [Description("Widowed")]
        Widowed = 4
    }
}
