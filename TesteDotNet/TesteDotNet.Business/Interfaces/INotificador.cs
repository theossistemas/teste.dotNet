using System;
using System.Collections.Generic;
using System.Text;
using TesteDotNet.Business.Notificacoes;

namespace TesteDotNet.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
