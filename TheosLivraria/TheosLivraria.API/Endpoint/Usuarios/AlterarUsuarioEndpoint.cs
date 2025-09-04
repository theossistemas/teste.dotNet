using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.API.Extensions;
using TheosLivraria.Core.Command.Usuarios;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.API.Endpoint.Usuarios
{
    public class AlterarUsuarioEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("{id}", [Authorize(Policy = "Administrador")] async ([FromRoute]Guid id, AlterarUsuarioCommand request, IUnitOfWork unitOfWork) =>
        {
            var command = request with { Id = id };
            var handle = new AlterarUsuarioCommandHandler(unitOfWork);
            var result = await handle.ExecuteAsync(command);

            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .WithName("AlterarUsuario")
        .WithTags("Ususario");


    }
}
