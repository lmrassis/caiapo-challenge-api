namespace ms_weather_music.Models.Response;

public static class ExceptionMessageResponse
{
	public const string NotAuthorized = "Not authorized";
	public const string SongsNotFound = "Songs not found";
	public const string CoordinatesInvalids = "Coordinates Invalids: Accept range values - latitude(-80,80), longitude(-180, 180)";
	public const string CityNameNullOrEmpty = "The name of the city cannot be null or empty.";
	public const string CityNameExceptedCaracteres = "The name of the city cannot exceed 100 characters.";
	public const string CityNameCaracteresInvalids = "The city name contains invalid characters.";
}