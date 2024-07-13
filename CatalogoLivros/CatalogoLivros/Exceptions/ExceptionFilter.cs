using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CatalogoLivros.Exceptions
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is JaExisteCadastroException)
            {
                _logger.LogWarning("Ocorreu uma exceção do tipo JaExisteCadastroException: {Mensagem}", context.Exception.Message);

                context.Result = new ConflictObjectResult(new { mensagem = context.Exception.Message });
                context.ExceptionHandled = true;
            }
            else
            {
                _logger.LogError(context.Exception, "Ocorreu um erro de exceção não tratada: Status Code 500");

                context.Result = new ObjectResult(new { mensagem = "Um erro inesperado ocorreu." })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
