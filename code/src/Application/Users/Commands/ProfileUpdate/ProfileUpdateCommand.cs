//using OpenProfileAPI.Application.Common.Interfaces;
//using OpenProfileAPI.Application.Common.Models;
//using OpenProfileAPI.Domain.Common;
//using OpenProfileAPI.Domain.Entities;
//using OpenProfileAPI.Domain.Enums;
//using Microsoft.AspNetCore.Identity;

//namespace OpenProfileAPI.Application.Users.Commands.ProfileUpdate;
//public class ProfileUpdateCommand : IRequest<ResponseBase>
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
//}

//public class ProfileUpdateCommandValidator : AbstractValidator<ProfileUpdateCommand>
//{
//    public ProfileUpdateCommandValidator()
//    {
//        RuleFor(x => x.MobileNumber)
//            .NotEmpty().WithMessage("Mobile number is required.");
//        //.Matches(@"^\+?\d{10,15}$").WithMessage("Invalid mobile number format.");

//        RuleFor(x => x.DateOfBirth)
//            .NotNull().WithMessage("Date of birth is required.")
//            .LessThan(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Date of birth must be in the past.");

//        RuleFor(x => x.GenderId)
//            .GreaterThan(0).WithMessage("Gender is required.");

//        RuleFor(x => x.CityId)
//            .NotNull().WithMessage("City is required.");

//        RuleFor(x => x.StateId)
//            .NotNull().WithMessage("State is required.");

//        RuleFor(x => x.CountryId)
//            .NotNull().WithMessage("Country is required.");

//        RuleFor(x => x.Address)
//            .NotEmpty().WithMessage("Address is required.");

//        RuleFor(x => x.PostalCode)
//            .NotEmpty().WithMessage("Postal code is required.");

//        RuleFor(x => x.NationalId)
//            .MaximumLength(20).WithMessage("National ID cannot exceed 20 characters.");

//        //RuleFor(x => x.MaritalStatusId)
//        //    .NotNull().WithMessage("Marital status is required.");

//        RuleFor(x => x.Occupation)
//            .MaximumLength(100).WithMessage("Occupation cannot exceed 100 characters.");

//        //RuleFor(x => x.LinkedInProfile)
//        //    .Must(link => string.IsNullOrWhiteSpace(link) || Uri.IsWellFormedUriString(link, UriKind.Absolute))
//        //    .WithMessage("LinkedIn profile must be a valid URL.");

//        //RuleFor(x => x.UserBio)
//        //    .MaximumLength(1000).WithMessage("Bio cannot exceed 1000 characters.");
//    }
//}

//public class ProfileUpdateCommandHandler(UserManager<User> userManager,
//    IUser currentUser,
//    IApplicationDbContext dbContext
//    ) : IRequestHandler<ProfileUpdateCommand, ResponseBase>
//{
//    private ResponseBase ErrorResponse(string error)
//    {
//        return new ResponseBase
//        {
//            Status = false,
//            Error = error
//        };
//    }

//    public async Task<ResponseBase> Handle(ProfileUpdateCommand request, CancellationToken cancellationToken)
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
