using System;
using System.Collections.Generic;
using System.Text;
using Theos.Model.Model;

namespace Theos.Service.Interface
{
    public interface ILivroService
    {
        IEnumerable<Livro> BuscarLivros();
        string NovoLivro(string nome);
        string ApagarLivro(int id);
        string AtualizarLivro(int id, string nomeLivro);
        bool LivroJaExiste(string nomeLivro);

    }
}
