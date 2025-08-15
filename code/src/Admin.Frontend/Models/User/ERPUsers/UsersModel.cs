using System.ComponentModel.DataAnnotations;
using IApply.Frontend.Common.CustomAttributes;

namespace IApply.Frontend.Models.User.ERPUsers;

public class UsersModel
{
    [Display(Name = "ID")]
    [ColumnWidth("w-1")]
    public int UserId { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Mobile Number")]
    public string? MobileNumber { get; set; }

    [IgnoreInTable]
    public int DesignationId { get; set; }

    //[Display(Name = "Designation")]
    //public string? Designation { get; set; }

    [Display(Name = "Address")]
    public string Address { get; set; } = string.Empty;

    [IgnoreInTable]
    public DateTime? DateOfBirth { get; set; }

    [Display(Name = "DOB")]
    public string? DOBFormatted => DateOfBirth?.ToString("dd-MM-yyyy");

    [Display(Name = "Last Login")]
    public DateTime? LastLoginDate { get; set; }

    [Display(Name = "Created Date")]
    public string? CreatedAt { get; set; }

    [IgnoreInTable]
    public int StatusId { get; set; }

    [Display(Name = "Status")]
    public string? Status { get; set; }

    [IgnoreInTable]
    public string? FirstName { get; set; }

    [IgnoreInTable]
    public string? LastName { get; set; }

    [IgnoreInTable]
    [Display(Name = "Land Line Number")]
    public string? LandLineNumber { get; set; }

    [IgnoreInTable]
    [Display(Name = "Permanent Address")]
    public string? PermanentAddress { get; set; }

    [IgnoreInTable]
    public string? UpdatedAt { get; set; }

}