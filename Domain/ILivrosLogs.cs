using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface ILivrosLogs
    {
        LivroLogs ToLivroLog(Livros livro, string action);
    }
}
