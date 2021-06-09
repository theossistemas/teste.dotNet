using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMSA.Livraria.Domain.Interfaces;
using TMSA.Livraria.Domain.Models;
using TMSA.Livraria.Domain.Models.Validations;

namespace TMSA.Livraria.Domain.Services
{
    public class LivroService : BaseService, ILivroServices
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository,
                            INotificador notificador) : base(notificador)
        {
            _livroRepository = livroRepository;
        }

        public async Task AtualizarLivro(Livro livro)
        {
            if (!ExecutarValidacao(new LivroValidations(), livro)) return;

            await _livroRepository.AtualizarLivro(livro);
        }

        public async Task CriarLivro(Livro livro)
        {
            if (!ExecutarValidacao(new LivroValidations(), livro)) return;

            if (_livroRepository.ObterLivroPorISBN(livro.ISBN).Result != null)
            {
                Notificar("Já existe este livro cadastrado.");
                return;
            }

            await _livroRepository.CriarLivro(livro);
        }

        public async Task<Livro> ObterLivroPorId(Guid livroId)
        {
            return await _livroRepository.ObterLivroPorId(livroId);
        }

        public async Task<Livro> ObterLivroPorISBN(string isbn)
        {
            return await _livroRepository.ObterLivroPorISBN(isbn);
        }

        public async Task<IEnumerable<Livro>> ObterLivros()
        {
            return await _livroRepository.ObterLivros();
        }

        public async Task RemoverLivro(Guid livroId)
        {
            await _livroRepository.RemoverLivro(livroId);
        }
    }
}
