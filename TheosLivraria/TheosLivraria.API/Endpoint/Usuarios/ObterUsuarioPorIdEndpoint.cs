using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.API.Extensions;
using TheosLivraria.Core.Queries.Ususarios;
using TheosLivraria.Infra.Configuracao;
using TheosLivraria.Infra.Repository.Usuarios.Query;

namespace TheosLivraria.API.Endpoint.Usuarios
{
    public class ObterUsuarioPorIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("{id}", [Authorize(Policy = "Administrador")] async ([FromRoute] Guid id, TheosApplicationDbContext context) =>
        {
            var command = new ObterUsuarioPorIdQuery(id);
            var handle = new ObterUsuarioPorIdQueryHandler(context);
            var result = await handle.ExecuteAsync(command);

            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .WithName("UsuarioPorId")
        .WithTags("Ususario");


    }
}
