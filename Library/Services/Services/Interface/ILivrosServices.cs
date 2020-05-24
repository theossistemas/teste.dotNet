using Persistence.Entity;
using System.Collections.Generic;

namespace Business.Services.Interface
{
    public interface ILivrosServices
    {
        void AddLivro(Livro livro);
        Livro EditLivro(int id, Livro livro);
        IEnumerable<Livro> GetAll();
        Livro GetById(int id);
        bool Save();
        void RemoveLivro(int id);
    }
}
