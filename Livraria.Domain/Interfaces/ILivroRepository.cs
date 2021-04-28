using System.Collections.Generic;
using Livraria.Domain.Entities;

namespace Livraria.Domain.Interfaces
{
    public interface ILivroRepository : IBaseRepository<Livro>
    {
        IList<Livro> GetName(string name);
         
    }
}