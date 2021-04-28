using System;
using System.Collections.Generic;
using System.Text;
using Theos.Model.Model;

namespace Theos.Data.Repositories.Interface
{
    public interface ILogErroRepository
    {
        void GravarErro(LogErro log);
    }
}
