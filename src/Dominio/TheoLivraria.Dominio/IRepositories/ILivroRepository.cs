using System.Collections.Generic;
using System.Threading.Tasks;
using TheoLivraria.Dominio.Entidades;

namespace TheoLivraria.Dominio.IRepositories
{
    public interface ILivroRepository
    {
        Task Criar(Livro livro);

        Task Atualizar(Livro livro);

        Task Excluir(Livro livro);

        Task<Livro> BuscarPorId(int id);

        Task<Livro> BuscarPorNome(string nome);

        Task<IEnumerable<Livro>> ListarTodos();
    }
}
