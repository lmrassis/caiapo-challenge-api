using System.Text.Json;
using ms_weather_music.Models.Response;
using ms_weather_music.Settings.OpenWeatherMapSettings;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace ms_weather_music.Services.OpenWeatherMap;

public class OpenWeatherMapService: IOpenWeatherMapService
{
	private readonly HttpClient _httpClient;
	private readonly OpenWeatherMapSettings _openWeatherMapSettings;

	public OpenWeatherMapService(HttpClient httpClient, IOptions<OpenWeatherMapSettings> openWeatherMapSettings)
	{
		_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		_openWeatherMapSettings = openWeatherMapSettings.Value;
	}

  public async Task<OpenWeatherMapReponse?> GetTemperatureByCityName(string cityName)
	{
		string content;

		var response = await _httpClient.GetAsync($"{_openWeatherMapSettings.API_URL}?q={cityName}&appid={_openWeatherMapSettings.API_KEY}&units=metric&lang=pt_br");

		if (!response.IsSuccessStatusCode)
			return null;
		
		response.EnsureSuccessStatusCode();

		content = await response.Content.ReadAsStringAsync();

		var result = JsonConvert.DeserializeObject<OpenWeatherMapReponse>(content);

		return result;
	}

	public async Task<OpenWeatherMapReponse?> GetTemperatureByCoordinates(double latitude, double longitude)
	{
		string content;
	
		var response = await _httpClient.GetAsync($"{_openWeatherMapSettings.API_URL}?lat={latitude}&lon={longitude}&appid={_openWeatherMapSettings.API_KEY}&units=metric&lang=pt_br");

		if (!response.IsSuccessStatusCode)
			return null;
		
		response.EnsureSuccessStatusCode();

		content = await response.Content.ReadAsStringAsync();

		var result = JsonConvert.DeserializeObject<OpenWeatherMapReponse>(content);

		return result;
	}
}