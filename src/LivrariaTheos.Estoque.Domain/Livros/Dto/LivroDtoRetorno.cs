using LivrariaTheos.Estoque.Domain.Dtos;

namespace LivrariaTheos.Estoque.Domain.Livros.Dto
{
    public class LivroDtoRetorno : BaseDto
    {
        public int AutorId { get; set; }
        public int GeneroId { get; set; }
        public string Nome { get; set; }
        public string Sinopse { get; set; }
        public int QuantidadePaginas { get; set; }
        public string CaminhoCapa { get; set; }
        public string NomeCapa { get; set; }
        public bool Ativo { get; set; }

        public virtual AutorDto Autor { get; set; }
        public virtual GeneroDto Genero { get; set; }
    }
}
