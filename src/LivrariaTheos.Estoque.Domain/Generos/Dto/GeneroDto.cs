using System.Collections.Generic;

namespace LivrariaTheos.Estoque.Domain.Dtos
{
    public class GeneroDto : BaseDto
    {
        public string Nome { get; set; }       
        public bool Ativo { get; set; }        
    }
}