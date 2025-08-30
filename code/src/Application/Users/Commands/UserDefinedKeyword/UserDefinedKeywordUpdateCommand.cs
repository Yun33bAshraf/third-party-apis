//using OpenProfileAPI.Application.Common.Interfaces;
//using OpenProfileAPI.Application.Common.Models;
//using OpenProfileAPI.Domain.Common;
//using OpenProfileAPI.Domain.Entities;
//using OpenProfileAPI.Domain.Enums;
//using Microsoft.AspNetCore.Identity;

//namespace OpenProfileAPI.Application.Users.Commands.UserDefinedKeyword;
//public class UserDefinedKeywordUpdateCommand : IRequest<ResponseBase>
//{
//    public int PreferenceId { get; set; }
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

//public class UserDefinedKeywordUpdateCommandValidator : AbstractValidator<UserDefinedKeywordUpdateCommand>
//{
//    public UserDefinedKeywordUpdateCommandValidator()
//    {
//        RuleFor(x => x.JobTitle)
//            .NotEmpty().WithMessage("Job title is required.")
//            .MaximumLength(100).WithMessage("Job title must not exceed 100 characters.");

//        RuleFor(x => x.IndustryId)
//            .NotNull().WithMessage("Industry must be selected.")
//            .GreaterThan(0).WithMessage("Invalid industry selected.");

//        RuleFor(x => x.WorkModeId)
//            .NotNull().WithMessage("Work mode must be selected.")
//            .GreaterThan(0).WithMessage("Invalid work mode selected.");

//        RuleFor(x => x.SalaryRangeMin)
//            .GreaterThanOrEqualTo(0).When(x => x.SalaryRangeMin.HasValue);

//        RuleFor(x => x.SalaryRangeMax)
//            .GreaterThanOrEqualTo(0).When(x => x.SalaryRangeMax.HasValue);

//        RuleFor(x => x)
//            .Must(x => !x.SalaryRangeMin.HasValue || !x.SalaryRangeMax.HasValue || x.SalaryRangeMin <= x.SalaryRangeMax)
//            .WithMessage("SalaryRangeMin must be less than or equal to SalaryRangeMax.");

//        RuleFor(x => x.CompanySize)
//            .InclusiveBetween(1, 3).When(x => x.CompanySize.HasValue)
//            .WithMessage("Company size must be 1 (small), 2 (medium), or 3 (large).");

//        RuleFor(x => x.Location)
//            .MaximumLength(200).WithMessage("Location must not exceed 200 characters.");

//        RuleFor(x => x.JobTypeId)
//            .NotNull().WithMessage("Job type must be selected.")
//            .GreaterThan(0).WithMessage("Invalid job type selected.");

//        RuleFor(x => x.ApplicationLimit)
//            .InclusiveBetween(1, 100).When(x => x.ApplicationLimit.HasValue)
//            .WithMessage("Application limit must be between 1 and 100.");

//        RuleFor(x => x.FrequencyId)
//            .NotNull().WithMessage("Frequency must be selected.")
//            .InclusiveBetween(1, 2).WithMessage("Frequency must be 1 (Daily) or 2 (Weekly).");
//    }
//}

//public class UserDefinedKeywordUpdateCommandHandler(
//    IUser currentUser,
//    UserManager<User> userManager,
//    IApplicationDbContext dbContext)
//    : IRequestHandler<UserDefinedKeywordUpdateCommand, ResponseBase>
//{
//    private ResponseBase ErrorResponse(string message)
//    {
//        return new ResponseBase
//        {
//            Status = false,
//            Message = message
//        };
//    }

//    public async Task<ResponseBase> Handle(UserDefinedKeywordUpdateCommand request, CancellationToken cancellationToken)
//    {
//        var user = await userManager.FindByIdAsync(currentUser.Id.ToString());
//        if (user is null)
//            return ErrorResponse(AppMessage.UserNotFound.GetDescription());

//        var existingPreference = await dbContext.UserJobPreference
//            .FindAsync(new object[] { request.PreferenceId }, cancellationToken);

//        if (existingPreference == null || existingPreference.UserId != currentUser.Id)
//            return ErrorResponse(AppMessage.UnableToFindRecord.GetDescription());

//        // Update existing preference
//        existingPreference.JobTitle = request.JobTitle;
//        existingPreference.IndustryId = request.IndustryId;
//        existingPreference.WorkModeId = request.WorkModeId;
//        existingPreference.SalaryRangeMin = request.SalaryRangeMin;
//        existingPreference.SalaryRangeMax = request.SalaryRangeMax;
//        existingPreference.CompanySize = request.CompanySize;
//        existingPreference.Location = request.Location;
//        existingPreference.JobTypeId = request.JobTypeId;
//        existingPreference.ApplicationLimit = request.ApplicationLimit;
//        existingPreference.FrequencyId = request.FrequencyId;

//        await dbContext.SaveChangesAsync(cancellationToken);

//        return new ResponseBase
//        {
//            Status = true,
//            Message = AppMessage.UserKeywordsCreated.GetDescription()
//        };
//    }
//}

