using System.Collections.Generic;
using ProjetoLivraria.Domain.Entities;
using ProjetoLivraria.Domain.Interfaces.Repositories;
using System.Linq;


namespace ProjetoLivraria.Data.Repositories
{
    public class LivroRepository : BaseRepository<Livro>, ILivroRepository
    {
        public IList<Livro> LivrosOrdenados()
        {
            return Db.Livros.OrderBy(o => o.Nome).ToList();
        }
    }
}
