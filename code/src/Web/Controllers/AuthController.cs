//using ThirdPartyAPIs.Application.Auth.Command.ChangePassword;
//using ThirdPartyAPIs.Application.Auth.Command.CompleteForgetPassword;
//using ThirdPartyAPIs.Application.Auth.Command.CompleteSignUp;
//using ThirdPartyAPIs.Application.Auth.Command.ForgetPassword;
//using ThirdPartyAPIs.Application.Auth.Command.Login;
//using ThirdPartyAPIs.Application.Auth.Command.RefreshToken;
//using ThirdPartyAPIs.Application.Auth.Command.Register;
//using ThirdPartyAPIs.Application.Users.Commands.CompleteRegistration;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace ThirdPartyAPIs.Web.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//public class AuthController(ISender sender) : ControllerBase
//{
//    [AllowAnonymous]
//    [HttpPost("register")]
//    public async Task<IActionResult> CreateUser(RegisterCommand command)
//    {
//        var result = await sender.Send(command);
//        return result.Status ? Ok(result) : BadRequest(result);
//    }

//    [AllowAnonymous]
//    [HttpPost("verify-email")]
//    public async Task<IActionResult> SignUpComplete([FromBody] SignUpCompleteCommand command)
//    {
//        var response = await sender.Send(command);
//        if (!response.Status)
//            return BadRequest(response);

//        return Ok(response);
//    }

//    [AllowAnonymous]
//    [HttpPost("login")]
//    public async Task<IResult> Login(LoginCommand query)
//    {
//        var result = await sender.Send(query);
//        return result.Status ? Results.Ok(result) : Results.BadRequest(result);
//    }

//    [HttpPost("change-password")]
//    public async Task<IResult> ChangePassword(ChangePasswordCommand query)
//    {
//        var result = await sender.Send(query);
//        return result.Status ? Results.Ok(result) : Results.BadRequest(result);
//    }

//    [AllowAnonymous]
//    [HttpPost("forget-password")]
//    public async Task<IResult> ForgetPassword(ForgetPasswordCommand query)
//    {
//        var result = await sender.Send(query);
//        return result.Status ? Results.Ok(result) : Results.BadRequest(result);
//    }

//    [AllowAnonymous]
//    [HttpPost("reset-password")]
//    public async Task<IResult> CompletePasswordReset(CompleteForgetPasswordCommand query)
//    {
//        var result = await sender.Send(query);
//        return result.Status ? Results.Ok(result) : Results.BadRequest(result);
//    }

//    [HttpGet("refresh-token")]
//    public async Task<IActionResult> RefreshTokenGet()
//    {
//        var query = new RefreshTokenCommand();
//        var result = await sender.Send(query);

//        return result.Status ? Ok(result) : BadRequest(result);
//    }

//    //[AllowAnonymous]
//    //[HttpPost("complete-registration")]
//    //public async Task<IResult> CompleteRegistration(CompleteRegistrationCommand query)
//    //{
//    //    var result = await sender.Send(query);
//    //    return result.Status ? Results.Ok(result) : Results.BadRequest(result);
//    //}
//}
