using Entities;
using Repositories.Base;

namespace Repositories.Livros
{
    public interface ILivroRepository : IRepository<Livro>
    {
        void VerificarSeLivroNaoExiste(Livro livro);
    }
}