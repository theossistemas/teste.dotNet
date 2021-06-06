using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteDotNet.Business.Interfaces;
using TesteDotNet.Business.Models;

namespace TesteDotNet.Business.Services
{
    public class LivroService : BaseService, ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository,
                         INotificador notificador) : base(notificador)
        {
            _livroRepository = livroRepository;
        }

        public async Task<bool> Adicionar(Livro livro)
        {
            if (_livroRepository.Buscar(l => l.Nome == livro.Nome).Result.Any())
            {
                Notificar("Esse livro ja foi cadastrado");
                return false;
            }

            await _livroRepository.Adicionar(livro);
            return true;
        }

        public void Dispose()
        {
            _livroRepository?.Dispose();
        }
    }
}
