using System;
using System.Collections.Generic;
using System.Linq;
using TMSA.Livraria.Domain.Interfaces;

namespace TMSA.Livraria.Domain.Notifications
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificaoes;

        public Notificador()
        {
            _notificaoes = new List<Notificacao>();
        }

        public void Handle(Notificacao notificacao)
        {
            _notificaoes.Add(notificacao);
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificaoes;
        }

        public bool TemNotificacao()
        {
            return _notificaoes.Any();
        }
    }
}
