using Microsoft.AspNetCore.Diagnostics;
using PatientApi.Exceptions;
namespace PatientApi.ErrorHandling;


public class GlobalExceptionHandler : IExceptionHandler
{
    public void Handle(Exception exception)
    {
        // Log the exception or perform any other necessary actions
        throw new Exception("An unexpected error occurred.", exception);
    }

    public async Task<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken = default)
    {
        // Log the exception or perform any other necessary actions
        var (statusCode, message) = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, "The requested resource was not found."),
            _ => (StatusCodes.Status400BadRequest, "An unexpected error occurred.")
        };
        try
        {
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(message);
            return true;
        }
        catch
        {
            // If an exception occurs while handling the exception, we can log it or ignore it
            return false;
        }
    }
}