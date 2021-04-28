using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Theos.Model.Model;

namespace Theos.Data.Repositories.Interface
{
    public interface ILivroRepository 
    {
        IEnumerable<Livro> Livro { get; }
        void NovoLivro(Livro livro);
        void AtualizaLivro(Livro livro);
        void DeletarLivro(Livro livro);
        Livro BuscarLivroPorId(int id);
    }
}
