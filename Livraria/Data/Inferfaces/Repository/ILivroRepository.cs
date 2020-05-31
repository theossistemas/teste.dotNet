using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Inferfaces.Repository
{
    public interface ILivroRepository
    {
        Livro AddLivro(Livro livro);
        IEnumerable<Livro> GetAllLivro();
        Livro GetLivroById(int id);
        void RemoveLivro(int id);
        void UpdateLivro(Livro livro);
    }
}
