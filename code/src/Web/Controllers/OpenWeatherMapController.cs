using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThirdPartyAPIs.Application.Common.Models;
using ThirdPartyAPIs.Application.OpenWeatherMap;

namespace ThirdPartyAPIs.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class OpenWeatherMapController(ISender sender) : ControllerBase
{
    [HttpGet("current-and-forecast-weather")]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> CurrentAndForecastWeather([FromQuery] CurrentAndForecastWeatherQuery query)
    {
        var result = await sender.Send(query);
        return result.Status ? Ok(result) : BadRequest(result);
    }
    
    [HttpGet("current-weather")]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CurrentWeatherData([FromQuery] CurrentWeatherDataQuery query)
    {
        var result = await sender.Send(query);
        return result.Status ? Ok(result) : BadRequest(result);
    }

    [HttpGet("5-days-3-hours-forecast")]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> _5Day3HourForecastData([FromQuery] _5Day3HourForecastDataQuery query)
    {
        var result = await sender.Send(query);
        return result.Status ? Ok(result) : BadRequest(result);
    }
}
