using ms_weather_music.Models.Response;

namespace ms_weather_music.Services.OpenWeatherMap;

public interface IOpenWeatherMapService
{
  Task<OpenWeatherMapReponse?> GetTemperatureByCityName(string cityName);

  Task<OpenWeatherMapReponse?> GetTemperatureByCoordinates(double latitude, double longitude);
}