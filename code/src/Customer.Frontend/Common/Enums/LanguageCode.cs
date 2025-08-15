using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Common.Enums;

public enum LanguageCode
{
    [Display(Name = "English")]
    English = 10,

    [Display(Name = "Arabic")]
    Arabic = 20,

    [Display(Name = "Not Specified")]
    NotSpecified = 0
}