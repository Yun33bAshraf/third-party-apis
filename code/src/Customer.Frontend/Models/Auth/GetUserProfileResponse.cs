using System.ComponentModel.DataAnnotations;
using OpenProfileAPI.Frontend.Models;
namespace OpenProfileAPI.Frontend.Models.Auth;

public class GetUserProfileResponse : BaseResponse<GetUsersProfile>
{
}
public class GetUsersProfile
{

    [Display(Name = "User Id")]
    public int UserId { get; set; }

    [Display(Name = "Display Name")]
    public string? DisplayName { get; set; }

    [Display(Name = "Address")]
    public string? Address { get; set; }

    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Display(Name = "Phone")]
    public string? Phone { get; set; }   
    
    [Display(Name = "City")]
    public string? City { get; set; }
}
