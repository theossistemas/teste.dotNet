using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using theos_books.Data;
using theos_books.Models;

namespace theos_books.Business
{
    public class LogBO
    {
        private readonly ApplicationDbContext _context;

        public LogBO(ApplicationDbContext context)
        {
            _context = context;
        }

        public void saveLog(string message, ApplicationUser user)
        {
            var log = new Log();
            log.Message = message;
            log.Type = Models.Type.Log;
            log.CreateAt = DateTime.Now;
            log.User = user;
            _context.Add(log);
            _context.SaveChanges();
        }

        public void saveError(string message, ApplicationUser user)
        {
            var log = new Log();
            log.Message = message;
            log.Type = Models.Type.Error;
            log.CreateAt = DateTime.Now;
            _context.Add(log);
            _context.SaveChangesAsync();
        }
    }
}
