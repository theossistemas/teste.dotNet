using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.API.Extensions;
using TheosLivraria.Core.Queries.Ususarios;
using TheosLivraria.Domain.Interface;
using TheosLivraria.Infra.Configuracao;
using TheosLivraria.Infra.Repository.Usuarios.Query;

namespace TheosLivraria.API.Endpoint.Usuarios
{
    public class ObterUsuariosGridEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("", [Authorize(Policy = "Administrador")] async (TheosApplicationDbContext context) =>
        {
            var command = new ObterUsusariosQuery();
            var handle = new ObterUsusariosQueryHandler(context);
            var result = await handle.ExecuteAsync(command);

            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .WithName("ListarUsuarios")
        .WithTags("Ususario");


    }
}
