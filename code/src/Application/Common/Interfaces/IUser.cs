using ThirdPartyAPIs.Application.Common.Models;

namespace ThirdPartyAPIs.Application.Common.Interfaces;

public interface IUser
{
    int Id { get; }
    //Task<bool> ValidateUserAccess(int userId, CancellationToken cancellationToken);
}
