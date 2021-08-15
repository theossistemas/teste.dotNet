namespace LivrariaTheos.Estoque.Domain.Dtos
{
    public class LivroDto : BaseDto
    {
        public int AutorId { get; set; }
        public int GeneroId { get; set; }
        public string Nome { get;  set; }
        public string Sinopse { get;  set; }
        public int QuantidadePaginas { get;  set; }
        public string ImagemCapaBase64 { get; set; }
        public bool Ativo { get;  set; }
    }
}