using OpenProfileAPI.Application.Common.Models;

namespace OpenProfileAPI.Application.Common.Contracts;
public class AuthResponse
{
    public bool IsSuccess { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int UserTypeId { get; set; }
    public int SessionId { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public bool EnforceEmailConfirmation { get; set; }
    public bool EnforceMobileConfirmation { get; set; }
    public bool Enforce2FactorConfiguration { get; set; }
    public bool Enforce2FactorVerification { get; set; }
    public bool EnforcePasswordChangeOnFirstLogin { get; set; }
    public bool EnforceProfileCompletion { get; set; }
    public bool IsProfileCompleted { get; set; }
    public int RoleId { get; set; }
    public List<int> Rights { get; set; } = [];
}

