using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MyContext _context;
        private readonly DbSet<T> _dataset;
        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }
        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(t => t.Id == id);
                if (result == null)
                    return false;
                result.Ativo = false;
                _dataset.Update(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteFisicoAsync(int id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(t => t.Id == id);
                if (result == null)
                    return false;
                result.Ativo = false;
                _dataset.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();  //Verificar AsNoTracking 
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

        public async Task<bool> DeleteList(IEnumerable<T> item)
        {
            try
            {
                _dataset.RemoveRange(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> where)
        {
            return await _dataset.AnyAsync(where);
        }

        public async Task<IEnumerable<T>> InsertListAsync(IEnumerable<T> item)
        {
            try
            {
                _dataset.AddRange(item);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }
        public async Task<IEnumerable<T>> UpdateListAsync(IEnumerable<T> item)
        {
            try
            {
                _dataset.UpdateRange(item);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (!item.Ativo)
                {
                    item.Ativo = true;
                }

                item.DataCriacao = DateTime.Now;
                _dataset.Add(item);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
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

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataset.Where(x => x.Ativo).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(t => t.Id == item.Id);
                if (result == null)
                    return null;

                item.DataAlteracao = DateTime.Now;
                item.DataCriacao = result.DataCriacao;

                _context.Entry(result).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }
    }
}
