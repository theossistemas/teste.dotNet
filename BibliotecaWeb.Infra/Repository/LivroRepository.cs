using LivrariaWeb.Domain.Interface;
using LivrariaWeb.Domain.Model;
using LivrariaWeb.Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LivrariaWeb.Infra.Repository
{
    public class LivroRepository : RepositoryBase<Livro>, ILivroRepository
    {
        public LivroRepository(DbContextOptions<ContextBdLivraria> contextBdLivraria) : base(contextBdLivraria)
        {
        }

        public bool VerificaSeLivroExiste(long id)
        {
            return _dbSet.Any(x => (x.Id == id));
        }

        public bool VerificaSeLivroJaFoiCadastrado(string nomeLivro, string nomeAutor)
        {
            return _dbSet.Any(x => (x.NomeLivro == nomeLivro && x.NomeAutor == nomeAutor));
        }
    }
}