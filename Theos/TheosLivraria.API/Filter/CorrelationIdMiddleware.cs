namespace TheosLivraria.API.Filter
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        public const string Header = "X-Correlation-ID";

        public CorrelationIdMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext ctx)
        {
            var correlationId = ctx.Request.Headers.ContainsKey(Header)
                ? ctx.Request.Headers[Header].ToString()
                : Guid.NewGuid().ToString();

            ctx.Items[Header] = correlationId;
            ctx.Response.Headers[Header] = correlationId;

            using (Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId))
            {
                await _next(ctx);
            }
        }
    }
}
