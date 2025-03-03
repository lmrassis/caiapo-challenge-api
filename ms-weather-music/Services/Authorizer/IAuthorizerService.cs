namespace ms_weather_music.Services.Authorizer;

public interface IAuthorizerService
{
  Task<bool> GetAuthorize();
}