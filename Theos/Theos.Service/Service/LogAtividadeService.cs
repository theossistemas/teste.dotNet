using System;
using System.Collections.Generic;
using System.Text;
using Theos.Data.Repositories.Interface;
using Theos.Model.Model;
using Theos.Service.Interface;

namespace Theos.Service.Service
{
    public class LogAtividadeService : ILogAtividadeService
    {
        private ILogAtividadeRepository _logAtividadeRepository;

        public LogAtividadeService(ILogAtividadeRepository logAtividadeRepository)
        {
            _logAtividadeRepository = logAtividadeRepository;
        }
        public void GravarAtividade(string funcao)
        {
            LogAtividade log = new LogAtividade();
            log.DataAcesso = DateTime.Now;
            log.FuncaoAcessada = funcao;
            _logAtividadeRepository.GravarAtividade(log);

        }
    }
}
