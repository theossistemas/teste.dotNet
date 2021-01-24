using MaiaraBookstore.Data;
using MaiaraBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaiaraBookstore.Repository.LogRepository
{
    public class LogBookRepository : IRepository<LogBookstore>
    {
        private DataContext _dataContext;
        public LogBookRepository(DataContext DataContext) 
        {
            _dataContext = DataContext;
        }
        public void Delete(LogBookstore logBookstore)
        {
            _dataContext.LogBookstores.Remove(logBookstore);
        }

        public LogBookstore FindById(int id)
        {
            return _dataContext.LogBookstores.SingleOrDefault(l => l.Id == id);
        }

        public void Save(LogBookstore logBookstore)
        {
            _dataContext.LogBookstores.Add(logBookstore);
        }

        public void UpDate(LogBookstore logBookstore)
        {
            _dataContext.LogBookstores.Update(logBookstore);
        }
    }
}
