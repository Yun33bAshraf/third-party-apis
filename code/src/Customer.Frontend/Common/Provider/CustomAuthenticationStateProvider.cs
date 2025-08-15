//using Microsoft.AspNetCore.Components.Authorization;
//using IApply.Frontend.Models.Auth.Login;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Http;
//using IApply.Frontend.Models;
//using IApply.Frontend.Shared;
//using Microsoft.AspNetCore.Authentication;
//using System.Reflection;

//namespace IApply.Frontend.Common.Services;

//public class CustomAuthenticationStateProvider : AuthenticationStateProvider
//{
//    private readonly IHttpContextAccessor _httpContextAccessor; // Accessor to get HttpContext
//    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

//    public CustomAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor)
//    {
//        _httpContextAccessor = httpContextAccessor;
//    }

//    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
//    {
//        var httpContext = _httpContextAccessor.HttpContext;
//        try
//        {
//            var userSession = await httpContext.ReadItemEncryptedAsync<LoginResponse>("UserSession");
//            if (userSession == null)
//            {
//                return new AuthenticationState(_anonymous);
//            }
//            //var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
//            //    {
//            //        new Claim(ClaimTypes.Name, userSession.UserId),
//            //        new Claim(ClaimTypes.Thumbprint, userSession.AccessToken)
//            //    }, "JwtAuth"));
//            LoggedInUserModel user = new LoggedInUserModel(userSession.UserId, null, null, userSession.AccessToken);
//            var claims = user.ToClaims();

//            var identity = new ClaimsIdentity(claims, Constants.AUTH_SCHEME);
//            var claimsPrincipal = new ClaimsPrincipal(identity);
//            return new AuthenticationState(claimsPrincipal);
//        }
//        catch
//        {
//            return new AuthenticationState(_anonymous);
//        }
//    }

//    public async Task<ClaimsPrincipal> UpdateAuthenticationState(LoginResponse? userSession)
//    {
//        ClaimsPrincipal claimsPrincipal;
//        var httpContext = _httpContextAccessor.HttpContext;

//        if (userSession != null)
//        {
//            //claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
//            //{
//            //    new Claim(ClaimTypes.Name, userSession.UserId),
//            //    new Claim(ClaimTypes.Thumbprint, userSession.AccessToken)
//            //}));

//            //userSession.ExpiryTimeStamp = DateTime.Now.AddSeconds(userSession.ExpiresIn);
//            //await httpContext.SaveItemEncryptedAsync("UserSession", userSession); // Save using CookieService

//            LoggedInUserModel user = new LoggedInUserModel(userSession.UserId, null, null, userSession.AccessToken);

//            var claims = user.ToClaims();
//            var identity = new ClaimsIdentity(claims, Constants.AUTH_SCHEME);
//            claimsPrincipal = new ClaimsPrincipal(identity);
//            //var authProperties = new AuthenticationProperties
//            //{
//            //    IsPersistent = false//Model.RememberMe
//            //};

//            //await httpContext.SignInAsync(Constants.AUTH_SCHEME, claimsPrincipal, authProperties);
//        }
//        else
//        {
//            claimsPrincipal = _anonymous;
//            httpContext.Response.Cookies.Delete("UserSession"); // Remove cookie
//        }

//        //NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
//        return claimsPrincipal;
//    }

//    public async Task<string> GetToken()
//    {
//        var result = string.Empty;
//        var httpContext = _httpContextAccessor.HttpContext;

//        try
//        {
//            var userSession = await httpContext.ReadItemEncryptedAsync<LoginResponse>("UserSession");
//            if (userSession != null && DateTime.Now < userSession.ExpiryTimeStamp)
//            {
//                result = userSession.AccessToken;
//            }
//        }
//        catch
//        {
//            // Handle exceptions if necessary
//        }
//        return result;
//    }
//}
