using Microsoft.AspNetCore.Diagnostics;
using Shared.Exceptions;

namespace ProductService.Api.Middlewares
{
    internal class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not AppException)
            {
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsJsonAsync(new { Message = "Something went wrong! Please try again." }, cancellationToken);
                logger.LogError(exception, "Unhandled exception occurred");
            }
            else
            {
                httpContext.Response.StatusCode = exception switch
                {
                    ValidationException => StatusCodes.Status400BadRequest,
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status403Forbidden
                };
                await httpContext.Response.WriteAsJsonAsync(new { exception.Message }, cancellationToken);
                logger.LogWarning(exception.Message);
            }
            return true;
        }
    }
}
