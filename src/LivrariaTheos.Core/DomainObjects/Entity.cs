using FluentValidation;
using System;

namespace LivrariaTheos.Core.DomainObjects
{
    public abstract class Entity<TId, TEntity> :
        AbstractValidator<TEntity>
        where TId : struct
        where TEntity : Entity<TId, TEntity>
    {
        public TId Id { get; protected set; }      
        public string UsuarioInclusao { get; protected set; }
        public DateTime DataInclusao { get; protected set; }
        public string UsuarioAlteracao { get; protected set; }
        public DateTime? DataAlteracao { get; protected set; }       

        public abstract void Validar();

        protected Entity()
        {
            Id = default(TId);
            DataInclusao = DateTime.Now;
            UsuarioInclusao = "Administrador"; 
        }

        public void AlterarDataAlteracao(DateTime dataAlteracao)
        {
            DataAlteracao = dataAlteracao;
        }

        public void AlterarUsuarioAlteracao(string usuarioAlteracao)
        {
            UsuarioAlteracao = usuarioAlteracao;
        }
    }
}