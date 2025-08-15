using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Common.Enums;

public enum BackendActivationStatus
{
    [Display(Name = "None")]
    None = 0,

    [Display(Name = "Pending")]
    Pending = 10,

    [Display(Name = "Temporary Activated")]
    TemporaryActivated = 20,

    [Display(Name = "Fully Activated")]
    FullyActivated = 30,

    [Display(Name = "Rejected")]
    Rejected = 40,

}