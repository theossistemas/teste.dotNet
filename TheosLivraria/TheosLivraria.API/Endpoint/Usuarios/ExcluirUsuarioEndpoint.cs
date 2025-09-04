using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.API.Extensions;
using TheosLivraria.Core.Command.Usuarios;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.API.Endpoint.Usuarios
{
    public class ExcluirUsuarioEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("{id}", [Authorize(Policy = "Administrador")] async ([FromRoute] Guid id, IUnitOfWork unitOfWork) =>
        {
            var commnad = new ExcluirUsuarioCommand(id);
            var handle = new ExcluirUsuarioCommandHandler(unitOfWork);
            var result = await handle.ExecuteAsync(commnad);

            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .WithName("ExcluirUsuario")
        .WithTags("Ususario");


    }
}