using System;

namespace LivrariaTheos.Estoque.Domain.Dtos
{
    public class BaseDto
    {
        public int Id { get; set; }
        public string UsuarioInclusao { get; set; }
        public DateTime DataInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public BaseDto()
        {
            UsuarioInclusao = "Administrador";
            DataInclusao = DateTime.Now;
        }
    }
}