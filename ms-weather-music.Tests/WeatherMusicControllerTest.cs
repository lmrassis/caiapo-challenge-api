using Microsoft.AspNetCore.Mvc;
using Moq;
using ms_weather_music.Controllers;
using ms_weather_music.Models.Response;
using ms_weather_music.Services.WeatherMusic;

public class WeatherMusicControllerTests
{
	private readonly Mock<IWeatherMusicService> _mockWeatherMusicService;
	private readonly WeatherMusicController _controller;

	public WeatherMusicControllerTests()
	{
		_mockWeatherMusicService = new Mock<IWeatherMusicService>();
		_controller = new WeatherMusicController(_mockWeatherMusicService.Object);
	}

	[Fact]
	public async Task GetTopMusicsByCityNameReturnsOkResultWhenServiceReturnsSuccess()
	{
		// Arrange
		var cityName = "TestCity";
		var response = new Result<WeatherMusicResponse>(true, new WeatherMusicResponse(), "");
		_mockWeatherMusicService.Setup(s => s.GetTopMusicsByCityName(cityName))
			.ReturnsAsync(response);

		// Act
		var result = await _controller.GetTopMusicsByCityName(cityName);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		Assert.Equal(response, okResult.Value);
	}

	[Fact]
	public async Task GetTopMusicsByCityNameReturnsBadRequestWhenServiceReturnsFailure()
	{
		// Arrange
		var cityName = "TestCity";
		var response = new Result<WeatherMusicResponse>(false, null, "Error fetching data");
		_mockWeatherMusicService.Setup(s => s.GetTopMusicsByCityName(cityName))
			.ReturnsAsync(response);

		// Act
		var result = await _controller.GetTopMusicsByCityName(cityName);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
		Assert.Equal(response, badRequestResult.Value);
	}

	[Fact]
	public async Task GetTopMusicsByCoordinatesReturnsOkResultWhenServiceReturnsSuccess()
	{
		// Arrange
		var latitude = 10.0;
		var longitude = 20.0;
		var response = new Result<WeatherMusicResponse>(true, new WeatherMusicResponse(), "");
		_mockWeatherMusicService.Setup(s => s.GetTopMusicsByCoordinates(latitude, longitude))
			.ReturnsAsync(response);

		// Act
		var result = await _controller.GetTopMusicsByCoordinates(latitude, longitude);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result.Result);
		Assert.Equal(response, okResult.Value);
	}

	[Fact]
	public async Task GetTopMusicsByCoordinatesReturnsBadRequestWhenServiceReturnsFailure()
	{
		// Arrange
		var latitude = 10.0;
		var longitude = 20.0;
		var response = new Result<WeatherMusicResponse>(false, null, "Error fetching data");
		_mockWeatherMusicService.Setup(s => s.GetTopMusicsByCoordinates(latitude, longitude))
			.ReturnsAsync(response);

		// Act
		var result = await _controller.GetTopMusicsByCoordinates(latitude, longitude);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
		Assert.Equal(response, badRequestResult.Value);
	}
}