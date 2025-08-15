using ThirdPartyAPIs.Application.Common.Contracts;
using ThirdPartyAPIs.Application.Common.Models;
using ThirdPartyAPIs.Domain.Entities;
using AuthPolicyModel = ThirdPartyAPIs.Domain.Entities.AuthPolicy;

namespace ThirdPartyAPIs.Application.Common.Interfaces;
public interface ITokenRepository
{
    Task<LoginAttemptContext> ApplyPolicy(User user, LoginAttempts attempt, AuthPolicyModel policy, string timezone);
    AuthResponse GetAuthResponse(User user, LoginAttemptContext context, AuthPolicyModel policy);
    bool CheckAuthPolicyCompliance(User user, LoginAttempts attempt, AuthPolicyModel policy);
}
