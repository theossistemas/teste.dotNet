using System;
using System.Collections.Generic;
using System.Text;

namespace Theos.Service.Interface
{
   public interface ILogErroService
    {
        void GravarErro(string erro, DateTime dataErro);
    }
}
