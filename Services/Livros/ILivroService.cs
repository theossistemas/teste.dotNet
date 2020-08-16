using Models.DTO;
using Services.Base;
using System;
using System.Collections.Generic;

namespace Services.Livros
{
    public interface ILivroService : IService<LivroDTO>
    {
        void VerificarSeLivroNaoExiste(LivroDTO livro);

        IList<LivroDTO> FindByTitle(String title);
    }
}
