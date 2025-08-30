//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using OpenProfileAPI.Application.Users.Queries.GetUsersWithPagination;

//namespace OpenProfileAPI.Web.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//[Authorize]
//public class UserController(ISender sender) : ControllerBase
//{
//    [HttpGet("get")]
//    public async Task<IActionResult> GetUsers([FromQuery] GetUsersWithPaginationQuery query)
//    {
//        var result = await sender.Send(query);
//        return result.Status ? Ok(result) : BadRequest(result);
//    }

//    //[HttpGet("profile")]
//    //public async Task<IActionResult> GetCustomerUserProfile()
//    //{
//    //    var result = await sender.Send(new ProfileGetQuery());
//    //    return result.Status ? Ok(result) : BadRequest(result);
//    //}

//    //[HttpPut("complete-profile")]
//    //public async Task<IActionResult> CompleteProfile(CompleteProfileCommand command)
//    //{
//    //    var result = await sender.Send(command);
//    //    return result.Status ? Ok(result) : BadRequest(result);
//    //}

//    //[HttpPut("profile")]
//    //public async Task<IActionResult> ProfileUpdate(ProfileUpdateCommand command)
//    //{
//    //    var result = await sender.Send(command);
//    //    return result.Status ? Ok(result) : BadRequest(result);
//    //}

//    //[HttpPost("create")]
//    //public async Task<IActionResult> CreateUser(CreateUserCommand command)
//    //{
//    //    var result = await sender.Send(command);
//    //    return result.Status ? Ok(result) : BadRequest(result);
//    //}

//    //[HttpPut("update")]
//    //public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
//    //{
//    //    var result = await sender.Send(command);
//    //    return result.Status ? Ok(result) : BadRequest(result);
//    //}

//    //[HttpGet("rights")]
//    //public async Task<IActionResult> GetUserRights()
//    //{
//    //    var result = await sender.Send(new GetUserRightsQuery());
//    //    return result.Status ? Ok(result) : BadRequest(result);
//    //}
//}
