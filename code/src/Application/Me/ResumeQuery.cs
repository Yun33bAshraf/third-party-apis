using OpenProfileAPI.Application.Common.Contracts;
using OpenProfileAPI.Application.Common.Models;

namespace OpenProfileAPI.Application.Me;

public class ResumeQuery : IRequest<ResponseBase>
{
}

public class ResumeQueryHandler : IRequestHandler<ResumeQuery, ResponseBase>
{
    public Task<ResponseBase> Handle(ResumeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var resume = new ResumeResponse();

            return Task.FromResult(ApiResponse.Success(resume, "Data retreived successfully."));
        }
        catch (Exception ex)
        {
            return Task.FromResult(ApiResponse.Error("Failed to load resume", ex.Message));
        }
    }
}

public class ResumeResponse
{
    public string FullName { get; set; } = "Yuneeb Ashraf";
    public string Email { get; set; } = "yunee6ashraf@gmail.com";
    public string Phone { get; set; } = "+92 (340) 815 4066";
    public string LinkedIn { get; set; } = "https://www.linkedin.com/in/yuneeb-ashraf-5132272a7/";

    public string Summary { get; set; } =
        ".NET Developer with 2+ years of experience in building scalable web applications, " +
        "RESTful APIs, and Blazor solutions. Strong skills in backend development, API integration, " +
        "and database management with a focus on clean architecture, system optimization, and Python for backend services.";

    public TechnicalSkills Skills { get; set; } = new TechnicalSkills();

    public List<Experience> Experiences { get; set; } = new List<Experience>
        {
            new Experience
            {
                Title = "Full Stack Developer",
                Company = "Techlign",
                Duration = "October 2024 – Present",
                Responsibilities = new List<string>
                {
                    "Developed scalable RESTful APIs using .NET Core and SQL.",
                    "Integrated Blazor WebAssembly to enhance front-end user experience.",
                    "Worked with cross-functional teams to optimize system performance."
                }
            },
            new Experience
            {
                Title = "Backend Developer",
                Company = "Strahlen Studios",
                Duration = "July 2023 – October 2024",
                Responsibilities = new List<string>
                {
                    "Built and optimized APIs for high-performance applications.",
                    "Applied clean architecture principles for maintainability and scalability.",
                    "Integrated third-party APIs to enhance functionality."
                }
            },
            new Experience
            {
                Title = "Freelance Backend Developer",
                Company = "CrewU",
                Duration = "June 2024 – July 2024",
                Responsibilities = new List<string>
                {
                    "Developed backend systems using Python and FastAPI.",
                    "Implemented MongoDB for scalable data storage and integrated email automation."
                }
            }
        };

    public List<Project> Projects { get; set; } = new List<Project>
        {
            new Project
            {
                Name = "Techlign ERP",
                Description = "Developed an ERP system for managing employee records and workflows.",
                Technologies = new[] { ".NET 8", "Blazor", "MySQL" }
            },
            new Project
            {
                Name = "Mobula Ray EV",
                Description = "Designed APIs for EV registration, vehicle management, and booking systems.",
                Technologies = new[] { ".NET Core", "PostgreSQL", "Stripe", "AWS" }
            },
            new Project
            {
                Name = "Ticketing System",
                Description = "Created a multi-language task management system with Blazor and .NET Core APIs.",
                Technologies = new[] { ".NET Core", "MySQL", "Blazor" }
            },
            new Project
            {
                Name = "CrewU Website",
                Description = "Developed backend APIs with FastAPI, integrated MongoDB for scalable storage, and implemented Gmail SMTP for email automation.",
                Technologies = new[] { "Python", "FastAPI", "MongoDB", "Gmail SMTP" }
            },
            new Project
            {
                Name = "AIC Business Management System",
                Description = "Designed backend APIs for a business management platform.",
                Technologies = new[] { ".NET Core", "Microservices", "MySQL" }
            },
            new Project
            {
                Name = "Strahlen POS",
                Description = "Built backend APIs for a POS system with FBR tax compliance.",
                Technologies = new[] { ".NET Core", "MySQL", "FBR APIs" }
            }
        };

    public Education Education { get; set; } = new Education
    {
        Degree = "Associate Degree in Computer Science",
        Institution = "Virtual University of Pakistan",
        Duration = "October 2022 – August 2025"
    };
}

public class TechnicalSkills
{
    public string[] LanguagesAndFrameworks { get; set; } =
        { "C#", "ASP.NET Core", "Blazor", "Python", "MVC" };

    public string[] APIs { get; set; } =
        { "RESTful", "FastAPI", "Web & Mobile API Integration" };

    public string[] Databases { get; set; } =
        { "SQL Server", "MySQL", "PostgreSQL", "MongoDB", "Entity Framework Core" };

    public string[] CloudAndIntegrations { get; set; } =
        { "AWS (EC2, S3, Lambda)", "Stripe", "Smartcar API", "FBR Invoice APIs" };

    public string[] Tools { get; set; } =
        { "Visual Studio", "Git", "Postman", "Swagger", "JIRA" };

    public string[] Methodologies { get; set; } =
        { "Agile", "Clean Architecture", "Microservices", "Unit Testing" };
}

public class Experience
{
    public string Title { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public List<string> Responsibilities { get; set; } = new();
}

public class Project
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string[] Technologies { get; set; } = Array.Empty<string>();
}

public class Education
{
    public string Degree { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
}

