using ProjetoLivraria.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoLivraria.Domain.Interfaces.Services
{
    public interface ILivroService : IBaseService<Livro>
    {
        IList<Livro> LivrosOrdenados();
    }
}
