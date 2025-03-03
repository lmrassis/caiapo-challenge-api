using Microsoft.Extensions.Options;
using System.Text.Json;
using ms_weather_music.Models.Response;
using ms_weather_music.Settings.AuthorizerSettings;

namespace ms_weather_music.Services.Authorizer;

public class AuthorizerService: IAuthorizerService
{
	private readonly HttpClient _httpClient;

	private readonly AuthorizerSettings _authorizerSettings;

	public AuthorizerService(HttpClient httpClient, IOptions<AuthorizerSettings> authorizerSettings)
	{
		_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		_authorizerSettings = authorizerSettings.Value;
	}

	public async Task<bool> GetAuthorize()
	{
		string content;

		var response = await _httpClient.GetAsync(_authorizerSettings.API_URL);

		if (!response.IsSuccessStatusCode)
			return false;

		response.EnsureSuccessStatusCode();

		content = await response.Content.ReadAsStringAsync();

		var result = JsonSerializer.Deserialize<AuthorizeResponse>(content);

		return result?.status == "success";
	}
}