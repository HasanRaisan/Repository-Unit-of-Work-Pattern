using System.Net;
using System.Text.Json;

namespace Clean_Three_Tier_First.Midlleware
{

    /* 
     The ExceptionHandlingMiddleware catches and logs all unhandled exceptions in your ASP.NET Core app.
    When an error occurs anywhere (controller, service, or database), it prevents the app from crashing,
    logs the error details, and returns a clean JSON response with status code 500.
    To work properly, it must be placed first in the middleware pipeline.
    Handled exceptions (inside try/catch blocks) won’t reach it. 
     */

    /// <summary>
    /// Middleware to globally handle unhandled exceptions safely in production.
    /// Logs detailed info internally and returns a safe JSON response with a trace ID.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Generate trace ID for tracking
                var traceId = context.TraceIdentifier;

                // Log error details including trace ID
                _logger.LogError(
                    ex,
                    "Unhandled exception occurred while processing request {Path}.\nTraceId: {TraceId}",
                    context.Request.Path,
                    traceId
                );

                // Prepare safe response
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "An internal server error occurred. Please try again later.",
                    TraceId = traceId
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }
}


