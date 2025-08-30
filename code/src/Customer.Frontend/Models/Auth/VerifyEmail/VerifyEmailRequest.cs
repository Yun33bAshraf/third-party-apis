using System.ComponentModel.DataAnnotations;

namespace OpenProfileAPI.Frontend.Models.Auth.VerifyEmail;

public class VerifyEmailRequest
{
    public int UserId { get; set; }

    [Required]
    public string Token { get; set; } = string.Empty;
}
