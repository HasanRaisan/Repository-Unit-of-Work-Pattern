using System.Collections.Concurrent;

namespace Clean_Three_Tier_First.Midlleware
{
    /// <summary>
    /// Middleware to limit the number of requests per client IP within a defined time window.
    /// Each IP has its own counter and reset time.
    /// </summary>
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RateLimitingMiddleware> _logger;

        // Concurrent dictionary to store request count and last request time per IP
        private static readonly ConcurrentDictionary<string, (int Count, DateTime LastRequest)> _clients
            = new ConcurrentDictionary<string, (int Count, DateTime LastRequest)>();

        private const int MAX_REQUESTS = 3;           // Maximum requests allowed
        private const int TIME_WINDOW_SECONDS = 10;   // Time window in seconds

        public RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        //  The middleware method name should be InvokeAsync or Invoke 
        public async Task InvokeAsync(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown"; //?. → means “if not null, access this property.” ?? → means “if the result is null, use this value instead.” 
             
            var now = DateTime.UtcNow;

            // Get previous client state or initialize if new
            var (count, lastRequest) = _clients.GetOrAdd(clientIp, (0, now));

            // Reset counter if the time window has passed
            if ((now - lastRequest).TotalSeconds > TIME_WINDOW_SECONDS)
            {
                count = 1;
                lastRequest = now;
            }
            else
            {
                count++;
            }

            // UpdateAsync the dictionary
            _clients[clientIp] = (count, lastRequest);

            // If client exceeded max requests, return 429
            if (count > MAX_REQUESTS)
            {
                var traceId = context.TraceIdentifier; // unique trace ID for this request
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.Response.ContentType = "text/plain";

                var message = $"Too many requests. Please try again later. TraceId: {traceId}";

                // Log the rate limit violation
                _logger.LogWarning("Rate limit exceeded for IP {IP}. TraceId: {TraceId}", clientIp, traceId);

                await context.Response.WriteAsync(message);
                return; // Stop further processing
            }

            // Allow request to continue
            await _next(context);
        }
    }
}
