using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.API.Extensions;
using TheosLivraria.Core.Command.Livros;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.API.Endpoint.Livros
{
    public class AlterarLivroEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("{id}", [Authorize(Policy = "Administrador")] async ([FromRoute] Guid id, AlterarLivroCommand request, IUnitOfWork unitOfWork) =>
        {
            var command = request with { Id = id };
            var handle = new AlterarLivroCommandHandler(unitOfWork);
            var result = await handle.ExecuteAsync(command);

            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .WithName("AlterarLivro")
        .WithTags("Livro");


    }
}