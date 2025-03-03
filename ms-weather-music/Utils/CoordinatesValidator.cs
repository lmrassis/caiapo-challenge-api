namespace ms_weather_music.Models.Utils;

public class CoordinatesValidator
{
  public bool IsValidLatitude(double latitude)
  {
    return latitude >= -90 && latitude <= 90;
  }

  public bool IsValidLongitude(double longitude)
  {
    return longitude >= -180 && longitude <= 180;
  }
}