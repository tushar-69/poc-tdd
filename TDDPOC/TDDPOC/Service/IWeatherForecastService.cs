namespace TDDPOC.Service
{
    public interface IWeatherForecastService
    {
        WeatherForecast GetById(string summary);
    }
}
