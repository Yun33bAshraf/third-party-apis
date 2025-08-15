using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.Auth.Login;

public class LoginRequest
{
    [Required]
    [Display(Name = "Username")]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Password")]
    [MinLength(8)]
    public string Password { get; set; }

    //public int UserType { get; set; }

    [StringLength(50)]
    public string? IpAddress { get; set; }

    [StringLength(100)]
    public string? ClientInformation { get; set; }

    [StringLength(50)]
    public string? CurrentBrowserTimeZone { get; set; }

    //public bool IsSuccess { get; set; } = false;
}
