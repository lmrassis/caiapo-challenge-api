namespace ms_weather_music.Services.Spotify;

public interface ISpotifyService
{
  Task<string> GetCacheAccessToken();
  Task<string> GetAccessToken();

  Task<string> GetArtistId(string? accessToken, string artistName);

  Task<List<string>> GetTopTracksByArtistId(string? accessToken, string artistId);
}