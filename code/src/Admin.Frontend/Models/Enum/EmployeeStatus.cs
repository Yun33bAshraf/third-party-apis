using System.ComponentModel;

namespace IApply.Frontend.Models.Enum
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
