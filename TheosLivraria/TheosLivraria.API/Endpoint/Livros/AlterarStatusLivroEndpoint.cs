using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.API.Extensions;
using TheosLivraria.Core.Command.Livros;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.API.Endpoint.Livros
{
    public class AlterarStatusLivroEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPatch("alterar-status/{id}", [Authorize(Policy = "Administrador")] async ([FromRoute] Guid id, IUnitOfWork unitOfWork) =>
        {
            var command = new AlterarStatusLivroCommand(id);
            var handle = new AlterarStatusLivroCommandHandler(unitOfWork);
            var result = await handle.ExecuteAsync(command);

            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .WithName("AlterarStatusLivro")
        .WithTags("Livro");


    }
}
