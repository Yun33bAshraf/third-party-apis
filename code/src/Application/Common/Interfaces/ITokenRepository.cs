using OpenProfileAPI.Application.Common.Contracts;
using OpenProfileAPI.Application.Common.Models;
using OpenProfileAPI.Domain.Entities;
using AuthPolicyModel = OpenProfileAPI.Domain.Entities.AuthPolicy;

namespace OpenProfileAPI.Application.Common.Interfaces;
public interface ITokenRepository
{
    Task<LoginAttemptContext> ApplyPolicy(User user, LoginAttempts attempt, AuthPolicyModel policy, string timezone);
    AuthResponse GetAuthResponse(User user, LoginAttemptContext context, AuthPolicyModel policy);
    bool CheckAuthPolicyCompliance(User user, LoginAttempts attempt, AuthPolicyModel policy);
}
