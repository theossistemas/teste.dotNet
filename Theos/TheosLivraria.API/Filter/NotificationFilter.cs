using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using TheosLivraria.Domain.Interfaces.Notification;

namespace TheosLivraria.API.Filter
{
    /// <summary>
    /// Filtro de resultado que verifica notificações de domínio e validações de modelo,
    /// retornando detalhes do problema em caso de erro.
    /// </summary>
    public class DomainNotificationFilter : IAsyncResultFilter
    {
        private readonly IDomainNotification _domainNotification;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="DomainNotificationFilter"/>.
        /// </summary>
        /// <param name="domainNotification">Serviço de notificações de domínio.</param>
        public DomainNotificationFilter(IDomainNotification domainNotification)
        {
            _domainNotification = domainNotification;
        }

        /// <summary>
        /// Executa a lógica do filtro para verificar notificações e validações antes de retornar o resultado.
        /// </summary>
        /// <param name="context">Contexto de execução do resultado.</param>
        /// <param name="next">Delegado para executar o próximo filtro.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!context.ModelState.IsValid || _domainNotification.HasNotifications)
            {
                var validations = !context.ModelState.IsValid ?
                    JsonConvert.SerializeObject(context.ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)) :
                    JsonConvert.SerializeObject(_domainNotification.Notifications
                        .Select(x => x.Value));

                var problemDetails = new ProblemDetails
                {
                    Title = "Bad Request",
                    Status = StatusCodes.Status400BadRequest,
                    Instance = context.HttpContext.Request.Path.Value,
                    Detail = validations
                };

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/problem+json";

                using var writer = new StreamWriter(context.HttpContext.Response.Body);
                new JsonSerializer().Serialize(writer, problemDetails);
                await writer.FlushAsync();

                return;
            }

            await next();
        }
    }
}
