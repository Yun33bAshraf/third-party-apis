using OpenProfileAPI.Domain.Entities;

namespace OpenProfileAPI.Application.Common.Models;
public class LoginAttemptContext
{
    public LoginAttempts Attempt { get; set; } = null!;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
}
