using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThirdPartyAPIs.Application.Common.Models;
using ThirdPartyAPIs.Application.Me;

namespace ThirdPartyAPIs.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class MeController(ISender sender) : ControllerBase
{
    [HttpGet("resume")]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetResume()
    {
        var result = await sender.Send(new ResumeQuery());
        return Ok(result);
    }
}
