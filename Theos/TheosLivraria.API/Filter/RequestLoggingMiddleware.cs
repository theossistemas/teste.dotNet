using System.Diagnostics;

namespace TheosLivraria.API.Filter
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            var path = ctx.Request.Path;
            var method = ctx.Request.Method;
            var correlationId = ctx.Items[CorrelationIdMiddleware.Header]?.ToString();

            _logger.LogInformation("Request started {Method} {Path} CorrelationId:{CorrelationId}", method, path, correlationId);

            var sw = Stopwatch.StartNew();
            await _next(ctx);
            sw.Stop();

            _logger.LogInformation("Request finished {Method} {Path} StatusCode:{StatusCode} ElapsedMs:{ElapsedMs} CorrelationId:{CorrelationId}",
                method, path, ctx.Response.StatusCode, sw.ElapsedMilliseconds, correlationId);
        }
    }
}
