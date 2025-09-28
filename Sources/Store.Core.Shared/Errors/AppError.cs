namespace Store.Core.Shared;

public sealed class AppError : Exception
{
    public int StatusCode { get; private init; }
    public string ErrorCode { get; private init; }
    public string? Id { get; private init; }

    private AppError(int statusCode, string errorCode)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }


    public static AppError Conflict(string error, string id) => new(409, error) { Id = id };
}