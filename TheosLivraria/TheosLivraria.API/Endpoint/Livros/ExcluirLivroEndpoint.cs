using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.API.Extensions;
using TheosLivraria.Core.Command.Livros;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.API.Endpoint.Livros
{
    public class ExcluirLivroEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("{id}", [Authorize(Policy = "Administrador")] async ([FromRoute] Guid id, IUnitOfWork unitOfWork) =>
        {
            var commnad = new ExcluirLivroCommand(id);
            var handle = new ExcluirLivroCommandHandler(unitOfWork);
            var result = await handle.ExecuteAsync(commnad);

            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .WithName("ExcluirLivro")
        .WithTags("Livro");


    }
}