using System.Collections.Generic;
using System.Linq;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Livraria.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Data.Repository
{
    public class LivroRepository : BaseRepository<Livro>, ILivroRepository
    {
         public LivroRepository(MSSqlContext context) : base(context){
            
        }

         public IList<Livro> GetName(string name){
            return  _dbSet.AsNoTracking().Where(el => el.Nome.Contains(name)).ToList();            
        }
        
    }
}