using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using IApply.Frontend.Common.Constants;
using IApply.Frontend.Common.Models;
using IApply.Frontend.Components;
using IApply.Frontend.Services;
using IApply.Frontend.Services.ApiService;
using IApply.Frontend.Services.ApiService.Asset;
using IApply.Frontend.Services.ApiService.AttendenceMonthly;
using IApply.Frontend.Services.ApiService.Auth;
using IApply.Frontend.Services.ApiService.Business;
using IApply.Frontend.Services.ApiService.Category;
using IApply.Frontend.Services.ApiService.Employees;
using IApply.Frontend.Services.ApiService.Holiday;
using IApply.Frontend.Services.ApiService.Leads;
using IApply.Frontend.Services.ApiService.Leaves;
using IApply.Frontend.Services.ApiService.Me;
using IApply.Frontend.Services.ApiService.Project;
using IApply.Frontend.Services.ApiService.Role;
using IApply.Frontend.Services.ApiService.Subscription;
using IApply.Frontend.Services.ApiService.System;
using IApply.Frontend.Services.ApiService.User;
using IApply.Frontend.Services.ApiService.UserERP;
using IApply.Frontend.Services.ApiService.Workspace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"/var/aspnet-keys"))
    .SetApplicationName("MyBlazorApp")
    .SetDefaultKeyLifetime(TimeSpan.FromDays(90));


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped(sp => new HttpClient { });
builder.Services.AddHttpClient<ApiService>();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
builder.Services.AddAuthentication(Constants.AUTH_SCHEME)
                                    .AddCookie(Constants.AUTH_SCHEME, options =>
                                    {
                                        options.Cookie.Name = Constants.AUTH_COOKIE;
                                        options.LoginPath = "/";
                                        options.LogoutPath = "/logout";

                                        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                                        options.SlidingExpiration = true;
                                        options.Cookie.HttpOnly = true;
                                        // TODO: Need to change
                                        options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                                        options.Cookie.SameSite = SameSiteMode.Strict;
                                    });
builder.Services.AddAuthorization();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<LoadingService>();
builder.Services.AddSingleton<AuthResponseService>();
builder.Services.AddSingleton<AlertService>();
builder.Services.AddSingleton<CacheService>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<ApiService>(provider =>
{
    var httpClient = provider.GetRequiredService<HttpClient>();
    //var baseApiUrl = builder.Configuration.GetSection("BaseURL").Get<string[]>()?.FirstOrDefault() ?? "http://cmsdevapi.techlign.com/api";
    //var baseApiUrl = builder.Configuration["BaseURL"] ?? "http://cmsdevapi.techlign.com/api";
    var baseApiUrl = "https://localhost:5001/api/";
    var localStorage = provider.GetService<ILocalStorageService>();
    var authStateProvider = provider.GetService<AuthenticationStateProvider>();
    var alertService = provider.GetService<AlertService>();
    var navigationManager = provider.GetService<NavigationManager>();
    return new ApiService(httpClient, baseApiUrl, localStorage, authStateProvider, alertService, navigationManager);
});
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddMemoryCache();
//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
//builder.Services.AddScoped<IEmployeeService, EmployeeService>();
//builder.Services.AddScoped<ILeaveService, LeaveService>();
//builder.Services.AddScoped<IHolidayService, HolidayService>();
//builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IWorkspaceService, WorkspaceService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
//builder.Services.AddScoped<ILeadService, LeadService>();
//builder.Services.AddScoped<IAttendenceService, AttendenceService>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<IERPUserService, ERPUserService>();
builder.Services.AddScoped<ISystemService, SystemService>();
builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<IBusinessService, BusinessService>();
//builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<IMeService, MeService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<FirebaseConfig>(builder.Configuration.GetSection("FirebaseConfig"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
