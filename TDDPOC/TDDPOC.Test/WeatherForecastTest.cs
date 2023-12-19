using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TDDPOC.Controllers;
using TDDPOC.Service;

namespace TDDPOC.Test
{
    public class WeatherForecastTest
    {
        private WeatherForecastController _controller;
        private IFixture _fixture;
        private Mock<IWeatherForecastService> _mockWeatherForecastService;

        public WeatherForecastTest()
        {
            _fixture = new Fixture();
            _mockWeatherForecastService = new Mock<IWeatherForecastService>();
            _fixture.Register(() => _mockWeatherForecastService.Object);
            _controller = new WeatherForecastController(_mockWeatherForecastService.Object);
        }

        [Fact]
        public async void Get_WeatherForeCasts_ReturnsWeather()
        {
            var weatherForecast = _fixture.Create<WeatherForecast>();
            _mockWeatherForecastService.Setup(w => w.GetById(weatherForecast.Summary)).Returns(weatherForecast);

            var result = await _controller.GetBySummary(weatherForecast.Summary);

            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            var actualValue = objectResult.Value.Should().BeOfType<WeatherForecast>().Subject;
            actualValue.Should().NotBeNull();
            actualValue.Equals(weatherForecast);
        }

        [Fact]
        public async void Get_InvalidSummary_ReturnsNotFound()
        {
            WeatherForecast weatherForecast = null;
            string summary = new Guid().ToString();
            _mockWeatherForecastService.Setup(w => w.GetById(summary)).Returns(weatherForecast);

            var result = await _controller.GetBySummary(summary);

            var objectResult = result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void Get_InvalidNullSummary_ThrowsArgumentNullException()
        {
            try
            {
                var result = await _controller.GetBySummary(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Equal("Value cannot be null. (Parameter 'summary')", ex.Message);
            }
        }
    }
}