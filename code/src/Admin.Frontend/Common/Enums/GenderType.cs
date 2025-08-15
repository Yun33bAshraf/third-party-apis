using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Common.Enums;

public enum GenderType
{
    [Display(Name = "Male")]
    Male = 10,

    [Display(Name = "Female")]
    Female = 20,
}