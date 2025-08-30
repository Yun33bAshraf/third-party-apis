using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using OpenProfileAPI.Application.Common.Models;

namespace OpenProfileAPI.Application.OpenWeatherMap;

public class CurrentAndForecastWeatherQuery : IRequest<ResponseBase>
{
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string? Exclude { get; set; } = "current"; // "current", "minutely", "hourly", "daily or alerts"
    public string? Units { get; set; } = "standard"; // "standard", "metric", "imperial"
    public string? Language { get; set; } = "en"; // "en", "es", "fr", etc.
}

public class CurrentAndForecastWeatherQueryValidator : AbstractValidator<CurrentAndForecastWeatherQuery>
{
    public CurrentAndForecastWeatherQueryValidator()
    {
        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90 degrees.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180 degrees.");

        RuleFor(x => x.Units)
            .Must(u => new[] { "standard", "metric", "imperial" }.Contains(u!))
            .WithMessage("Units must be 'standard', 'metric', or 'imperial'.");

        //RuleFor(x => x.Language)
        //    .NotEmpty()
        //    .WithMessage("Language is required.");
    }
}

public class CurrentAndForecastWeatherQueryHandler(IConfiguration configuration, IHttpClientFactory httpClientFactory) : IRequestHandler<CurrentAndForecastWeatherQuery, ResponseBase>
{
    public async Task<ResponseBase> Handle(CurrentAndForecastWeatherQuery request, CancellationToken cancellationToken)
    {
        var appId = configuration["OpenWeatherMap:APIKey"];

        var apiUrl = $"https://api.openweathermap.org/data/3.0/onecall" +
                     $"?lat={request.Latitude}" +
                     $"&lon={request.Longitude}" +
                     $"&exclude={request.Exclude}" +
                     $"&units={request.Units}" +
                     $"&lang={request.Language}" +
                     $"&appid={appId}";

        var client = httpClientFactory.CreateClient();
        var response = await client.GetAsync(apiUrl, cancellationToken);

        if (!response.IsSuccessStatusCode)  
        {
            return new ResponseBase
            {
                Status = false,
                Message = $"Failed to retrieve weather data. Status code: {response.StatusCode}"
            };
        }

        var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);
        var weatherData = JsonSerializer.Deserialize<WeatherApiResponse>(
            jsonString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        return new ResponseBase
        {
            Status = true,
            Data = weatherData
        };
    }
}

public class WeatherApiResponse
{
    [JsonPropertyName("lat")]
    public decimal Latitude { get; set; }

    [JsonPropertyName("lon")]
    public decimal Longitude { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("timezone_offset")]
    public int TimezoneOffset { get; set; }

    [JsonPropertyName("current")]
    public CurrentWeather? Current { get; set; }

    [JsonPropertyName("minutely")]
    public List<MinutelyWeather>? Minutely { get; set; }

    [JsonPropertyName("hourly")]
    public List<HourlyWeather>? Hourly { get; set; }

    [JsonPropertyName("daily")]
    public List<DailyWeather>? Daily { get; set; }

    [JsonPropertyName("alerts")]
    public List<WeatherAlert>? Alerts { get; set; }
}

public class CurrentWeather
{
    [JsonPropertyName("dt")]
    public long DateTime { get; set; }

    [JsonPropertyName("sunrise")]
    public long Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public long Sunset { get; set; }

    [JsonPropertyName("temp")]
    public decimal Temperature { get; set; }

    [JsonPropertyName("feels_like")]
    public decimal FeelsLike { get; set; }

    [JsonPropertyName("pressure")]
    public int Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("dew_point")]
    public decimal DewPoint { get; set; }

    [JsonPropertyName("uvi")]
    public decimal UVIndex { get; set; }

    [JsonPropertyName("clouds")]
    public int Clouds { get; set; }

    [JsonPropertyName("visibility")]
    public int Visibility { get; set; }

    [JsonPropertyName("wind_speed")]
    public decimal WindSpeed { get; set; }

    [JsonPropertyName("wind_deg")]
    public int WindDirection { get; set; }

    [JsonPropertyName("wind_gust")]
    public decimal WindGust { get; set; }

    [JsonPropertyName("weather")]
    public List<WeatherDescription>? Weather { get; set; }
}

public class MinutelyWeather
{
    [JsonPropertyName("dt")]
    public long DateTime { get; set; }

    [JsonPropertyName("precipitation")]
    public decimal Precipitation { get; set; }
}

public class HourlyWeather
{
    [JsonPropertyName("dt")]
    public long DateTime { get; set; }

    [JsonPropertyName("temp")]
    public decimal Temperature { get; set; }

    [JsonPropertyName("feels_like")]
    public decimal FeelsLike { get; set; }

    [JsonPropertyName("pressure")]
    public int Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("dew_point")]
    public decimal DewPoint { get; set; }

    [JsonPropertyName("uvi")]
    public decimal UVIndex { get; set; }

    [JsonPropertyName("clouds")]
    public int Clouds { get; set; }

    [JsonPropertyName("visibility")]
    public int Visibility { get; set; }

    [JsonPropertyName("wind_speed")]
    public decimal WindSpeed { get; set; }

    [JsonPropertyName("wind_deg")]
    public int WindDirection { get; set; }

    [JsonPropertyName("wind_gust")]
    public decimal WindGust { get; set; }

    [JsonPropertyName("weather")]
    public List<WeatherDescription>? Weather { get; set; }

    [JsonPropertyName("pop")]
    public decimal ProbabilityOfPrecipitation { get; set; }
}

public class DailyWeather
{
    [JsonPropertyName("dt")]
    public long DateTime { get; set; }

    [JsonPropertyName("sunrise")]
    public long Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public long Sunset { get; set; }

    [JsonPropertyName("moonrise")]
    public long Moonrise { get; set; }

    [JsonPropertyName("moonset")]
    public long Moonset { get; set; }

    [JsonPropertyName("moon_phase")]
    public decimal MoonPhase { get; set; }

    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    [JsonPropertyName("temp")]
    public TemperatureRange? Temperature { get; set; }

    [JsonPropertyName("feels_like")]
    public FeelsLikeTemperature? FeelsLike { get; set; }

    [JsonPropertyName("pressure")]
    public int Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("dew_point")]
    public decimal DewPoint { get; set; }

    [JsonPropertyName("wind_speed")]
    public decimal WindSpeed { get; set; }

    [JsonPropertyName("wind_deg")]
    public int WindDirection { get; set; }

    [JsonPropertyName("wind_gust")]
    public decimal WindGust { get; set; }

    [JsonPropertyName("weather")]
    public List<WeatherDescription>? Weather { get; set; }

    [JsonPropertyName("clouds")]
    public int Clouds { get; set; }

    [JsonPropertyName("pop")]
    public decimal ProbabilityOfPrecipitation { get; set; }

    [JsonPropertyName("rain")]
    public decimal? Rain { get; set; }

    [JsonPropertyName("uvi")]
    public decimal UVIndex { get; set; }
}

public class TemperatureRange
{
    [JsonPropertyName("day")]
    public decimal Day { get; set; }

    [JsonPropertyName("min")]
    public decimal Min { get; set; }

    [JsonPropertyName("max")]
    public decimal Max { get; set; }

    [JsonPropertyName("night")]
    public decimal Night { get; set; }

    [JsonPropertyName("eve")]
    public decimal Evening { get; set; }

    [JsonPropertyName("morn")]
    public decimal Morning { get; set; }
}

public class FeelsLikeTemperature
{
    [JsonPropertyName("day")]
    public decimal Day { get; set; }

    [JsonPropertyName("night")]
    public decimal Night { get; set; }

    [JsonPropertyName("eve")]
    public decimal Evening { get; set; }

    [JsonPropertyName("morn")]
    public decimal Morning { get; set; }
}

public class WeatherDescription
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

public class WeatherAlert
{
    [JsonPropertyName("sender_name")]
    public string? SenderName { get; set; }

    [JsonPropertyName("event")]
    public string? Event { get; set; }

    [JsonPropertyName("start")]
    public long Start { get; set; }

    [JsonPropertyName("end")]
    public long End { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }
}
