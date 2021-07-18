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
        public Dictionary<string, string> Erros { get; private set; } = new Dictionary<string, string>();
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
            catch (System.Exception)
            {
                this.Erros.Add("Erro", "Ocorreu um erro ao excluir um livro.");
            }

        }
    }
}
