using Livraria.Domain;
using Livraria.Domain.Interfaces;
using Livraria.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Services.LivrosServices
{
    public class AlterarLivro
    {
        public Dictionary<string, string> Erros { get; private set; } = new Dictionary<string, string>();
        private readonly ILivrosRepository _livrosRepository;

        public AlterarLivro(ILivrosRepository livrosRepository)
        {
            _livrosRepository = livrosRepository;
        }

        public async Task Alterar(int id, Livro livro)
        {
            try
            {
                var RLivro = await _livrosRepository.BuscarPorId(id);
                RLivro.AtualizarDadosDoLivro(livro.Nome);

                await _livrosRepository.Atualizar(RLivro);
            }
            catch (Exception)
            {
                this.Erros.Add("Erro", "Ocorreu um erro ao alterar um livro.");
            }

        }
    }
}
