using System.Security.Claims;

namespace IApply.Frontend.Models;

public record LoggedInUserModel(int Id, string? Name, string? Email, string Token,int? Role)
{
    public Claim[] ToClaims() =>
        [
            new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
            new Claim(ClaimTypes.Name, Name ?? string.Empty),
            new Claim(ClaimTypes.Email, Email ?? string.Empty),
            new Claim(ClaimTypes.Thumbprint, Token),
            new Claim(ClaimTypes.Role, Role.ToString()),
        ];

    public static LoggedInUserModel FromPrincipal(ClaimsPrincipal principal)
    {
        var userIdString = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0";
        int userId = int.TryParse(userIdString, out var parsedId) ? parsedId : 0; // Convert to int safely

        //var userId = principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        var name = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;
        var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
        var accessToken = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Thumbprint)?.Value ?? string.Empty;
        var role = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? string.Empty;
        int roleId = int.TryParse(userIdString, out var roleIds) ? roleIds : 0; // Convert to int safely

        return new LoggedInUserModel(userId, name, email, accessToken, roleId);
    }
}
