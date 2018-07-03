using LivrariaTemp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaTemp.Repositories
{
    public interface ILivroRepository
    {
        IEnumerable<Livro> GetAll();
        void Add(Livro livro);
        Livro Find(int id);
        void Remove(int id);
        void Update(Livro livro);        
    }
}
