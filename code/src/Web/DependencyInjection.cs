using Azure.Identity;
using OpenProfileAPI.Application.Common.Interfaces;
using OpenProfileAPI.Infrastructure.Data;
using OpenProfileAPI.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace OpenProfileAPI.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("full-compliant", policy =>
            {
                policy.RequireClaim("auth-policy-compliance", "true");
            });

            options.AddPolicy("basic-compliant", policy =>
            {
                policy.RequireClaim("usr");
            });
        });

        //services.AddOpenApiDocument((configure, sp) =>
        //{
        //    configure.Title = "Clinic System API";
        //    // Add JWT
        //    configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
        //    {
        //        Type = OpenApiSecuritySchemeType.ApiKey,
        //        Name = "Authorization",
        //        In = OpenApiSecurityApiKeyLocation.Header,
        //        Description = "Type into the textbox: Bearer {your JWT token}."
        //    });
        //    configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        //});

        return services;
    }

    public static IServiceCollection AddKeyVaultIfConfigured(this IServiceCollection services, ConfigurationManager configuration)
    {
        var keyVaultUri = configuration["KeyVaultUri"];
        if (!string.IsNullOrWhiteSpace(keyVaultUri))
        {
            configuration.AddAzureKeyVault(
                new Uri(keyVaultUri),
                new DefaultAzureCredential());
        }

        return services;
    }
}
