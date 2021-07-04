using Livraria.Domain.Entities.Administracao;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Services.Interfaces.Administracao
{
    public interface ILogService
    {
        Task<List<Log>> ConsultarTodos();
    }
}
