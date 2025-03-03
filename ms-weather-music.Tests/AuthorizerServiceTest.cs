using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Moq.Protected;
using ms_weather_music.Models.Response;
using ms_weather_music.Services.Authorizer;
using ms_weather_music.Settings.AuthorizerSettings;

public class AuthorizerServiceTests
{
	private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
	private readonly HttpClient _httpClient;
	private readonly AuthorizerService _authorizerService;
	private readonly AuthorizerSettings _authorizerSettings;

	public AuthorizerServiceTests()
	{
		_httpMessageHandlerMock = new Mock<HttpMessageHandler>();
		_httpClient = new HttpClient(_httpMessageHandlerMock.Object);
		
		_authorizerSettings = new AuthorizerSettings
		{
			API_URL = "https://api.example.com/authorize"
		};
		
		var options = Options.Create(_authorizerSettings);
		_authorizerService = new AuthorizerService(_httpClient, options);
	}

	[Fact]
	public async Task GetAuthorizeReturnsFalseWhenResponseIsNotSuccessful()
	{
		// Arrange
		var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
		_httpMessageHandlerMock
			.Protected()
			.Setup<Task<HttpResponseMessage>>(
				"SendAsync",
				ItExpr.IsAny<HttpRequestMessage>(),
				ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(responseMessage);
		
		// Act
		var result = await _authorizerService.GetAuthorize();
		
		// Assert
		Assert.False(result);
	}

	[Fact]
	public async Task GetAuthorizeReturnsTrueWhenResponseIsSuccessfulAndStatusIsSuccess()
	{
		// Arrange
		var authorizeResponse = new AuthorizeResponse { status = "success" };
		var jsonResponse = JsonSerializer.Serialize(authorizeResponse);
		
		var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
		{
			Content = new StringContent(jsonResponse)
		};
		
		_httpMessageHandlerMock
			.Protected()
			.Setup<Task<HttpResponseMessage>>(
				"SendAsync",
				ItExpr.IsAny<HttpRequestMessage>(),
				ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(responseMessage);
		
		// Act
		var result = await _authorizerService.GetAuthorize();
		
		// Assert
		Assert.True(result);
	}

	[Fact]
	public void ConstructorThrowsArgumentNullExceptionWhenHttpClientIsNull()
	{
		// Arrange
		var options = Options.Create(_authorizerSettings);
		
		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => new AuthorizerService(null, options));
	}
}