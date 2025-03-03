namespace ms_weather_music.Models.Response;

public class SpotifyAccessTokenResponse
{
	public string? access_token { get; set; }
	public string? token_type { get; set; }
	public int? expires_in { get; set; }
	public string? scope { get; set; }
}

public class SpotifyTopTracksResponse
{
  public List<Track>? Tracks { get; set; }
}

public class Track
{
	public Album? Album { get; set; }
	public List<Artist>? Artists { get; set; }
	public List<string>? AvailableMarkets { get; set; }
	public int DiscNumber { get; set; }
	public int DurationMs { get; set; }
	public bool Explicit { get; set; }
	public ExternalIds? ExternalIds { get; set; }
	public ExternalUrls? ExternalUrls { get; set; }
	public string? Href { get; set; }
	public string? Id { get; set; }
	public bool IsPlayable { get; set; }
	public LinkedFrom? LinkedFrom { get; set; }
	public Restrictions? Restrictions { get; set; }
	public string? Name { get; set; }
	public int Popularity { get; set; }
	public string? PreviewUrl { get; set; }
	public int TrackNumber { get; set; }
	public string? Type { get; set; }
	public string? Uri { get; set; }
	public bool IsLocal { get; set; }
}

public class Album
{
	public string? AlbumType { get; set; }
	public int TotalTracks { get; set; }
	public List<string>? AvailableMarkets { get; set; }
	public ExternalUrls? ExternalUrls { get; set; }
	public string? Href { get; set; }
	public string? Id { get; set; }
	public List<Image>? Images { get; set; }
	public string? Name { get; set; }
	public string? ReleaseDate { get; set; }
	public string? ReleaseDatePrecision { get; set; }
	public Restrictions? Restrictions { get; set; }
	public string? Type { get; set; }
	public string? Uri { get; set; }
	public List<Artist>? Artists { get; set; }
}

public class Artist
{
	public ExternalUrls? ExternalUrls { get; set; }
	public string? Href { get; set; }
	public string? Id { get; set; }
	public string? Name { get; set; }
	public string? Type { get; set; }
	public string? Uri { get; set; }
}

public class Image
{
	public string? Url { get; set; }
	public int Height { get; set; }
	public int Width { get; set; }
}

public class ExternalIds
{
	public string? Isrc { get; set; }
	public string? Ean { get; set; }
	public string? Upc { get; set; }
}

public class ExternalUrls
{
	public string? Spotify { get; set; }
}

public class LinkedFrom
{
	// You can include properties here if needed in the future
}

public class Restrictions
{
	public string? Reason { get; set; }
}