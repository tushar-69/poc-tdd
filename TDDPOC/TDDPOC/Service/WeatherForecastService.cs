namespace TDDPOC.Service
{
    public class WeatherForecastService : IWeatherForecastService
    {
        public WeatherForecast GetById(string summary)
        {
            return new WeatherForecast();
        }
    }
}
