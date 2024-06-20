using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Common.Exceptions;
using PosApi.Common;

namespace PosApi.ExceptionHandler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);
            if (exception is ValidationException validationException)
            {
                await HandleValidationExceptionAsync(httpContext, validationException);
                return true;
            }
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error",
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            var errors = exception.DetailedErrors
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

            var details = new PayoutValidationProblemDetails(exception.DetailedErrors, errors)
            {
                Instance = exception.Instance
            };
            var error = new
            {
                details.ErrorDetails.Values.FirstOrDefault()?[0].Code,
                details.ErrorDetails.Values.FirstOrDefault()?[0].Description
            };

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(error, context.RequestAborted);
        }
    }
}
