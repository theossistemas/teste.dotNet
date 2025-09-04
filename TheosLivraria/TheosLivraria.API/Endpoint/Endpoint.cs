using TheosLivraria.API.Endpoint.Livros;
using TheosLivraria.API.Endpoint.Seguranca;
using TheosLivraria.API.Endpoint.Usuarios;
using TheosLivraria.API.Extensions;

namespace TheosLivraria.API.Endpoint
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("/api");
            MapVersionedEndPoints(endpoints, "v1");
        }

        public static void MapVersionedEndPoints(IEndpointRouteBuilder endpoints, string version)
        {

            endpoints.MapGroup($"{version}/usuarios")
                .WithTags("Usuário V1")
                .MapEndpoint<CriarUsuarioEndpoint>()
                .MapEndpoint<AlterarStatusUsuarioEndpoint>()
                .MapEndpoint<AlterarUsuarioEndpoint>()
                .MapEndpoint<ExcluirUsuarioEndpoint>()
                .MapEndpoint<ObterUsuariosGridEndpoint>()
                .MapEndpoint<ObterUsuarioPorIdEndpoint>();


            endpoints.MapGroup($"{version}/livros")
               .WithTags("Livro V1")
               .MapEndpoint<CriarLivroEndpoint>()
               .MapEndpoint<AlterarStatusLivroEndpoint>()
               .MapEndpoint<AlterarLivroEndpoint>()
               .MapEndpoint<ExcluirLivroEndpoint>()
               .MapEndpoint<ObterLivrosGridEndpoint>()
               .MapEndpoint<ObterLivroPorIdEndpoint>();

            endpoints.MapGroup($"{version}/seguranca")
               .WithTags("Seguranca V1")
               .MapEndpoint<LoginEndpoint>();

        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder builder) where TEndpoint : IEndpoint
        {
            TEndpoint.Map(builder);
            return builder;
        }
    }
}
