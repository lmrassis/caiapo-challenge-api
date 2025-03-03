using Microsoft.AspNetCore.Mvc;
using ms_weather_music.Models.Response; 
using ms_weather_music.Services.WeatherMusic;

namespace ms_weather_music.Controllers;

[ApiController]
[Route("api/v1/weather-music")]
public class WeatherMusicController: ControllerBase
{
	private readonly IWeatherMusicService _weatherMusicService;

	public WeatherMusicController(IWeatherMusicService weatherMusicService)
	{
		_weatherMusicService = weatherMusicService;
	}

	/// <summary>
	///	Get up to a maximum of the 5 best songs of an artist based on the name of the city.
	/// </summary>
	/// <param name="cityName">City name. To be more precise, include the name of the city, a comma, and the two-letter country code (ISO3166). You will get all the matching cities in the chosen country. The order is important - first is the city name, then the comma, and then the country. Example - London, GB or New York, US.</param>
	/// <returns>Up to a maximum of the top 5 songs by an artist.</returns>
	[HttpGet("top-musics/city/{cityName}")]
	public async Task<ActionResult<Result<WeatherMusicResponse>>> GetTopMusicsByCityName(string cityName)
	{
		var result = await _weatherMusicService.GetTopMusicsByCityName(cityName);
		return result.IsSuccess ? Ok(result) : BadRequest(result);
	}

	/// <summary>
	///	Get up to a maximum of the 5 best songs by an artist based on the coordinates.
	/// </summary>
	/// <param name="latitude">Latitude. The accepted range of values is between -80 and 80.</param>
	/// <param name="longitude">Longitude. The accepted range of values is between -180 and 180.</param>
	/// <returns>Up to a maximum of the top 5 songs by an artist.</returns>
	[HttpGet("top-musics/coordinates/{latitude}/{longitude}")]
	public async Task<ActionResult<Result<WeatherMusicResponse>>> GetTopMusicsByCoordinates(double latitude, double longitude)
	{
		var result = await _weatherMusicService.GetTopMusicsByCoordinates(latitude, longitude);
		return result.IsSuccess ? Ok(result) : BadRequest(result);
	}
}