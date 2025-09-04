using Microsoft.AspNetCore.Authorization;
using TheosLivraria.API.Extensions;
using TheosLivraria.Core.Queries.Livros;
using TheosLivraria.Infra.Configuracao;
using TheosLivraria.Infra.Repository.Livros.Query;

namespace TheosLivraria.API.Endpoint.Livros
{
    public class ObterLivrosGridEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("", [Authorize(Roles = "Administrador, Publico")] async (TheosApplicationDbContext context) =>
        {
            var command = new ObterLivrosQuery();
            var handle = new ObterLivrosQueryHandler(context);
            var result = await handle.ExecuteAsync(command);

            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        })
        .WithName("ListarLivros")
        .WithTags("Livro");

    }
}