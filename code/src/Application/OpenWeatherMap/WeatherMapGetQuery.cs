using Microsoft.Extensions.Configuration;
using OpenProfileAPI.Application.Common.Contracts;
using OpenProfileAPI.Application.Common.Models;
using OpenProfileAPI.Domain.Common;

namespace OpenProfileAPI.Application.OpenWeatherMap;

public class WeatherMapGetQuery : IRequest<ResponseBase>
{
    public string Layer { get; set; } = string.Empty;
    public int Zoom { get; set; }
    public int XCoordinate { get; set; }
    public int YCoordinate { get; set; }
}

public class WeatherMapGetQueryValidator : AbstractValidator<WeatherMapGetQuery>
{
    private static readonly string[] AllowedLayers =
    {
        "clouds_new",
        "precipitation_new",
        "pressure_new",
        "wind_new",
        "temp_new"
    };

    public WeatherMapGetQueryValidator()
    {
        RuleFor(x => x.Layer)
            .NotEmpty().WithMessage("Layer is required.")
            .Must(layer => AllowedLayers.Contains(layer))
            .WithMessage("Invalid layer type. Allowed: " + string.Join(", ", AllowedLayers));

        RuleFor(x => x.Zoom)
            .InclusiveBetween(0, 18)
            .WithMessage("Zoom must be between 0 and 18.");

        RuleFor(x => x.XCoordinate)
            .GreaterThanOrEqualTo(0)
            .WithMessage("X must be a positive integer.");

        RuleFor(x => x.YCoordinate)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Y must be a positive integer.");
    }
}

public class WeatherMapGetQueryHandler(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    : IRequestHandler<WeatherMapGetQuery, ResponseBase>
{
    public async Task<ResponseBase> Handle(WeatherMapGetQuery request, CancellationToken cancellationToken)
    {
        var apiKey = configuration["OpenWeatherMap:APIKey"];
        var apiUrl = configuration["OpenWeatherMap:WeatherMapApiUrl"];

        if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiUrl))
            return ApiResponse.Error("API key or API URL is not configured.");

        var apiCall = $"{apiUrl}" +
                      $"{request.Layer}" +
                      $"/{request.Zoom}" +
                      $"/{request.XCoordinate}" +
                      $"/{request.YCoordinate}.png?" +
                      $"&appid={apiKey}";

        var client = httpClientFactory.CreateClient();

        var response = await client.GetAsync(apiCall, cancellationToken);

        if (!response.IsSuccessStatusCode)

            return ApiResponse.Error($"Failed to retrieve weather map data. Code: {response.StatusCode.GetDescription()}.");
        
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        
        return ApiResponse.Success(content);
    }
}
