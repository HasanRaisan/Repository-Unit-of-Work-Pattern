using System.Diagnostics;

namespace TeacherStudentAPI.Midlleware
{
    /// <summary>
    /// Middleware to log detailed performance metrics for each HTTP request.
    /// Logs elapsed time, memory usage, and CPU usage.
    /// </summary>
    public class AdvancedProfilingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AdvancedProfilingMiddleware> _logger;

        public AdvancedProfilingMiddleware(RequestDelegate next, ILogger<AdvancedProfilingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();


            // Memory usage at the start
            long memoryBefore = GC.GetTotalMemory(false);

            // CPU usage snapshot (optional, simple approximation)
            var process = Process.GetCurrentProcess();
            TimeSpan cpuStart = process.TotalProcessorTime;

            // Execute the next middleware
            await _next(context);

            stopwatch.Stop();

            // Memory usage after request
            long memoryAfter = GC.GetTotalMemory(false);
            long memoryUsed = memoryAfter - memoryBefore;

            // CPU usage after request
            TimeSpan cpuEnd = process.TotalProcessorTime;
            double cpuUsedMs = (cpuEnd - cpuStart).TotalMilliseconds;

            // Log the metrics
            _logger.LogInformation(
                "Request {Path} took {ElapsedMilliseconds}ms, Memory Used: {MemoryUsed} bytes, CPU Time: {CpuUsed} ms",
                context.Request.Path,
                stopwatch.ElapsedMilliseconds,
                memoryUsed,
                cpuUsedMs
            );
        }
    }
}
