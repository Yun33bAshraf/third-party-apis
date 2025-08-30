using System.Linq.Expressions;
using OpenProfileAPI.Application.Common.Interfaces;
using OpenProfileAPI.Application.Common.Models;
using OpenProfileAPI.Domain.Common;
using OpenProfileAPI.Domain.Entities;
using OpenProfileAPI.Domain.Enums;

namespace OpenProfileAPI.Application.Users.Queries.GetUsersWithPagination;

public record GetUsersWithPaginationQuery : IRequest<ResponseBase>
{
    public int UserId { get; set; }
    public int UserTypeId { get; set; } = (int)UserType.Customer;
    public string? DisplayName { get; set; }
    public int? CountryId { get; set; } = (int)Country.Pakistan;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetUsersWithPaginationQueryHandler(
        IQueryRepository<UserProfile> userProfileRepo
        //IUser currentUser
    ) : IRequestHandler<GetUsersWithPaginationQuery, ResponseBase>
{
    private ResponseBase ErrorResponse(string error)
    {
        return new ResponseBase
        {
            Status = false,
            Error = error
        };
    }

    public async Task<ResponseBase> Handle(GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        //bool isValid = await currentUser.ValidateUserAccess(currentUser.Id, cancellationToken);
        //if (!isValid)
        //    return ErrorResponse(AppMessage.UnauthorizedAccess.GetDescription());

        //TODO: user id filter is not working.

        Expression<Func<UserProfile, bool>> filter = x =>
            x.UserId > request.UserId &&
            x.User.UserTypeId == request.UserTypeId &&
            (!request.CountryId.HasValue || x.CountryId == request.CountryId.Value) &&
            (string.IsNullOrEmpty(request.DisplayName) || x.DisplayName.Contains(request.DisplayName));

        Func<IQueryable<UserProfile>, IQueryable<UserProfile>> include = query =>
            query.Include(x => x.User)
                 .Include(x => x.City)
                 .Include(x => x.Country)
                 .Include(x => x.Gender);

        int totalRecords = await userProfileRepo.CountAsync(filter, cancellationToken);

        var userProfiles = await userProfileRepo.GetAllWithIncludesAsync(
            filter,
            include,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        var responseData = userProfiles.Select(x => new UserProfileResponse
        {
            UserId = x.UserId,
            UserProfileId = x.Id,
            DisplayName = x.DisplayName,
            FirstName = x.User.FirstName ?? string.Empty,
            LastName = x.User.LastName ?? string.Empty,
            Address = x.Address ?? string.Empty,
            Email = x.Email,
            MobileNumber = x.MobileNumber,
            GenderId = x.GenderId ?? 0,
            Gender = x.Gender?.Name ?? string.Empty,
            CityId = x.CityId ?? 0,
            City = x.City?.Name ?? string.Empty,
            CountryId = x.CountryId ?? 0,
            Country = x.Country?.Name ?? string.Empty,
            DateOfBirth = x.DateOfBirth ?? default,
            LastLoginDate = x.User.LastLoginDate ?? default,
            Created = x.Created,
        }).ToList();

        return new ResponseBase
        {
            Status = true,
            Data = responseData,
            Pagination = new Pagination
            {
                TotalRecords = totalRecords,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            }
        };
    }
}

public class UserProfileResponse
{
    public int UserId { get; set; }
    public int UserProfileId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public int GenderId { get; set; }
    public string? Gender { get; set; } = string.Empty;
    public int CityId { get; set; }
    public string? City { get; set; } = string.Empty; 
    public int CountryId { get; set; }
    public string? Country { get; set; } = string.Empty; 
    public DateTime LastLoginDate { get; set; }
    public DateTimeOffset Created { get; set; }
}
