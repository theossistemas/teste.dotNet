using System.Collections.Generic;
using ProjetoLivraria.Domain.Entities;
using ProjetoLivraria.Domain.Interfaces.Repositories;
using ProjetoLivraria.Domain.Interfaces.Services;

namespace ProjetoLivraria.Domain.Services
{
    public class LivroService : BaseService<Livro>, ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
            : base(livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public IList<Livro> LivrosOrdenados()
        {
            return _livroRepository.LivrosOrdenados();
        }
    }
}
