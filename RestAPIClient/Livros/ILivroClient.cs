using Models.DTO;
using RestAPIClient.Response;
using System;
using System.Threading.Tasks;

namespace RestAPIClient.Livros
{
    public interface ILivroClient
    {
        Task<IApiResponse> Save(LivroDTO livro, UsuarioDTO usuario);

        Task<IApiResponse> Update(Int64? id, LivroDTO livro, UsuarioDTO usuario);

        Task<IApiResponse> Find(Int64? id);

        Task<IApiResponse> Delete(Int64? id, UsuarioDTO usuario);

        Task<IApiResponse> FindAll();

        Task<IApiResponse> FindByTitle(String title);
    }
}
