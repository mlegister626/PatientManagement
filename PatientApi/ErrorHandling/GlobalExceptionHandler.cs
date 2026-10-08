using Microsoft.AspNetCore.Diagnostics;
using PatientApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
namespace PatientApi.ErrorHandling;


public class GlobalExceptionHandler : Microsoft.AspNetCore.Diagnostics.IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken = default)
    {
        var (statusCode, message) = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
            ArgumentException => (StatusCodes.Status400BadRequest, exception.Message),
            InvalidOperationException => (StatusCodes.Status409Conflict, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = statusCode,
            Title = statusCode switch
            {
                StatusCodes.Status400BadRequest => "Invalid request",
                StatusCodes.Status404NotFound => "Resource not found",
                StatusCodes.Status409Conflict => "Request conflict",
                _ => "Server error"
            },
            Detail = message
        }, cancellationToken);

        return true;
    }
}