using System;
using System.Collections.Generic;
using System.Text;
using Theos.Data.Contexto;
using Theos.Data.Repositories.Interface;
using Theos.Model.Model;

namespace Theos.Data.Repositories.Repository
{
    public class LogErroRepository : ILogErroRepository
    {
        private readonly AppDbContext _contexto;
        public LogErroRepository(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public void GravarErro(LogErro log)
        {
            _contexto.Add(log);
            _contexto.SaveChanges();
        }
    }
}
