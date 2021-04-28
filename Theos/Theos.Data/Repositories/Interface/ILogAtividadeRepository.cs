using System;
using System.Collections.Generic;
using System.Text;
using Theos.Model.Model;

namespace Theos.Data.Repositories.Interface
{
    public interface ILogAtividadeRepository
    {
        void GravarAtividade(LogAtividade log);
    }
}
