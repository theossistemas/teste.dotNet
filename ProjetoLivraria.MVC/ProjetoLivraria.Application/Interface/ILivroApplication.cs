using ProjetoLivraria.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoLivraria.Application.Interface
{
    public interface ILivroApplication : IBaseApplication<Livro>
    {
        IList<Livro> LivrosOrdenados();
    }
}
