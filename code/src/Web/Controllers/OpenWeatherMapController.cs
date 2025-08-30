using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenProfileAPI.Application.Common.Models;
using OpenProfileAPI.Application.OpenWeatherMap;

namespace OpenProfileAPI.Web.Controllers;

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

    [HttpGet("weather-map")]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> WeatherMapGetQuery([FromQuery] WeatherMapGetQuery query)
    {
        var result = await sender.Send(query);
        return result.Status ? Ok(result) : BadRequest(result);
    }
}
