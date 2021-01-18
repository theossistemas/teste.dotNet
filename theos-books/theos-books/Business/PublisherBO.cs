using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using theos_books.Data;
using theos_books.Models;

namespace theos_books.Business
{
    public class PublisherBO
    {
        private readonly ApplicationDbContext _context;
        private LogBO logBO;

        public PublisherBO(ApplicationDbContext context)
        {
            _context = context;
            logBO = new LogBO(context);
        }

        public async Task<Publisher> Save(Publisher publisher, ApplicationUser user)
        {
            try
            {
                await _context.Publishers.AddAsync(publisher);
                await _context.SaveChangesAsync();
                publisher = await _context.Publishers.FindAsync(publisher.Id);
                this.logBO.saveLog("Editora " + publisher.Name + " salvo com sucesso !", user);
                return publisher;
            }
            catch (Exception e)
            {
                this.logBO.saveError(e.Message, user);
                throw;
            }
        }

        public async Task<Publisher> Edit(Publisher publisher, ApplicationUser user)
        {
            try
            {
                _context.Entry(publisher).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                this.logBO.saveLog("Editora " + publisher.Name + " editada com sucesso !", user);
                return await  _context.Publishers.FindAsync(publisher.Id);
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
                var publisher = await _context.Publishers.FindAsync(id);
                _context.Publishers.Remove(publisher);
                await _context.SaveChangesAsync();
                this.logBO.saveLog("Editora " + publisher.Name + " excluida com sucesso !", user);
            }
            catch (Exception e)
            {
                this.logBO.saveError(e.Message, user);
                throw;
            }
        }

    }
}
