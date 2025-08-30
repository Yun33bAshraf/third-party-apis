//using OpenProfileAPI.Application.Common.Interfaces;
//using OpenProfileAPI.Application.Common.Models;
//using OpenProfileAPI.Domain.Common;
//using OpenProfileAPI.Domain.Entities;
//using OpenProfileAPI.Domain.Enums;
//using Microsoft.AspNetCore.SignalR;

//namespace OpenProfileAPI.Application.Users.Queries.ProfileGet;
//public class ProfileGetQuery : IRequest<ResponseBase>
//{
//}

//public class ProfileGetQueryHandler(IQueryRepository<UserProfile> userProfileRepo,
//    IUser currentUser
//    ) : IRequestHandler<ProfileGetQuery, ResponseBase>
//{
//    private ResponseBase ErrorResponse(string error)
//    {
//        return new ResponseBase
//        {
//            Status = false,
//            Error = error
//        };
//    }

//    public async Task<ResponseBase> Handle(ProfileGetQuery request, CancellationToken cancellationToken)
//    {
//        var profile = await userProfileRepo.GetSingleWithIncludesAsync(
//            condition: x => x.UserId == currentUser.Id,
//            include: query => query
//                .Include(x => x.User)
//                    .ThenInclude(u => u.Experiences)
//                .Include(x => x.User)
//                    .ThenInclude(u => u.Educations)
//                .Include(x => x.User)
//                    .ThenInclude(u => u.Skills)
//                .Include(x => x.City)
//                .Include(x => x.State)
//                .Include(x => x.Country)
//                .Include(x => x.Gender)
//                .Include(x => x.MaritalStatus)
//                .Include(x => x.User)
//                    .ThenInclude(u => u.Subscription)
//                    .ThenInclude(s => s!.SubscriptionPlan),
//            cancellationToken: cancellationToken
//        );

//        if (profile == null || profile.User == null)
//            return ErrorResponse(AppMessage.UserNotFound.GetDescription());

//        // Local variable to simplify and avoid multiple calls
//        var sub = profile.User.Subscription;


//        var response = new ProfileGetResponseModel
//        {
//            UserId = profile.User.Id,
//            UserProfileId = profile.Id,
//            DisplayName = profile.DisplayName,
//            FirstName = profile.User.FirstName,
//            LastName = profile.User.LastName,
//            LastLoginDate = profile.User.LastLoginDate,
//            UserTypeId = profile.User.UserTypeId,
//            UserType = ((UserType)profile.User.UserTypeId).GetDescription() ?? string.Empty,
//            Address = profile.Address,
//            Email = profile.Email,
//            MobileNumber = profile.MobileNumber,
//            DateOfBirth = profile.DateOfBirth,
//            CityId = profile.CityId,
//            City = profile.City?.Name,
//            StateId = profile.StateId,
//            State = profile.State?.Name,
//            CountryId = profile.CountryId,
//            Country = profile.Country?.Name,
//            PostalCode = profile.PostalCode,
//            GenderId = profile.GenderId,
//            Gender = profile.Gender?.Name,
//            NationalId = profile.NationalId,
//            MaritalStatusId = profile.MaritalStatusId,
//            MaritalStatus = profile.MaritalStatus?.Name,
//            Occupation = profile.Occupation,
//            LinkedInProfile = profile.LinkedInProfile,
//            UserBio = profile.UserBio,
//            IsProfileCompleted = profile.User.IsProfileCompleted,

//            Experiences = profile.User.Experiences?.Select(e => new GetCurrentUserExperience
//            {
//                CompanyName = e.CompanyName,
//                Position = e.Position,
//                Location = e.Location,
//                StartDate = e.StartDate,
//                EndDate = e.EndDate,
//                IsCurrent = e.IsCurrent,
//                CurrentlyEmployed = e.IsCurrent ? "Yes" : "No"
//            }).ToList() ?? [],

//            Educations = profile.User.Educations?.Select(ed => new GetCurrentUserEducation
//            {
//                Degree = ed.Degree,
//                Institution = ed.Institution,
//                FieldOfStudy = ed.FieldOfStudy,
//                StartDate = ed.StartDate,
//                EndDate = ed.EndDate
//            }).ToList() ?? [],

//            Skills = profile.User.Skills?.Select(s => new GetCurrentUserSkills
//            {
//                SkillId = s.Id,
//                ProficiencyLevelId = s.ProficiencyLevelId,
//                ProficiencyLevel = ((ProficiencyLevel)s.ProficiencyLevelId).GetDescription() ?? string.Empty
//            }).ToList() ?? [],



//            Subscription = MapSubscription(sub, profile.User.Id)
//        };

//        return new ResponseBase
//        {
//            Status = true,
//            Data = response,
//            Message = response is not null ? AppMessage.DataFetchedSuccessfully.GetDescription() : AppMessage.NoDataFound.GetDescription(),
//        };
//    }

//    private MySubscription? MapSubscription(Subscription? sub, int userId)
//    {
//        if (sub == null) return null;

//        var plan = sub.SubscriptionPlan;

//        return new MySubscription
//        {
//            UserId = userId,
//            SubscriptionId = sub.Id,
//            SubscriptionPlanId = sub.SubscriptionPlanId,
//            Name = plan?.Name ?? string.Empty,
//            MonthlyPrice = plan?.MonthlyPrice ?? 0,
//            MaxApplicationsPerDay = plan?.MaxApplicationsPerDay ?? 0,
//            MaxApplicationsPerMonth = plan?.MaxApplicationsPerMonth ?? 0,
//            StartedAt = sub.StartedAt,
//            RenewedAt = sub.RenewedAt,
//            CanceledAt = sub.CanceledAt
//        };
//    }
//}

//public class ProfileGetResponseModel
//{
//    public int UserId { get; set; }
//    public int UserProfileId { get; set; }
//    public string DisplayName { get; set; } = string.Empty;
//    public string FirstName { get; set; } = string.Empty;
//    public string LastName { get; set; } = string.Empty;
//    public string? Address { get; set; } = string.Empty;
//    public string Email { get; set; } = string.Empty;
//    public string MobileNumber { get; set; } = string.Empty;
//    public DateOnly? DateOfBirth { get; set; }
//    public DateTime? LastLoginDate { get; set; }
//    public int UserTypeId { get; set; }
//    public string UserType { get; set; } = string.Empty;
//    public bool IsProfileCompleted { get; set; }
//    public int? CityId { get; set; }
//    public string? City { get; set; } = string.Empty;
//    public int? StateId { get; set; }
//    public string? State { get; set; } = string.Empty;
//    public int? CountryId { get; set; }
//    public string? Country { get; set; } = string.Empty;
//    public string? PostalCode { get; set; } = string.Empty;
//    public int? GenderId { get; set; }
//    public string? Gender { get; set; } = string.Empty;
//    public string? NationalId { get; set; }
//    public int? MaritalStatusId { get; set; }
//    public string? MaritalStatus { get; set; } = string.Empty;
//    public string? Occupation { get; set; }
//    public string? LinkedInProfile { get; set; }
//    public string? UserBio { get; set; }
//    public List<GetCurrentUserExperience> Experiences { get; set; } = [];
//    public List<GetCurrentUserEducation> Educations { get; set; } = [];
//    public List<GetCurrentUserSkills> Skills { get; set; } = [];
//    public MySubscription? Subscription { get; set; } = null;
//}

//public class GetCurrentUserExperience
//{
//    public string CompanyName { get; set; } = string.Empty;
//    public string Position { get; set; } = string.Empty;
//    public string Location { get; set; } = string.Empty;
//    public DateTime StartDate { get; set; }
//    public DateTime? EndDate { get; set; }
//    public bool IsCurrent { get; set; }
//    public string CurrentlyEmployed { get; set; } = string.Empty; // e.g., "Yes" or "No"
//}

//public class GetCurrentUserEducation
//{
//    public string Degree { get; set; } = string.Empty;
//    public string Institution { get; set; } = string.Empty;
//    public string FieldOfStudy { get; set; } = string.Empty;
//    public DateTime StartDate { get; set; }
//    public DateTime? EndDate { get; set; }
//}

//public class GetCurrentUserSkills
//{
//    public int SkillId { get; set; }
//    public int ProficiencyLevelId { get; set; } // e.g., Beginner, Intermediate, Expert
//    public string ProficiencyLevel{ get; set; } = string.Empty;
//}

//public class MySubscription
//{
//    public int UserId { get; set; }
//    public int SubscriptionId { get; set; }
//    public int SubscriptionPlanId { get; set; }
//    public string Name { get; set; } = string.Empty;
//    public decimal MonthlyPrice { get; set; } // = 10.99m;
//    public int MaxApplicationsPerDay { get; set; } // = 30;
//    public int MaxApplicationsPerMonth { get; set; } // = 1000;
//    public DateTime StartedAt { get; set; }
//    public DateTime? RenewedAt { get; set; }
//    public DateTime? CanceledAt { get; set; }
//    public bool IsActive => CanceledAt == null && StartedAt <= DateTime.UtcNow;

//}
