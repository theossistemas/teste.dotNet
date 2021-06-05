using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TesteDotNet.Business.Interfaces;
using TesteDotNet.Business.Models;
using TesteDotNet.Data.Context;

namespace TesteDotNet.Data.Repository
{
    public class LivroRepository : Repository<Livro>, ILivroRepository
    {
        public LivroRepository(DataDbContext context): base (context){}

        public async Task<IEnumerable<Livro>> ListarPorNome()
        {
            return await Db.Livros.AsNoTracking()
                 .OrderBy(c => c.Nome)
                 .ToListAsync();
        }
    }
}
