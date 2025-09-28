using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Azure.Cosmos;
using Store.Core.Shared;

/// <summary>
/// In case a specific app error is thrown to return correct status code to the client.
/// </summary>
internal sealed class AppErrorFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is AppError appError)
        {
            context.Result = new ObjectResult(new { appError.Id, appError.ErrorCode })
            {
                StatusCode = appError.StatusCode
            };

            context.ExceptionHandled = true;
        }
    }
}