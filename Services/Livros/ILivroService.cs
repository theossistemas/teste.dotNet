using Models.DTO;
using Services.Base;
using System;
using System.Collections.Generic;

namespace Services.Livros
{
    public interface ILivroService : IService<LivroDTO>
    {
        Boolean VerificarSeLivroNaoExiste(String titulo);

        String ValidarLivro(LivroDTO livro);

        IList<LivroDTO> FindByTitle(String title);
    }
}
