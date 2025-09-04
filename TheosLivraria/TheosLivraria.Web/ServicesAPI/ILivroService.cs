using Refit;
using TheosLivraria.Web.Request.Livros;
using TheosLivraria.Web.Util;
using TheosLivraria.Web.Util.Response.Livro;


namespace TheosLivraria.Web.ServicesAPI
{
    public interface ILivroService
    {
        [Post("/api/v1/livros")]
        Task<Retorno<int>> Criar([Body] CriarLivroRequest request);

        [Get("/api/v1/livros")]
        Task<Retorno<List<LivrosGridDTO>>> ObterTodos();

        [Get("/api/v1/livros/{id}")]
        Task<Retorno<LivroDTO>> ObterPorId(Guid Id);

        [Put("/api/v1/livros/{id}")]
        Task<Retorno<bool>> Alterar(string id, [Body] AlterarLivroRequest request);

        [Delete("/api/v1/livros/{id}")]
        Task<Retorno<bool>> Delete(Guid Id);
    }
}
