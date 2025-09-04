using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Domain.Interface;
using TheosLivraria.Infra.Configuracao;
using TheosLivraria.Infra.Repository.Base;

namespace TheosLivraria.Infra.Repository.Logs
{
    public class LogRepository(TheosApplicationDbContext context) :RepositoryBase<int, Log>(context), ILogRepository
    {
    }
}
