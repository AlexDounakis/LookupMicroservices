using BatchProcessing.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BatchProcessing.Handlers
{
    public class IPServiceNotAvailableExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<IPServiceNotAvailableExceptionHandler> _logger;

        public IPServiceNotAvailableExceptionHandler(ILogger<IPServiceNotAvailableExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not IPServiceNotAvailableException badRequestException)
            {
                return false;
            }

            _logger.LogError(
                badRequestException,
                "Exception occurred: {Message}",
                badRequestException.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status503ServiceUnavailable,
                Title = "Service Unavailable",
                Detail = badRequestException.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
