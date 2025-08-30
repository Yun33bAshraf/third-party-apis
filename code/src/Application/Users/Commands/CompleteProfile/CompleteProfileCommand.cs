//using OpenProfileAPI.Application.Common.Interfaces;
//using OpenProfileAPI.Application.Common.Models;
//using OpenProfileAPI.Domain.Common;
//using OpenProfileAPI.Domain.Entities;
//using ExperienceModel = OpenProfileAPI.Domain.Entities.Experience;
//using SkillsModel = OpenProfileAPI.Domain.Entities.Skill;
//using OpenProfileAPI.Domain.Enums;
//using Microsoft.AspNetCore.Identity;

//namespace OpenProfileAPI.Application.Users.Commands.CompleteProfile;
//public class CompleteProfileCommand : IRequest<ResponseBase>
//{
//    public string? Address { get; set; } = string.Empty;
//    public string MobileNumber { get; set; } = string.Empty;
//    public DateOnly? DateOfBirth { get; set; }

//    public int? CityId { get; set; }
//    public int? StateId { get; set; }
//    public int? CountryId { get; set; }
//    public string? PostalCode { get; set; } = string.Empty;

//    public int GenderId { get; set; }
//    public string? NationalId { get; set; }
//    public int? MaritalStatusId { get; set; }
//    public string? Occupation { get; set; }
//    public string? LinkedInProfile { get; set; }
//    public string? UserBio { get; set; }

//    public List<CurrentUserExperience> Experiences { get; set; } = [];
//    public List<CurrentUserEducation> Educations { get; set; } = [];
//    public List<CurrentUserSkills> Skills { get; set; } = [];

//}

//public class CurrentUserExperience
//{
//    public string CompanyName { get; set; } = string.Empty;
//    public string Position { get; set; } = string.Empty;
//    public string Location { get; set; } = string.Empty;
//    public DateTime StartDate { get; set; }
//    public DateTime? EndDate { get; set; }
//    public bool IsCurrent { get; set; }
//}

//public class CurrentUserEducation
//{
//    public string Degree { get; set; } = string.Empty;
//    public string Institution { get; set; } = string.Empty;
//    public string FieldOfStudy { get; set; } = string.Empty;
//    public DateTime StartDate { get; set; }
//    public DateTime? EndDate { get; set; }
//}

//public class CurrentUserSkills
//{
//    public int SkillId { get; set; }
//    public int ProficiencyLevelId { get; set; } // e.g., Beginner, Intermediate, Expert
//}

//public class CompleteProfileCommandHandler(UserManager<User> userManager,
//    IUser currentUser,
//    IApplicationDbContext dbContext
//    ) : IRequestHandler<CompleteProfileCommand, ResponseBase>
//{
//    private ResponseBase ErrorResponse(string error)
//    {
//        return new ResponseBase
//        {
//            Status = false,
//            Error = error
//        };
//    }

//    public async Task<ResponseBase> Handle(CompleteProfileCommand request, CancellationToken cancellationToken)
//    {
//        var user = await userManager.FindByIdAsync(currentUser.Id.ToString());
//        if (user is null)
//            return ErrorResponse(AppMessage.UserNotFound.GetDescription());

//        var userProfile = await dbContext.UserProfile
//            .FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);
//        if (userProfile is null)
//            return ErrorResponse(AppMessage.UserProfileNotFound.GetDescription());

//        // Update user profile details
//        userProfile.Address = request.Address;
//        userProfile.Email = user.Email ?? string.Empty;
//        userProfile.MobileNumber = request.MobileNumber;
//        userProfile.DateOfBirth = request.DateOfBirth;
//        userProfile.CityId = request.CityId;
//        userProfile.StateId = request.StateId;
//        userProfile.CountryId = request.CountryId;
//        userProfile.PostalCode = request.PostalCode;
//        userProfile.GenderId = request.GenderId;
//        userProfile.NationalId = request.NationalId;
//        userProfile.MaritalStatusId = request.MaritalStatusId;
//        userProfile.Occupation = request.Occupation;
//        userProfile.LinkedInProfile = request.LinkedInProfile;
//        userProfile.UserBio = request.UserBio;
//        userProfile.LastModifiedBy = currentUser.Id;
//        userProfile.LastModified = DateTime.UtcNow;

//        dbContext.UserProfile.Update(userProfile);

//        // Add new experiences if provided
//        if (request.Experiences?.Count > 0 == true)
//        {
//            var experiences = request.Experiences.Select(exp => new ExperienceModel
//            {
//                UserId = currentUser.Id,
//                CompanyName = exp.CompanyName,
//                Position = exp.Position,
//                Location = exp.Location,
//                StartDate = exp.StartDate,
//                EndDate = exp.EndDate,
//                IsCurrent = exp.IsCurrent,
//                LastModifiedBy = currentUser.Id,
//                LastModified = DateTime.UtcNow
//            });

//            await dbContext.Experience.AddRangeAsync(experiences, cancellationToken);
//        }

//        // Add new educations if provided
//        if (request.Educations?.Count > 0 == true)
//        {
//            var educations = request.Educations.Select(edu => new Education
//            {
//                UserId = currentUser.Id,
//                Degree = edu.Degree,
//                Institution = edu.Institution,
//                FieldOfStudy = edu.FieldOfStudy,
//                StartDate = edu.StartDate,
//                EndDate = edu.EndDate,
//                LastModifiedBy = currentUser.Id,
//                LastModified = DateTime.UtcNow
//            });

//            await dbContext.Education.AddRangeAsync(educations, cancellationToken);
//        }

//        // Add new skills if provided
//        if (request.Skills?.Count > 0 == true)
//        {
//            var skills = request.Skills.Select(skill => new SkillsModel
//            {
//                UserId = currentUser.Id,
//                SkillId = skill.SkillId,
//                ProficiencyLevelId = skill.ProficiencyLevelId,
//                LastModifiedBy = currentUser.Id,
//                LastModified = DateTime.UtcNow
//            });

//            await dbContext.Skill.AddRangeAsync(skills, cancellationToken);
//        }

//        await dbContext.SaveChangesAsync(cancellationToken);

//        // Check if profile is complete after all inserts
//        bool hasExperience = await dbContext.Experience.AnyAsync(e => e.UserId == currentUser.Id, cancellationToken);
//        bool hasEducation = await dbContext.Education.AnyAsync(e => e.UserId == currentUser.Id, cancellationToken);
//        bool hasSkills = await dbContext.Skill.AnyAsync(s => s.UserId == currentUser.Id, cancellationToken);

//        user.IsProfileCompleted = hasExperience && hasEducation && hasSkills;
//        await userManager.UpdateAsync(user);

//        return new ResponseBase
//        {
//            Status = true,
//            Data = new
//            {
//                UserId = currentUser.Id,
//                user.IsProfileCompleted,
//            },
//            Message = AppMessage.ProfileUpdatedSuccessfully.GetDescription()
//        };
//    }
//}
