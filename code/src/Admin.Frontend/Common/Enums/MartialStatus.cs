using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Common.Enums
{
    public enum MaritalStatus
    {
        [Display(Name = "Married")]
        Married = 40,

        [Display(Name = "Unmarried")]
        Unmarried = 50,
    }
}
