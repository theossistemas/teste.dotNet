using MaiaraBookstore.Data;
using MaiaraBookstore.Models;
using MaiaraBookstore.Repository.LivroRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaiaraBookstore.Services
{
    public class LivroServiceImpl : ILivroService
    {
        private LivroRepository _livroRepository;

        public LivroServiceImpl()
        {
            _livroRepository = new LivroRepository(new DataContext());
        }
        public bool ValidaSeTituloDeLivroEstaCadastrado(string titulo)
        {
            Livro livro = _livroRepository.FindByTitulo(titulo);
            if (livro == null)
            {
                return false;
            }
            return true;
        }
    }
}
