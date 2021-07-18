using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces.Repositories
{
    public interface ILivrosRepository : IRepositoryBase<Livro>
    {
        public Task Criar(Livro livro);

        public Task Atualizar(Livro livro);

        public Task Excluir(Livro livro);

        public Task<Livro> BuscarPorId(int id);

        public Task<Livro> BuscarPorNome(string nome);

        public Task<IEnumerable<Livro>> ListarTodos();
    }
}
