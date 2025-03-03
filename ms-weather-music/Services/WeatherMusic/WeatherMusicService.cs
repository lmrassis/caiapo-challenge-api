using ms_weather_music.Models.Response;
using ms_weather_music.Models.Utils;
using ms_weather_music.Services.Authorizer;
using ms_weather_music.Services.OpenWeatherMap;
using ms_weather_music.Services.Spotify;

namespace ms_weather_music.Services.WeatherMusic;

public class WeatherMusicService: IWeatherMusicService
{
	private readonly IAuthorizerService _authorizerService;
	private readonly IOpenWeatherMapService _openWeatherMapService;
	private readonly ISpotifyService _spotifyService;

	public WeatherMusicService(IAuthorizerService authorizerService, IOpenWeatherMapService openWeatherMapService, ISpotifyService spotifyService)
	{
		_authorizerService = authorizerService ?? throw new ArgumentNullException(nameof(authorizerService));
		_openWeatherMapService = openWeatherMapService ?? throw new ArgumentNullException(nameof(openWeatherMapService));
		_spotifyService = spotifyService ?? throw new ArgumentNullException(nameof(spotifyService));
	}

	public async Task<Result<WeatherMusicResponse>> GetTopMusicsByCityName(string cityName)
	{
		CityNameValidator validator = new CityNameValidator();

		var validatorResult = validator.IsValidCityName(cityName);

		if (validatorResult.StatusError)
			return Result<WeatherMusicResponse>.Failure(validatorResult.ErrorMessage);
		
		return await GetTopMusics(async () => await _openWeatherMapService.GetTemperatureByCityName(cityName));
	}

	public async Task<Result<WeatherMusicResponse>> GetTopMusicsByCoordinates(double latitude, double longitude)
	{
		CoordinatesValidator validator = new CoordinatesValidator();

		if (!validator.IsValidLatitude(latitude) || !validator.IsValidLongitude(longitude))
      return Result<WeatherMusicResponse>.Failure(ExceptionMessageResponse.CoordinatesInvalids);

		return await GetTopMusics(async () => await _openWeatherMapService.GetTemperatureByCoordinates(latitude, longitude));
	}

	private async Task<Result<WeatherMusicResponse>> GetTopMusics(Func<Task<OpenWeatherMapReponse?>> getTemperatureFunc)
	{
    if (!await _authorizerService.GetAuthorize())
      return Result<WeatherMusicResponse>.Failure(ExceptionMessageResponse.NotAuthorized);

    var temperature = await getTemperatureFunc();

    if (temperature == null)
      return Result<WeatherMusicResponse>.Failure(ExceptionMessageResponse.SongsNotFound);

    var temp = temperature.Main?.Temp;
    var singerName = GetSingerNameByTemperature(temp);

    var artistId = await _spotifyService.GetArtistId(null, singerName);

    if (artistId == null)
      return Result<WeatherMusicResponse>.Failure(ExceptionMessageResponse.SongsNotFound);

    var songs = await _spotifyService.GetTopTracksByArtistId(null, artistId);

    if (songs == null)
      return Result<WeatherMusicResponse>.Failure(ExceptionMessageResponse.SongsNotFound);

    var response = new WeatherMusicResponse 
    {
			SingerName = singerName,
			Temperature = $"{temp}°C",
			Songs = songs
    };

    return Result<WeatherMusicResponse>.Success(response);
	}

	static string GetSingerNameByTemperature(double? temperature)
	{
		if (temperature > 30)
			return SingerNames.DavidGuetta;
		else if (temperature >= 15 && temperature <= 30)
			return  SingerNames.Madonna;
		else if (temperature >= 10 && temperature < 15)
			return SingerNames.OsParalamasDoSucesso;
		else
			return SingerNames.LudwingVanBeethoven;
	}
}