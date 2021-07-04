using Livraria.Domain.Entities.Administracao;
using Livraria.Infra.Data.Interfaces.Repositories.Administracao;
using Livraria.Services.Interfaces.Administracao;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Services.Administracao
{
    public class LogService : ILogService
    {
        private readonly ILogRepositorio _logRepositorio;

        public LogService(ILogRepositorio logRepositorio)
        {
            _logRepositorio = logRepositorio;
        }

        public async Task<List<Log>> ConsultarTodos()
        {
            return await _logRepositorio.GetAll().ToListAsync();
        }
    }
}
