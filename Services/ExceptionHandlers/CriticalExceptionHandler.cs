using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace App.Services.ExceptionHandlers
{
    public class CriticalExceptionHandler() : IExceptionHandler
    {
        ValueTask<bool> IExceptionHandler.TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is CriticalException)
            {
                Console.WriteLine("Critical Exception Occurred");
            }

            return ValueTask.FromResult(false);

        }
    }
}
