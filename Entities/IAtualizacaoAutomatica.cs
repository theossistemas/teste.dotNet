using System;
using Entities;

namespace Entities
{
    public interface IAtualizacaoAutomatica
    {
        String ComandoCriar { get; set; }

        String ComandoExcluir { get; set; }

        Versao Versao { get; set; }
    }
}