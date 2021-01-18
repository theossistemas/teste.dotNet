using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using theos_books.Data;
using theos_books.Models;

namespace theos_books.Business
{
    public class BookBO
    {
        private readonly ApplicationDbContext _context;
        private LogBO logBO;

        public BookBO(ApplicationDbContext context)
        {
            _context = context;
            logBO = new LogBO(context);
        }

        public async Task<Book> Save(Book book, ApplicationUser user)
        {
            try
            {
                Book result = new Book();
                if (_context.Books.Count(b => b.Title.Contains(book.Title) || b.ISBN.Contains(book.ISBN)) < 1)
                {
                    book.Genre = _context.Genres.Find(book.Genre.Id);
                    book.Publisher = _context.Publishers.Find(book.Publisher.Id);
                    await _context.Books.AddAsync(book);
                    await _context.SaveChangesAsync();
                    result = await _context.Books.Include(b => b.Genre).Include(b => b.Publisher).FirstOrDefaultAsync(b => b.Id == book.Id);
                    this.logBO.saveLog("Livro " + result.Title + " salvo com sucesso !", user);
                }
                return result;
            }
            catch (Exception e)
            {
                this.logBO.saveError(e.Message, user);
                throw;
            }
        }

        public async Task<Book> Edit(Book book, ApplicationUser user)
        {
            try
            {
                _context.Entry(book).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                this.logBO.saveLog("Livro " + book.Title + " editado com sucesso !", user);
                return await _context.Books.Include(b => b.Genre).Include(b => b.Publisher).FirstOrDefaultAsync(b => b.Id == book.Id);
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
                var book = await _context.Books.FindAsync(id);
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                this.logBO.saveLog("Livro " + book.Title + " excluido com sucesso !", user);
            }
            catch (Exception e)
            {
                this.logBO.saveError(e.Message, user);
                throw;
            }
        }
    }
}
