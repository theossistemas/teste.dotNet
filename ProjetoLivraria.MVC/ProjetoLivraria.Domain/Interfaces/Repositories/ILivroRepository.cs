using ProjetoLivraria.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoLivraria.Domain.Interfaces.Repositories
{
    public interface ILivroRepository : IBaseRepository<Livro>
    {
        IList<Livro> LivrosOrdenados();

    }
}
