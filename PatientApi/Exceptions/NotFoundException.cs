using Microsoft.AspNetCore.Diagnostics;
namespace PatientApi.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {

    }

    public async Task<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken = default)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await context.Response.WriteAsync("The requested resource was not found.");
        return true;
    }
}