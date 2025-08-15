using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Common.Enums;

public enum RidePreference
{
    [Display(Name = "Male")]
    Male = 10,

    [Display(Name = "Female")]
    Female = 20,

    [Display(Name = "None")]
    None = 0,

}