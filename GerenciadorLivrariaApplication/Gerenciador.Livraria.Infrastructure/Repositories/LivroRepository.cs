using Gerenciador.Livraria.Core.Entities.Livraria;
using Gerenciador.Livraria.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Gerenciador.Livraria.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.Infrastructure.Repositories
{
    public class LivroRepository : Repository<LivroEntity>, ILivroRepository
    {
        public LivroRepository(LivrariaDbContext livrariaDbContext) : base(livrariaDbContext)
        {

        }

        public async Task<IEnumerable<LivroEntity>> GetAllIncluded()
        {
            return await _dbSet.Include(x => x.Autor)
                               .Include(x => x.Categoria)
                               .ToListAsync();
        }
    }
}
