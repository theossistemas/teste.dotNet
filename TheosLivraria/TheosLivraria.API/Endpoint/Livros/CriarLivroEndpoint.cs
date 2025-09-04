using Microsoft.AspNetCore.Authorization;
using TheosLivraria.API.Extensions;
using TheosLivraria.Core.Command.Livros;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.API.Endpoint.Livros
{
    public class CriarLivroEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("", [Authorize(Policy = "Administrador")] async (CriarLivroCommand request, IUnitOfWork unitOfWork) =>
        {
            var handle = new CriarLivroCommandHandler(unitOfWork);
            var result = await handle.ExecuteAsync(request);

            return result.Success ? Results.Created("", result) : Results.BadRequest(result);
        })
        .WithName("CriarLivro")
        .WithTags("Livro");
    }
}
