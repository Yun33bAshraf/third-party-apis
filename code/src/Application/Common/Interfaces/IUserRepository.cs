using OpenProfileAPI.Application.Common.Models;
namespace OpenProfileAPI.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetUsers(int pageNo, int pageSize);
}
