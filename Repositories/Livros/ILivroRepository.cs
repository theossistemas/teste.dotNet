using Entities;
using Repositories.Base;
using System;
using System.Collections.Generic;

namespace Repositories.Livros
{
    public interface ILivroRepository : IRepository<Livro>
    {
        Boolean VerificarSeLivroNaoExiste(String titulo);

        IList<Livro> FindByTitle(String title);
    }
}