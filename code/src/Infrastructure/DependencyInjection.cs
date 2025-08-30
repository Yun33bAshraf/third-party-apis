using System.Data.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using OpenProfileAPI.Application.Common.Interfaces;
using OpenProfileAPI.Domain.Entities;
using OpenProfileAPI.Infrastructure.Data;
using OpenProfileAPI.Infrastructure.Data.Interceptors;
using OpenProfileAPI.Infrastructure.Identity;
using OpenProfileAPI.Infrastructure.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddAuthorizationBuilder();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, serverVersion);
        }, ServiceLifetime.Scoped);

        // Add Identity services
        services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders(); // Add only the SignInManager for login functionality

        services.AddScoped<IDbRepository, DbRepository>();
        services.AddTransient(typeof(DbConnection), (IServiceProvider) => InitializeDatabase(connectionString));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IIdentityService, IdentityService>();

        services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));

        services.AddScoped(typeof(IDataRepository<>), typeof(DataRepository<>));
        services.AddScoped<IEmailSenderRepository, EmailSenderRepository>();

        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IS3FileRepository, S3FileRepository>();
        services.AddScoped<IFileUploadRepository, FileUploadRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<DataSeeder>();

        return services;
    }

    private static object InitializeDatabase(string cconnectionString)
    {
        // [START mysql_connection]
        var connectionString = new MySqlConnectionStringBuilder(cconnectionString);
        DbConnection connection = new MySqlConnection(connectionString.ConnectionString);
        // [END mysql_connection]
        return connection;
    }
}
