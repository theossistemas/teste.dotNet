using Microsoft.AspNetCore.Authorization;
using TheosLivraria.API.Extensions;
using TheosLivraria.Core.Command.Usuarios;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.API.Endpoint.Usuarios
{
    public class CriarUsuarioEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("", [Authorize(Policy = "Administrador")] async (CriarUsuarioCommand request, IUnitOfWork unitOfWork) =>
        {
            var handle = new CriarUsuarioCommandHandler(unitOfWork);
            var result = await handle.ExecuteAsync(request);

            return result.Success ? Results.Created("", result) : Results.BadRequest(result);
        })
        .WithName("CriarUsuario")
        .WithTags("Ususario");
        
       
    }
}
