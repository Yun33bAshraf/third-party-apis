using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using OpenProfileAPI.Application.Common.Contracts;
using OpenProfileAPI.Application.Common.Models;
using OpenProfileAPI.Domain.Common;

namespace OpenProfileAPI.Application.OpenWeatherMap;

public class _5Day3HourForecastDataQuery : IRequest<ResponseBase>
{
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}

public class _5Day3HourForecastDataQueryValidator : AbstractValidator<_5Day3HourForecastDataQuery>
{
    public _5Day3HourForecastDataQueryValidator()
    {
        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90 degrees.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180 degrees.");
    }
}

public class _5Day3HourForecastDataQueryHandler(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    : IRequestHandler<_5Day3HourForecastDataQuery, ResponseBase>
{
    public async Task<ResponseBase> Handle(_5Day3HourForecastDataQuery request, CancellationToken cancellationToken)
    {
        var apiKey = configuration["OpenWeatherMap:APIKey"];
        var apiUrl = configuration["OpenWeatherMap:5Day3HourForecastApiUrl"];

        if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiUrl))
            return ApiResponse.Error("API key or API URL is not configured.");

        var apiCall = $"{apiUrl}" +
                      $"lat={request.Latitude}" +
                      $"&lon={request.Longitude}" +
                      $"&appid={apiKey}";

        var client = httpClientFactory.CreateClient();
        var response = await client.GetAsync(apiCall, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return ApiResponse.Error($"Failed to retrieve forecast data. Code: {response.StatusCode.GetDescription}.");

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        var forecastData = JsonSerializer.Deserialize<ForecastResponse>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (forecastData is null)
            return ApiResponse.Error("Failed to deserialize forecast data.");

        var summary = new ForecastSummaryDto
        {
            City = forecastData.City?.Name,
            Country = forecastData.City?.Country,
            Summary = forecastData.List?
            .GroupBy(f => DateTime.TryParse(f.DtTxt, out var parsed) ? parsed.Date : DateTime.MinValue)
            .Select(group => new ForecastBaseDto
            {
                RawDateTime = group.Key,
                Unit = "celsius",
                Forecasts = [.. group.Select(item =>
                {
                    DateTime.TryParse(item.DtTxt, out var parsedDateTime);

                    return new ForecastPeriodDto
                    {
                        Time = TimeOnly.FromDateTime(parsedDateTime),
                        Temperature = item.Main?.Temp != null ? Math.Round(item.Main.Temp - 273.15m, 1) : 0m,
                        FeelsLike = item.Main?.Feels_Like != null ? Math.Round(item.Main.Feels_Like - 273.15m, 1) : 0m,
                        Description = item.Weather?.FirstOrDefault()?.Description ?? "Unknown",
                        Humidity = $"{item.Main?.Humidity ?? 0}%",
                        WindSpeed = item.Wind != null
                            ? $"{Math.Round(item.Wind.Speed * 3.6m, 1)} km/h"
                            : "0 km/h",
                        RainVolume = item.Rain?.VolumeLast3Hours != null
                            ? $"{item.Rain.VolumeLast3Hours} mm"
                            : null
                    };
                })]
            })
            .ToList()
        };

        return ApiResponse.Success(summary, "5-days/3-hours forecast data retrieved successfully");
    }
}

//Custom Response
public class ForecastSummaryDto
{
    public string? City { get; set; }
    public string? Country { get; set; }
    public List<ForecastBaseDto>? Summary { get; set; }
}

public class ForecastBaseDto
{
    public DateTime RawDateTime { get; set; }
    public string DayOfWeek => $"{RawDateTime:dddd, MMMM dd, yyyy}";
    public string? Unit { get; set; } = "celsius";
    public List<ForecastPeriodDto>? Forecasts { get; set; }
}

public class ForecastPeriodDto
{
    public TimeOnly Time { get; set; }
    public decimal Temperature { get; set; }
    public decimal FeelsLike { get; set; }
    public string? Description { get; set; }
    public string? Humidity { get; set; }
    public string? WindSpeed { get; set; }
    public string? RainVolume { get; set; }
}

// Raw Response
public class ForecastResponse
{
    [JsonPropertyName("cod")]
    public string? Cod { get; set; }

    [JsonPropertyName("message")]
    public int Message { get; set; }

    [JsonPropertyName("cnt")]
    public int Cnt { get; set; }

    [JsonPropertyName("list")]
    public List<ForecastEntry>? List { get; set; }

    [JsonPropertyName("city")]
    public ForecastCity? City { get; set; }
}

public class ForecastEntry
{
    [JsonPropertyName("dt")]
    public long Dt { get; set; }

    [JsonPropertyName("main")]
    public MainInfo? Main { get; set; }

    [JsonPropertyName("weather")]
    public List<WeatherInfo>? Weather { get; set; }

    [JsonPropertyName("clouds")]
    public _5Day3HourForecastClouds? Clouds { get; set; }

    [JsonPropertyName("wind")]
    public _5Day3HourForecastWind? Wind { get; set; }

    [JsonPropertyName("visibility")]
    public int Visibility { get; set; }

    [JsonPropertyName("pop")]
    public double Pop { get; set; }

    [JsonPropertyName("rain")]
    public _5Day3HourForecastRain? Rain { get; set; }

    [JsonPropertyName("sys")]
    public ForecastSys? Sys { get; set; }

    [JsonPropertyName("dt_txt")]
    public string? DtTxt { get; set; }
}

public class MainInfo
{
    [JsonPropertyName("temp")]
    public decimal Temp { get; set; }

    [JsonPropertyName("feels_like")]
    public decimal Feels_Like { get; set; }

    [JsonPropertyName("temp_min")]
    public decimal Temp_Min { get; set; }

    [JsonPropertyName("temp_max")]
    public decimal Temp_Max { get; set; }

    [JsonPropertyName("pressure")]
    public int Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }
}

public class WeatherInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("main")]
    public string? Main { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }
}

public class _5Day3HourForecastClouds
{
    [JsonPropertyName("all")]
    public int All { get; set; }
}

public class _5Day3HourForecastWind
{
    [JsonPropertyName("speed")]
    public decimal Speed { get; set; }

    [JsonPropertyName("deg")]
    public int Deg { get; set; }

    [JsonPropertyName("gust")]
    public decimal Gust { get; set; }
}

public class _5Day3HourForecastRain
{
    [JsonPropertyName("3h")]
    public decimal? VolumeLast3Hours { get; set; }
}

public class ForecastSys
{
    [JsonPropertyName("pod")]
    public string? Pod { get; set; }
}

public class ForecastCity
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("coord")]
    public _5Day3HourForecastCoord? Coord { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("population")]
    public int Population { get; set; }

    [JsonPropertyName("timezone")]
    public int Timezone { get; set; }

    [JsonPropertyName("sunrise")]
    public long Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public long Sunset { get; set; }
}

public class _5Day3HourForecastCoord
{
    [JsonPropertyName("lat")]
    public decimal Lat { get; set; }

    [JsonPropertyName("lon")]
    public decimal Lon { get; set; }
}


