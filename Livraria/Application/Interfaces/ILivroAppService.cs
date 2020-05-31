using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Application.Interfaces
{
    public interface ILivroAppService
    {
        Livro AddLivro(Livro livro);
        IEnumerable<Livro> GetAllLivro();
        Livro GetLivroById(int id);
        void RemoveLivro(int id);
        void UpdateLivro(Livro livro);
    }
}
