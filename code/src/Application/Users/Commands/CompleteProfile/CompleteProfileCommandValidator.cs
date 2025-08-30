//namespace OpenProfileAPI.Application.Users.Commands.CompleteProfile;

//public class CompleteProfileCommandValidator : AbstractValidator<CompleteProfileCommand>
//{
//    public CompleteProfileCommandValidator()
//    {
//        RuleFor(x => x.MobileNumber)
//            .NotEmpty().WithMessage("Mobile number is required.");
//            //.Matches(@"^\+?\d{10,15}$").WithMessage("Invalid mobile number format.");

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

//        RuleFor(x => x.MaritalStatusId)
//            .NotNull().WithMessage("Marital status is required.");

//        RuleFor(x => x.Occupation)
//            .MaximumLength(100).WithMessage("Occupation cannot exceed 100 characters.");

//        //RuleFor(x => x.LinkedInProfile)
//        //    .Must(link => string.IsNullOrWhiteSpace(link) || Uri.IsWellFormedUriString(link, UriKind.Absolute))
//        //    .WithMessage("LinkedIn profile must be a valid URL.");

//        //RuleFor(x => x.UserBio)
//        //    .MaximumLength(1000).WithMessage("Bio cannot exceed 1000 characters.");

//        When(x => x.Experiences.Count > 0, () => RuleForEach(x => x.Experiences).SetValidator(new CurrentUserExperienceValidator()));

//        When(x => x.Educations.Count > 0, () => RuleForEach(x => x.Educations).SetValidator(new CurrentUserEducationValidator()));

//        When(x => x.Skills.Count > 0, () => RuleForEach(x => x.Skills).SetValidator(new CurrentUserSkillValidator()));
//    }
//}

//public class CurrentUserExperienceValidator : AbstractValidator<CurrentUserExperience>
//{
//    public CurrentUserExperienceValidator()
//    {
//        RuleFor(e => e.CompanyName).NotEmpty().WithMessage("Company name is required.");
//        RuleFor(e => e.Position).NotEmpty().WithMessage("Position is required.");
//        RuleFor(e => e.Location).NotEmpty().WithMessage("Location is required.");
//        RuleFor(e => e.StartDate).NotEmpty().WithMessage("Start date is required.");
//        RuleFor(e => e.EndDate)
//            .GreaterThan(e => e.StartDate)
//            .When(e => e.EndDate.HasValue && !e.IsCurrent)
//            .WithMessage("End date must be after start date.");
//    }
//}

//public class CurrentUserEducationValidator : AbstractValidator<CurrentUserEducation>
//{
//    public CurrentUserEducationValidator()
//    {
//        RuleFor(e => e.Degree).NotEmpty().WithMessage("Degree is required.");
//        RuleFor(e => e.Institution).NotEmpty().WithMessage("Institution is required.");
//        RuleFor(e => e.FieldOfStudy).NotEmpty().WithMessage("Field of study is required.");
//        RuleFor(e => e.StartDate).NotEmpty().WithMessage("Start date is required.");
//        RuleFor(e => e.EndDate)
//            .GreaterThan(e => e.StartDate)
//            .When(e => e.EndDate.HasValue)
//            .WithMessage("End date must be after start date.");
//    }
//}

//public class CurrentUserSkillValidator : AbstractValidator<CurrentUserSkills>
//{
//    public CurrentUserSkillValidator()
//    {
//        RuleFor(s => s.SkillId).GreaterThan(0).WithMessage("Skill is required.");
//        RuleFor(s => s.ProficiencyLevelId)
//            .InclusiveBetween(1, 3).WithMessage("Proficiency level is required.");
//    }
//}
