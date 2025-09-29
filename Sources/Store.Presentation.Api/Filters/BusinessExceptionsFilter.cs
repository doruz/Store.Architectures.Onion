using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Core.Business.Errors;

/// <summary>
/// In case a specific app error is thrown to return correct status code to the client.
/// </summary>
internal sealed class BusinessExceptionsFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BusinessException exception)
        {
            context.Result = new ObjectResult(exception.Error)
            {
                StatusCode = exception.StausCode
            };

            context.ExceptionHandled = true;
        }
    }
}