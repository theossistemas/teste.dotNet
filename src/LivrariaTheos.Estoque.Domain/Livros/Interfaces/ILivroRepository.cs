using LivrariaTheos.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaTheos.Estoque.Domain.Livros.Interfaces
{
    public interface ILivroRepository : IRepository<Livro>
    {
        Task<Livro> ObterPorId(int id);
        Task<IEnumerable<Livro>> ObterPorNome(string nome);
        Task<IEnumerable<Livro>> ObterTodosAtivos();
        Task<IEnumerable<Livro>> ObterTodos();
        Task<IEnumerable<Livro>> ObterPorAutor(int autorId);
        Task<IEnumerable<Livro>> ObterPorGenero(int generoId);
        Task<IEnumerable<Livro>> ObterPorNacionalidadeDoAutor(int nacionalidadeAutor);

        void Adicionar(Livro livro);
        void Atualizar(Livro livro);
        void Excluir(Livro livro);
    }
}