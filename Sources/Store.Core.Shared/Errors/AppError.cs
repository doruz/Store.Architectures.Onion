namespace Store.Core.Shared;

public sealed class AppError(int statusCode, string errorCode) : Exception
{
    public int StatusCode { get; private init; } = statusCode;
    public string ErrorCode { get; private init; } = errorCode;
    public object ErrorDetails { get; private set; } = new { };

    public AppError WithDetails(object details)
    {
        ErrorDetails = details;
        return this;
    }

    public static AppError NotFound(string errorCode, string id) => new(404, errorCode)
    {
        ErrorDetails = new { id }
    };

    public static AppError Conflict(string error, string id) => new(409, error)
    {
        ErrorDetails = new { id }
    };
}