using TheosLivraria.Domain.Interfaces.Logs;

namespace TheosLivraria.API.Filter
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next; _logger = logger;
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (Exception ex)
            {
                var correlationId = ctx.Items[CorrelationIdMiddleware.Header]?.ToString();
                _logger.LogError(ex, "Unhandled exception. CorrelationId:{CorrelationId}", correlationId);

                // opcional: gravar na tabela AuditLogs via scoped service:
                var auditService = ctx.RequestServices.GetService<IAuditService>();
                if (auditService != null)
                {
                    await auditService.LogErrorAsync(ex, ctx.Request.Path, correlationId);
                }

                ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
                ctx.Response.ContentType = "application/json";
                var payload = new { Message = "Erro interno", CorrelationId = correlationId };
                await ctx.Response.WriteAsJsonAsync(payload);
            }
        }
    }
}
