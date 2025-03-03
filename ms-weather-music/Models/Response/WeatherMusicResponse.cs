namespace ms_weather_music.Models.Response;

public class WeatherMusicResponse
{
    public string? SingerName { get; set; } = string.Empty;
    public string? Temperature { get; set; }

    public List<string> Songs { get; set; } = [];
}