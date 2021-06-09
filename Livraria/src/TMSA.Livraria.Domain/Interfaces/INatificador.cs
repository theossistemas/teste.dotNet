using System.Collections.Generic;
using TMSA.Livraria.Domain.Notifications;

namespace TMSA.Livraria.Domain.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
