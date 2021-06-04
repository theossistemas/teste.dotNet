using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TesteDotNet.Business.Interfaces;
using TesteDotNet.Business.Models;
using TesteDotNet.Data.Context;

namespace TesteDotNet.Data.Repository
{
    public class LivroRepository : Repository<Livro>, ILivroRepository
    {
        public LivroRepository(DataDbContext context): base (context){}
        public Task<IEnumerable<Livro>> ListarPorNome()
        {
            throw new NotImplementedException();
        }
    }
}
