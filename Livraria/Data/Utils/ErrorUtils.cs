using Livraria.Entities;
using System;
using System.Threading.Tasks;

namespace Livraria.Data.Utils
{
    public class ErrorUtils
    {
        private readonly ApplicationDbContext _db;
        public ErrorUtils(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task GenerateErrorLog(string errorMessage, string function)
        {
            ErrorLog errorLog = new ErrorLog
            {
                Description = errorMessage,
                Function = function,
                CreatedAt = DateTime.Now
            };
            await _db.ErrorLogs.AddAsync(errorLog);
            await _db.SaveChangesAsync();
        }
    }
}
