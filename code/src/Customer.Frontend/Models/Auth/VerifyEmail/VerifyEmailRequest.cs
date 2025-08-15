using System.ComponentModel.DataAnnotations;

namespace IApply.Frontend.Models.Auth.VerifyEmail;

public class VerifyEmailRequest
{
    public int UserId { get; set; }

    [Required]
    public string Token { get; set; } = string.Empty;
}
