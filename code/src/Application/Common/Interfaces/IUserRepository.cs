using ThirdPartyAPIs.Application.Common.Models;
namespace ThirdPartyAPIs.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetUsers(int pageNo, int pageSize);
}
