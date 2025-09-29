namespace Store.Core.Business.Errors;

public sealed class BusinessException(BusinessError error) : Exception
{
    public BusinessError Error { get; } = error;

    public int StausCode => Error.Status;
}