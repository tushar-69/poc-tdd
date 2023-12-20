using TDDPOC.Controllers;
using TDDPOC.Service;

namespace TDDPOC.Test.ControllerTest
{
    public class WeatherForecastTest
    {
        private readonly WeatherForecastController _controller;
        private readonly IFixture _fixture;
        private readonly Mock<IWeatherForecastService> _mockWeatherForecastService;

        public WeatherForecastTest()
        {
            _fixture = new Fixture();
            _mockWeatherForecastService = new Mock<IWeatherForecastService>();
            _fixture.Register(() => _mockWeatherForecastService.Object);
            _controller = new WeatherForecastController(_mockWeatherForecastService.Object);
        }

        [Fact]
        public void Get_WeatherForeCasts_ReturnsWeather()
        {
            var weatherForecast = _fixture.Create<WeatherForecast>();
            _mockWeatherForecastService.Setup(forecast => forecast.GetById(weatherForecast.Summary)).Returns(weatherForecast);

            var result = _controller.GetBySummary(weatherForecast.Summary);

            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            var actualValue = objectResult.Value.Should().BeOfType<WeatherForecast>().Subject;
            actualValue.Equals(weatherForecast);
        }

        [Fact]
        public void Get_InvalidSummary_ReturnsNotFound()
        {
            WeatherForecast weatherForecast = null;
            string summary = new Guid().ToString();
            _mockWeatherForecastService.Setup(forecast => forecast.GetById(summary)).Returns(weatherForecast);

            var result = _controller.GetBySummary(summary);

            var objectResult = result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Get_InvalidNullSummary_ThrowsArgumentNullException()
        {
            try
            {
                var result = _controller.GetBySummary(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Equal("Value cannot be null. (Parameter 'summary')", ex.Message);
            }
        }
    }
}