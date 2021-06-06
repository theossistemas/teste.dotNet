using LivrariaWeb.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaWeb.Domain.Interface
{
    public interface ILivroRepository : IRepositoryBase<Livro>
    {
        bool VerificaSeLivroJaFoiCadastrado(string nomeLivro, string nomeAutor);
        bool VerificaSeLivroExiste(long id);
    }
}
