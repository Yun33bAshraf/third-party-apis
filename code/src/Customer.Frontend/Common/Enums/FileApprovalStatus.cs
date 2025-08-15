using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Common.Enums;

public enum FileApprovalStatus
{
    [Display(Name = "Pending")]
    Pending = 0,

    [Display(Name = "Approved")]
    Approved = 10,

    [Display(Name = "Rejected")]
    Rejected = 20,

}