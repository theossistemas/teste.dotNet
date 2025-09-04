using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheosLivraria.Domain.Entidades
{
    public class Log : EntidadeBase<int>
    {
        public TipoLog Tipo { get; private set; }
        public string Origem { get; private set; }
        public string? DadosJson { get; private set; }      
        public int? UsuarioId { get; private set; }    
        public virtual Usuario? Usuario { get; private set; }


        public Log(
            TipoLog tipo,
            string origem,
            string? dadosJson = null,
            int? usuarioId = null
            )
        {
            Tipo = tipo;
            Origem = origem;
            DadosJson = dadosJson;
            UsuarioId = usuarioId;
            DataCriacao = DateTime.UtcNow;
            Ativo = true;
        }

    }
}
