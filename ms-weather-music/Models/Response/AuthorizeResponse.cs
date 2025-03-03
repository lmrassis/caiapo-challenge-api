namespace ms_weather_music.Models.Response;

public class AuthorizeResponse
{
	public string? status { get; set; }
	public DataResponse? data { get; set; }
}

public class DataResponse
{
	public bool authorization { get; set; }
}