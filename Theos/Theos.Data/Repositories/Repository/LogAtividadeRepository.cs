using System;
using System.Collections.Generic;
using System.Text;
using Theos.Data.Contexto;
using Theos.Data.Repositories.Interface;
using Theos.Model.Model;

namespace Theos.Data.Repositories.Repository
{
    public class LogAtividadeRepository : ILogAtividadeRepository
    {
        private readonly AppDbContext _contexto;
        public LogAtividadeRepository(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public void GravarAtividade(LogAtividade log)
        {
            _contexto.Add(log);
            _contexto.SaveChanges();
        }
    }
}
