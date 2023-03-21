using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApplicationCore.Dto;

[DisplayName("WeatherForecast")]
public class WeatherForecastDto
{
    [Required]
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [Required]
    [JsonPropertyName("temperature_c")]
    public int TemperatureC { get; set; }

    [Required]
    [JsonPropertyName("temperature_f")]
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    [Required]
    [JsonPropertyName("summary")]
    public string? Summary { get; set; }
}
