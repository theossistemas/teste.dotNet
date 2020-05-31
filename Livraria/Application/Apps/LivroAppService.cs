using Livraria.Application.Interfaces;
using Livraria.Inferfaces.Repository;
using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Application.Apps
{
    public class LivroAppService : ILivroAppService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroAppService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public Livro AddLivro(Livro livro)
        {
            return _livroRepository.AddLivro(livro);
        }

        public IEnumerable<Livro> GetAllLivro()
        {
            return _livroRepository.GetAllLivro();
        }

        public Livro GetLivroById(int id)
        {
            return _livroRepository.GetLivroById(id);
        }

        public void RemoveLivro(int id)
        {
            _livroRepository.RemoveLivro(id);
        }

        public void UpdateLivro(Livro livro)
        {
            _livroRepository.UpdateLivro(livro);
        }
    }
}
