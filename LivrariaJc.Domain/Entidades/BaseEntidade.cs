using System;

namespace LivrariaJc.Domain.Entidades
{
    public class BaseEntidade
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }    
        public DateTime? DataAtualizacao { get; set; }
    }
}
