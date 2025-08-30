using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OpenProfileAPI.Frontend.Models.Enum
{
    public enum PaymentStatus
    {
        [Description("Paid")]
        Paid = 1,
        [Description("UnPaid")]
        UnPaid,
        [Description("Partial")]
        Partial,
        [Description("Due")]
        Due
    }
}
