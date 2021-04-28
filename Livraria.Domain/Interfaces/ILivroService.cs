using System;
using System.Collections.Generic;
using Livraria.Domain.Entities;

namespace Livraria.Domain.Interfaces
{
    public interface ILivroService: IDisposable
    {
        IList<Livro> GetName(string name);
        Livro Insert(Livro obj);
        Livro Update(Livro obj);
        void Delete(int id);
        IList<Livro> Select();
        Livro Select(int id);
    }
}