using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.API.Extensions;
using TheosLivraria.Core.Command.Usuarios;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.API.Endpoint.Usuarios
{
    public class AlterarStatusUsuarioEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPatch("alterar-status/{id}", [Authorize(Policy = "Administrador")] async ([FromRoute] Guid id, IUnitOfWork unitOfWork) =>
        {
            var command = new AlterarStatusUsuarioCommand(id);
            var handle = new AlterarStatusUsuarioCommandHandler(unitOfWork);
            var result = await handle.ExecuteAsync(command);

            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .WithName("AlterarStatusUsuario")
        .WithTags("Ususario");


    }
}
