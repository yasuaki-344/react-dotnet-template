using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("weather_forecasts")]
    public class WeatherForecast
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("temperature")]
        public int TemperatureC { get; set; }

        [Column("summary")]
        public string? Summary { get; set; }
    }
}
