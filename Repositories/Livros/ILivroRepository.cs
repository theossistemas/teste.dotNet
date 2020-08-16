using Entities;
using Repositories.Base;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Repositories.Livros
{
    public interface ILivroRepository : IRepository<Livro>
    {
        void VerificarSeLivroNaoExiste(Livro livro);

        IList<Livro> FindByTitle(String title);
    }
}