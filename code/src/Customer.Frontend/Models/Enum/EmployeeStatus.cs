using System.ComponentModel;

namespace OpenProfileAPI.Frontend.Models.Enum
{
    public enum EmployeeStatus
    {
        [Description("Active")]
        Active = 1,
        [Description("In-Active")]
        InActive,
        //[Description("On Leave")]
        //OnLeave
    }
}
