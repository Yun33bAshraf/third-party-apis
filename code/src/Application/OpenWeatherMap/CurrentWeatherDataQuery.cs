using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using OpenProfileAPI.Application.Common.Models;
using System.Text.Json;
using OpenProfileAPI.Domain.Common;
using OpenProfileAPI.Application.Common.Contracts;

namespace OpenProfileAPI.Application.OpenWeatherMap;

public class CurrentWeatherDataQuery : IRequest<ResponseBase>
{
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}

public class CurrentWeatherDataQueryValidator : AbstractValidator<CurrentWeatherDataQuery>
{
    public CurrentWeatherDataQueryValidator()
    {
        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90 degrees.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180 degrees.");
    }
}

public class CurrentWeatherDataQueryHandler(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    : IRequestHandler<CurrentWeatherDataQuery, ResponseBase>
{
    public async Task<ResponseBase> Handle(CurrentWeatherDataQuery request, CancellationToken cancellationToken)
    {
        var apiKey = configuration["OpenWeatherMap:APIKey"];
        var apiUrl = configuration["OpenWeatherMap:CurrentWeatherApiUrl"];

        if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiUrl))
            return ApiResponse.Error("API key or API URL is not configured.");

        var apiCall = $"{apiUrl}" +
                     $"lat={request.Latitude}" +
                     $"&lon={request.Longitude}" +
                     $"&appid={apiKey}";

        var client = httpClientFactory.CreateClient();
        var response = await client.GetAsync(apiCall, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return ApiResponse.Error($"Failed to retrieve current weather data. Code: {response.StatusCode.GetDescription}.");

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        var weatherData = JsonSerializer.Deserialize<CurrentWeatherResponse>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (weatherData is not null)
        {
            decimal tempCelsius = weatherData.Main?.Temp ?? 0;
            decimal feelsLikeCelsius = weatherData.Main?.FeelsLike ?? 0;
            int humidityPercent = weatherData.Main?.Humidity ?? 0;
            decimal windSpeedKmh = (weatherData.Wind?.Speed ?? 0) * 3.6m;

            var dto = new CurrentWeatherInfoDto
            {
                City = weatherData.Name,
                Country = weatherData.Sys?.Country,
                Longitude = weatherData.Coord?.Lon ?? 0,
                Latitude = weatherData.Coord?.Lat ?? 0,
                Unit = "celsius",
                ObservationTime = DateTimeOffset.FromUnixTimeSeconds(weatherData.Dt).UtcDateTime,
                Temperature = Math.Round((weatherData.Main?.Temp ?? 0) - 273.15m, 1),
                FeelsLike = Math.Round((weatherData.Main?.FeelsLike ?? 0) - 273.15m, 1),
                Description = weatherData.Weather?.FirstOrDefault()?.Description ?? string.Empty,
                Humidity = $"{humidityPercent}%",
                WindSpeed = $"{Math.Round(windSpeedKmh, 1)} km/h",
                Sunrise = DateTimeOffset.FromUnixTimeSeconds(weatherData.Sys?.Sunrise ?? 0).UtcDateTime,
                Sunset = DateTimeOffset.FromUnixTimeSeconds(weatherData.Sys?.Sunset ?? 0).UtcDateTime
            };

            if (dto.Country == "PK")
            {
                dto.ObservationTime = dto.ObservationTime.AddHours(5);
                dto.Sunrise = dto.Sunrise.AddHours(5);
                dto.Sunset = dto.Sunset.AddHours(5);
            }

            return ApiResponse.Success(dto, "Current weather data retrieved successfully.");
        }
        else
        {
            return ApiResponse.Error("Invalid weather data received.");
        }
    }
}

// Custom Response
public class CurrentWeatherInfoDto
{
    public string? City { get; set; }
    public string? Country { get; set; }
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
    public string? Unit { get; set; } // e.g., "celsius". 
    public DateTime ObservationTime { get; set; }
    public decimal Temperature { get; set; }
    public decimal FeelsLike { get; set; }
    public string? Description { get; set; }
    public string? Humidity { get; set; }
    public string? WindSpeed { get; set; }
    public DateTime Sunrise { get; set; }
    public DateTime Sunset { get; set; }
}


// Raw Response
public class CurrentWeatherResponse
{
    [JsonPropertyName("coord")]
    public Coord? Coord { get; set; }

    [JsonPropertyName("weather")]
    public List<Weather>? Weather { get; set; }

    [JsonPropertyName("base")]
    public string? Base { get; set; }

    [JsonPropertyName("main")]
    public MainData? Main { get; set; }

    [JsonPropertyName("visibility")]
    public int Visibility { get; set; }

    [JsonPropertyName("wind")]
    public Wind? Wind { get; set; }

    [JsonPropertyName("rain")]
    public Rain? Rain { get; set; }

    [JsonPropertyName("clouds")]
    public Clouds? Clouds { get; set; }

    [JsonPropertyName("dt")]
    public long Dt { get; set; }

    [JsonPropertyName("sys")]
    public Sys? Sys { get; set; }

    [JsonPropertyName("timezone")]
    public int Timezone { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("cod")]
    public int Cod { get; set; }
}

public class Coord
{
    [JsonPropertyName("lon")]
    public decimal Lon { get; set; }

    [JsonPropertyName("lat")]
    public decimal Lat { get; set; }
}

public class Weather
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

public class MainData
{
    [JsonPropertyName("temp")]
    public decimal Temp { get; set; }

    [JsonPropertyName("feels_like")]
    public decimal FeelsLike { get; set; }

    [JsonPropertyName("temp_min")]
    public decimal TempMin { get; set; }

    [JsonPropertyName("temp_max")]
    public decimal TempMax { get; set; }

    [JsonPropertyName("pressure")]
    public int Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("sea_level")]
    public int SeaLevel { get; set; }

    [JsonPropertyName("grnd_level")]
    public int GroundLevel { get; set; }
}

public class Wind
{
    [JsonPropertyName("speed")]
    public decimal Speed { get; set; }

    [JsonPropertyName("deg")]
    public int Deg { get; set; }

    [JsonPropertyName("gust")]
    public decimal Gust { get; set; }
}

public class Rain
{
    [JsonPropertyName("1h")]
    public decimal OneHour { get; set; }
}

public class Clouds
{
    [JsonPropertyName("all")]
    public int All { get; set; }
}

public class Sys
{
    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("sunrise")]
    public long Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public long Sunset { get; set; }
}

