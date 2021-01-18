using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using theos_books.Data;
using theos_books.Models;

namespace theos_books.Business
{
    public class GenresBO
    {
        private readonly ApplicationDbContext _context;
        private LogBO logBO;

        public GenresBO(ApplicationDbContext context)
        {
            _context = context;
            logBO = new LogBO(context);
        }

        public async Task<Genre> Save(Genre genre, ApplicationUser user)
        {
            try
            {
                await _context.Genres.AddAsync(genre);
                await _context.SaveChangesAsync();
                genre = await _context.Genres.FindAsync(genre.Id);
                this.logBO.saveLog("Genero " + genre.Name + " salvo com sucesso !", user);
                return genre;
            }
            catch (Exception e)
            {
                this.logBO.saveError(e.Message, user);
                throw;
            }
        }

        public async Task<Genre> Edit(Genre genre, ApplicationUser user)
        {
            try
            {
                _context.Entry(genre).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                this.logBO.saveLog("Genero " + genre.Name + " editada com sucesso !", user);
                return await _context.Genres.FindAsync(genre.Id);
            }
            catch (Exception e)
            {
                this.logBO.saveError(e.Message, user);
                throw;
            }
        }

        public async Task Delete(int id, ApplicationUser user)
        {
            try
            {
                var genre = await _context.Genres.FindAsync(id);
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
                this.logBO.saveLog("Genero " + genre.Name + " excluida com sucesso !", user);
            }
            catch (Exception e)
            {
                this.logBO.saveError(e.Message, user);
                throw;
            }
        }
    }
}
