using System;
using System.Collections.Generic;
using System.Text;
using Theos.Data.Repositories.Interface;
using Theos.Model.Model;
using Theos.Service.Interface;

namespace Theos.Service.Service
{
    public class LogErroService : ILogErroService
    {
        private ILogErroRepository _iLogErroRepository;
        public LogErroService(ILogErroRepository iLogErroRepository)
        {
            _iLogErroRepository = iLogErroRepository;
        }

        public void GravarErro(string erro, DateTime dataErro)
        {
            LogErro log = new LogErro();
            log.DataErro = dataErro;
            log.Erro = erro;
            _iLogErroRepository.GravarErro(log);
        }
    }
}
