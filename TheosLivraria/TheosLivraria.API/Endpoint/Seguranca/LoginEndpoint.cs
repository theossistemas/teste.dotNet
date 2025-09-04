using TheosLivraria.API.Extensions;
using TheosLivraria.Core.Command.Seguranca;
using TheosLivraria.Core.Command.Usuarios;
using TheosLivraria.Core.Sergurancao;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.API.Endpoint.Seguranca
{
    public class LoginEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/login", async (LoginCommand command, IUnitOfWork unitOfWork, ITokenService tokenService, ILogger<LoginEndpoint> logger) =>
        {
            logger.BeginScope("Iniciando endpoint de login");
            var handle = new LoginCommandHandler(unitOfWork, tokenService);
            var result = await handle.ExecuteAsync(command);
            logger.LogInformation("Login attempt for email: {Email} - Success: {Success}", command.Email, result.Success);
            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .WithName("Login")
        .WithTags("Seguranca");

    }
}
