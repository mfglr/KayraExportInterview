using Microsoft.AspNetCore.Diagnostics;
using Shared.Exceptions;

namespace AuthService.Api.Middlewares
{
    internal class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = exception switch
            {
                ValidationException => 400,
                NotFoundException => 404,
                _ => 500,
            };

            await httpContext.Response.WriteAsJsonAsync(new {exception.Message},cancellationToken);

            return true;
        }
    }
}
