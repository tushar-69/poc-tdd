using System.ComponentModel.DataAnnotations;

namespace TDDPOC
{
    public class WeatherForecast
    {
        public string Date { get; set; }

        [Range(-270, 270)]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [MinLength(3)]
        public string? Summary { get; set; }
    }
}
