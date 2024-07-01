using LivrariaJc.Domain.Entidades;
using LivrariaJc.Domain.Input;
using LivrariaJc.Domain.Interface.Repositories;
using LivrariaJc.Domain.Output;
using System.Threading.Tasks;

namespace LivrariaJc.Domain.Interfaces.Repositories
{
    public interface ILivroRepositories : IBaseRepositories<LivrosEntidade>
    {
        Task<PagedQuery<LivrosEntidade>> ObterPaginadoAsync(LivroFilterInput input);
        Task<bool> VerificaLivroCadastrado(string titulo, int? id = null);
    }
}
