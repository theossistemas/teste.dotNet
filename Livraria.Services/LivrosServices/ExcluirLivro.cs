using Livraria.Domain;
using Livraria.Domain.Interfaces;
using Livraria.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Services.LivrosServices
{
    public class ExcluirLivro
    {

        private readonly ILivrosRepository _livrosRepository;

        public ExcluirLivro(ILivrosRepository livrosRepository)
        {
            _livrosRepository = livrosRepository;
        }

        public async Task Executar(Livro livro)
        {
            try
            {
                await _livrosRepository.Excluir(livro);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
