using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ms_weather_music.Settings.OpenWeatherMapSettings;
using ms_weather_music.Settings.SpotifySettings;
using ms_weather_music.Services.Authorizer;
using ms_weather_music.Services.OpenWeatherMap;
using ms_weather_music.Services.Spotify;
using ms_weather_music.Services.WeatherMusic;
using ms_weather_music.Settings.AuthorizerSettings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AuthorizerSettings>(options =>
{
    builder.Configuration.GetSection("AuthorizerSettings").Bind(options);  
});

builder.Services.Configure<OpenWeatherMapSettings>(options =>
{
    builder.Configuration.GetSection("OpenWeatherMapSettings").Bind(options);  
});

builder.Services.Configure<SpotifySettings>(options =>
{
    builder.Configuration.GetSection("SpotifySettings").Bind(options);  
});

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	c.IncludeXmlComments(xmlPath);

	c.SwaggerDoc("v1", new() { Title = "ms-weather-music", Version = "v1" });
});

builder.Services.AddScoped<IWeatherMusicService, WeatherMusicService>();

builder.Services.AddHttpClient<IAuthorizerService, AuthorizerService>();
builder.Services.AddHttpClient<IOpenWeatherMapService, OpenWeatherMapService>();
builder.Services.AddHttpClient<ISpotifyService, SpotifyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();