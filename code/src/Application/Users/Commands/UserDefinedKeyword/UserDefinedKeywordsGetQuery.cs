//using System.Linq.Expressions;
//using OpenProfileAPI.Application.Common.Interfaces;
//using OpenProfileAPI.Application.Common.Models;
//using OpenProfileAPI.Domain.Entities;

//namespace OpenProfileAPI.Application.Users.Commands.UserDefinedKeyword;
//public class UserDefinedKeywordsGetQuery : IRequest<ResponseBase>
//{
//}

//public class UserDefinedKeywordsGetQueryHandler(
//    IQueryRepository<UserJobPreference> jobPreferenceRepository,
//    IUser currentUser
//) : IRequestHandler<UserDefinedKeywordsGetQuery, ResponseBase>
//{
//    private ResponseBase ErrorResponse(string error) => new()
//    {
//        Status = false,
//        Error = error
//    };

//    public async Task<ResponseBase> Handle(UserDefinedKeywordsGetQuery request, CancellationToken cancellationToken)
//    {
//        Expression<Func<UserJobPreference, bool>> condition = x => x.UserId == currentUser.Id;

//        Expression<Func<UserJobPreference, UserJobPreferenceResponseModel>> projection = x => new UserJobPreferenceResponseModel
//        {
//            PreferenceId = x.Id,
//            UserId = currentUser.Id,
//            Username = $"{x.User.FirstName} {x.User.LastName}",
//            JobTitle = x.JobTitle,
//            IndustryId = x.IndustryId,
//            WorkModeId = x.WorkModeId,
//            SalaryRangeMin = x.SalaryRangeMin,
//            SalaryRangeMax = x.SalaryRangeMax,
//            CompanySize = x.CompanySize,
//            Location = x.Location,
//            JobTypeId = x.JobTypeId,
//            ApplicationLimit = x.ApplicationLimit,
//            FrequencyId = x.FrequencyId,
//        };

//        var data = await jobPreferenceRepository.GetAsync(
//            condition,
//            projection,
//            cancellationToken
//        );

//        if (data == null)
//            return ErrorResponse("No keywords found.");

//        return new ResponseBase
//        {
//            Status = true,
//            Message = "Data fetched successfully.",
//            Data = data is not null ? data : "No data found."
//        };
//    }
//}

//public class UserJobPreferenceResponseModel
//{
//    public int PreferenceId { get; set; }
//    public int UserId { get; set; }
//    public string Username { get; set; } = string.Empty;
//    public string? JobTitle { get; set; } = string.Empty;
//    public int? IndustryId { get; set; } // IT, Finance, etc.
//    public int? WorkModeId { get; set; } // Remote, Hybrid, Onsite
//    public int? SalaryRangeMin { get; set; }
//    public int? SalaryRangeMax { get; set; }
//    public int? CompanySize { get; set; } //  <50, 50-500, >500 employees
//    public string? Location { get; set; } = string.Empty;
//    public int? JobTypeId { get; set; } // Full-time, Part-time, Contract, etc.
//    public int? ApplicationLimit { get; set; } // e.g., 5, 10, 20
//    public int? FrequencyId { get; set; } // Daily or Weekly
//}
