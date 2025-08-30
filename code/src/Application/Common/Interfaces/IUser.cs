using OpenProfileAPI.Application.Common.Models;

namespace OpenProfileAPI.Application.Common.Interfaces;

public interface IUser
{
    int Id { get; }
    //Task<bool> ValidateUserAccess(int userId, CancellationToken cancellationToken);
}
