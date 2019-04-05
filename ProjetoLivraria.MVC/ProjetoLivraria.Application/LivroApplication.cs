using ProjetoLivraria.Application.Interface;
using ProjetoLivraria.Domain.Entities;
using ProjetoLivraria.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLivraria.Application
{
    public class LivroApplication : BaseApplication<Livro>, ILivroApplication
    {
        private readonly ILivroService _livroService;

        public LivroApplication(ILivroService livroService)
            : base(livroService)
        {
            _livroService = livroService;
        }

        public IList<Livro> LivrosOrdenados()
        {
            return _livroService.LivrosOrdenados();
        }
    }
}
