using System.Text.RegularExpressions;
using ms_weather_music.Models.Response;

namespace ms_weather_music.Models.Utils;

public class CityNameValidator
{
  public ValidatorResponse IsValidCityName(string cityName)
  {
    if (string.IsNullOrWhiteSpace(cityName))
    {
			return new ValidatorResponse(
				ExceptionMessageResponse.CityNameNullOrEmpty,
				true
			);
    }

    if (cityName.Length > 100)
    {
			return new ValidatorResponse(
				ExceptionMessageResponse.CityNameExceptedCaracteres,
				true
			);
    }

    if (!Regex.IsMatch(cityName, @"^[a-zA-ZÀ-ÿ-,\s]+$"))
    {
			return new ValidatorResponse(
				ExceptionMessageResponse.CityNameCaracteresInvalids,
				true
			);
    }

		return new ValidatorResponse(
			"",
			false
		);
  }

	public class ValidatorResponse(string errorMessage, bool statusError)
	{
		public string ErrorMessage { get; set; } = errorMessage;
		public bool StatusError { get; set; } = statusError;
	}
}