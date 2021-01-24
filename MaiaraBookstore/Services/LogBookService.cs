using MaiaraBookstore.Data;
using MaiaraBookstore.Models;
using MaiaraBookstore.Repository.LogRepository;

namespace MaiaraBookstore.Services
{
    public class LogBookService
    {
        private LogBookRepository _logBookRepository;
        public LogBookService(DataContext dataContext) 
        {
            _logBookRepository = new LogBookRepository(dataContext);
        }

        public void SalvarLog(Livro livro, string descricaoLog) 
        {
            _logBookRepository.Save(new LogBookstore(descricaoLog, livro));
        }

        public void SalvarLog(string descricaoLog)
        {
            _logBookRepository.Save(new LogBookstore(descricaoLog));
        }
    }
}
