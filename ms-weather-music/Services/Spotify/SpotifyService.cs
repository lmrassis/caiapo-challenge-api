using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ms_weather_music.Models.Response;
using ms_weather_music.Settings.SpotifySettings;


namespace ms_weather_music.Services.Spotify;

public class SpotifyService: ISpotifyService
{
	private readonly HttpClient _httpClient;
	private readonly IMemoryCache _cache;

	private readonly SpotifySettings _spotifySettings;
	private const string CacheKey = "SpotifyAccessToken";
	private TimeSpan _tokenExpiration = TimeSpan.FromMinutes(60);

	public SpotifyService(HttpClient httpClient, IOptions<SpotifySettings> spotifySettings, IMemoryCache cache)
	{
		_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		_spotifySettings = spotifySettings.Value;
		_cache = cache ?? throw new ArgumentNullException(nameof(cache));
	}

	public async Task<string> GetCacheAccessToken()
	{
		if (!_cache.TryGetValue(CacheKey, out string token))
		{
			// Se não estiver no cache, chama a API para obter um novo token
			token = await GetAccessToken();

			// Armazena o token em cache com tempo de expiração
			var cacheEntryOptions = new MemoryCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = _tokenExpiration
			};

			_cache.Set(CacheKey, token, cacheEntryOptions);
		}

		return token;
	}

	public async Task<string> GetAccessToken()
	{
		string token = string.Empty;

		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
			"Basic",
			Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_spotifySettings.CLIENT_ID}:{_spotifySettings.CLIENT_SECRET}"))
		);

		List<KeyValuePair<string, string>> args = [
      new KeyValuePair<string, string>("grant_type", "client_credentials")
		];

		var requestBody = new FormUrlEncodedContent(args);

		var response = await _httpClient.PostAsync(_spotifySettings.API_URL_AUTH, requestBody);

		if (!response.IsSuccessStatusCode)
			return token;
		
		response.EnsureSuccessStatusCode();

		var content = await response.Content.ReadAsStringAsync();

		var result = JsonConvert.DeserializeObject<SpotifyAccessTokenResponse>(content)?.access_token;
		if (result != null)
		{
			token = result.ToString();
		}

		return token;
	}

	public async Task<string> GetArtistId(string? accessToken, string artistName)
	{
    var currentAccessToken = accessToken ?? await GetCacheAccessToken();
    
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentAccessToken);

    var response = await _httpClient.GetAsync($"{_spotifySettings.API_URL_BASE}/search?q={Uri.EscapeDataString(artistName)}&type=artist&limit=1");
    
    if (!response.IsSuccessStatusCode)
    {
      return string.Empty;
    }

		response.EnsureSuccessStatusCode();

    var objectResponse = await response.Content.ReadAsStringAsync();
    var parsedResponse = JObject.Parse(objectResponse);
    
    return (string?)parsedResponse["artists"]?["items"]?[0]?["id"] ?? string.Empty;
	}

	public async Task<List<string>> GetTopTracksByArtistId(string? accessToken, string artistId)
	{
		var currentAccessToken = accessToken ?? await GetCacheAccessToken();
		
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentAccessToken);

		var response = await _httpClient.GetAsync($"{_spotifySettings.API_URL_BASE}/artists/{artistId}/top-tracks");

		response.EnsureSuccessStatusCode();

		var content = await response.Content.ReadAsStringAsync();
		var jsonResponse = JsonConvert.DeserializeObject<SpotifyTopTracksResponse>(content);

		var tracks = jsonResponse?.Tracks;

		return tracks?
			.Select(track => track?.Name)
			.Where(name => !string.IsNullOrEmpty(name))
			.Take(5)
			.OfType<string>()
			.ToList() ?? new List<string>();
	}
}