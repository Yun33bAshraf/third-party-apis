using OpenProfileAPI.Application.Common.Interfaces;
using OpenProfileAPI.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace OpenProfileAPI.Web.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    //public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public int Id
    {
        get
        {
            var idString = _httpContextAccessor.HttpContext?.User?.FindFirst("usr")?.Value;

            //var idString = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(idString, out int id))
            {
                return id;
            }
            return 0; // or throw an exception if ID is expected to be valid and non-null
        }
    }

    //public async Task<bool> ValidateUserAccess(int userId, CancellationToken cancellationToken)
    //{
    //    var user = await _dbContext.Users
    //        .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

    //    if (user == null)
    //    {
    //        return false;  // User not found
    //    }

    //    if (user.UserTypeId != (int)UserType.Admin)
    //    {
    //        return false;  // Unauthorized access
    //    }

    //    // Return true if the user is an admin
    //    return true;
    //}
}
