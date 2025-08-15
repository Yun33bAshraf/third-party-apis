using IApply.Frontend.Models.Auth.Login;
using IApply.Frontend.Models.Rights;

namespace IApply.Frontend.Services
{
    public class AuthResponseService
    {
        public Dictionary<string, LoginResponse> Responses { get; set; } = new();
        public Dictionary<string, List<Right>> Rights { get; set; } = new();
    }
}
