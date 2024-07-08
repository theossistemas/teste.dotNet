using Gerenciador.Livraria.Core.Entities.Logs;
using Gerenciador.Livraria.Core.Interfaces.Logs;
using Gerenciador.Livraria.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.Core.Helpers.Logs
{
    public class LoggerHelper : ILoggerHelper
    {
        private readonly IRepository<LogsEntity> _logsEntityRepository;

        public LoggerHelper(IRepository<LogsEntity> logsEntityRepository)
        {
            _logsEntityRepository = logsEntityRepository;
        }

        public async Task RegistrarLog(string descricao, string tipoDeRegistro)
        {
            var log = new LogsEntity
            {
                Descricao = descricao,
                TipoDeRegistro = tipoDeRegistro,
                DataDeRegistro = DateTime.Now
            };

            await _logsEntityRepository.InsertAsync(log);
        }

        public async Task RegistrarLogDeErro(Exception exception)
        {
            var log = new LogsEntity
            {
                Descricao = exception.Message,
                TipoDeRegistro = "Erro",
                DataDeRegistro = DateTime.Now
            };

            await _logsEntityRepository.InsertAsync(log);
        }
    }
}
