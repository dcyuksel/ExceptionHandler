using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandler.ExceptionHandlers;

public class NotImplementedExceptionHandler(ILogger<NotImplementedExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not NotImplementedException)
        {
            return false;
        }

        logger.LogError(exception, "NotImplementedException is occured with message: {message}.", exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "InternalServerError",
            Detail = exception.Message + $" It is handled by {nameof(NotImplementedExceptionHandler)}."
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
