using Api.Domain.Interfaces;
using Base.Domain.Entities.Cadastros.Base;
using Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.Repositories
{

    public class UserRepository<T> : IRepositoryUser<T> where T : ApplicationUser
    {
        private readonly MyContext _context;
        private readonly DbSet<T> _dataset;
        public UserRepository(MyContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public async Task<T> SelectWhereDefaultAsync(Expression<Func<T, bool>> where)
        {
            try
            {
                return await _dataset.Where(where).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<T> GetDataSet()
        {
            return _context.Set<T>();  //Verificar AsNoTracking 
        }
        public async Task<IEnumerable<T>> SelectWhereAsync(Expression<Func<T, bool>> where)
        {
            try
            {
                return await _dataset.Where(where).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<T> SelectAsync(int id)
        {
            try
            {
                return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
