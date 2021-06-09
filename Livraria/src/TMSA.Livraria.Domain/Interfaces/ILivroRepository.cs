using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMSA.Livraria.Domain.Models;

namespace TMSA.Livraria.Domain.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<Livro>> ObterLivros();
        Task<Livro> ObterLivroPorId(Guid livroId);
        Task<Livro> ObterLivroPorISBN(string isbn);
        Task CriarLivro(Livro livro);
        Task AtualizarLivro(Livro livro);
        Task RemoverLivro(Guid livroId);
    }
}
