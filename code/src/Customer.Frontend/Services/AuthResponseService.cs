using OpenProfileAPI.Frontend.Models.Auth.Login;
using OpenProfileAPI.Frontend.Models.Rights;

namespace OpenProfileAPI.Frontend.Services
{
    public class AuthResponseService
    {
        public Dictionary<string, LoginResponse> Responses { get; set; } = new();
        public Dictionary<string, List<Right>> Rights { get; set; } = new();
    }
}
