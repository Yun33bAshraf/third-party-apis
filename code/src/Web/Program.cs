using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using OpenProfileAPI.Infrastructure.Data;
using OpenProfileAPI.Infrastructure.Data.Configurations;
using OpenProfileAPI.Web.Middleware;
using OpenProfileAPI.Application;
using OpenProfileAPI.Web;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins", builder =>
    {
        builder
            .WithOrigins(
                //"https://localhost:7223",
                //"http://localhost:3000",
                //"https://localhost:7223",
                //"http://tsapp.dev.techlign.com",
                "http://tsapp.dev.techlign.com"
                //https://localhost:6001"
                )
            //.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Warning()
    .WriteTo.Console()
    .WriteTo.MySQL(
        connectionString: connectionString,
        tableName: "Logs"
    )
    .CreateLogger();

builder.Host.UseSerilog();

// JWT Authentication Configuration
var jwtSection = builder.Configuration.GetSection("Jwt");
string secretKey = jwtSection["AdminSecretKey"] ?? "";
if (string.IsNullOrEmpty(secretKey))
{
    throw new Exception("JWT SecretKey is missing in the configuration.");
}

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
});

var key = Encoding.UTF8.GetBytes(secretKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Add services to the container.
builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();
builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("AppConfig"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<AppConfig>>().Value);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OpenProfileAPI", Version = "v1" });
    // c.SchemaFilter<SwaggerSchemaFilter>();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
                Scheme = "Bearer",
                Name = "Bearer",
                In = ParameterLocation.Header,
            }, new List<string>()
        },
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();

// Use CORS
//app.UseCors("AllowOrigins");
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // TODO: Need to commend if build without migration
    await app.InitialiseDatabaseAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Ensure that the CORS middleware is added before any other middleware that could short-circuit the request, such as authentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.Map("/", () => Results.Redirect("/swagger/index.html"));

//app.MapEndpoints();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    seeder.Seed(); // or await seeder.SeedAsync() if async
}

app.Run();

public partial class Program { }
