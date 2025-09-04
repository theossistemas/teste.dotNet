using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheosLivraria.Domain.Entidades
{
    public abstract class EntidadeBase<Tid> where Tid : struct
    {
       
        public Tid Id { get; protected set; }
        public Guid Uuid { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime? DataAtualizacao { get; protected set; }
        public bool Ativo { get; protected set; }

        protected EntidadeBase()
        {
            Uuid = Guid.NewGuid();
            DataCriacao = DateTime.UtcNow;
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Ativar()
        {
            Ativo = true;
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}
