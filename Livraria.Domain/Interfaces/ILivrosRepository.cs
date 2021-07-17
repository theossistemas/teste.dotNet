using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces
{
    public interface ILivrosRepository : IRepositoryBase<Livro>
    {
        Task Criar(Livro livro);

        Task Atualizar(Livro livro);

        Task Excluir(Livro livro);

        Task<Livro> BuscarPorId(int id);

        Task<Livro> BuscarPorNome(string nome);

        Task<IEnumerable<Livro>> ListarTodos();
    }
}
