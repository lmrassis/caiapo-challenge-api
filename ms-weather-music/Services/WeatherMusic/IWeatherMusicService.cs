using ms_weather_music.Models.Response;

namespace ms_weather_music.Services.WeatherMusic;

public interface IWeatherMusicService
{
	Task<Result<WeatherMusicResponse>> GetTopMusicsByCityName(string cityName);

	Task<Result<WeatherMusicResponse>> GetTopMusicsByCoordinates(double latitude, double longitude);
}