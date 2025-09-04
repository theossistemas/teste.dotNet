using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.API.Extensions;
using TheosLivraria.Core.Queries.Livros;
using TheosLivraria.Infra.Configuracao;
using TheosLivraria.Infra.Repository.Livros.Query;

namespace TheosLivraria.API.Endpoint.Livros
{
    public class ObterLivroPorIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("{id}", [Authorize(Policy = "Administrador")] async ([FromRoute] Guid id, TheosApplicationDbContext context) =>
        {
            var command = new ObterLivroPorIdQuery(id);
            var handle = new ObterLivroPorIdQueryHandler(context);
            var result = await handle.ExecuteAsync(command);

            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .WithName("LivroPorId")
        .WithTags("Livro");
    }
}