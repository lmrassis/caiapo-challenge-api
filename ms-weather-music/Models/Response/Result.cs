namespace ms_weather_music.Models.Response;

public class Result<T>
{
	public bool IsSuccess { get; set; }
	public string ErrorMessage { get; set; } = String.Empty;
	public T? Data { get; set; }

	public Result(bool isSuccess, T data, string errorMessage)
	{
		IsSuccess = isSuccess;
		Data = data;
		ErrorMessage = errorMessage;
	}
	
	public Result(bool isSuccess)
	{
		IsSuccess = isSuccess;
	}
	
	public static Result<T> Success(T data) => new Result<T>(true, data, string.Empty);
	public static Result<T> Failure(string errorMessage) => new Result<T>(false) { ErrorMessage = errorMessage };
}