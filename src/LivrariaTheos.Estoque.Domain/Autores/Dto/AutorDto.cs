namespace LivrariaTheos.Estoque.Domain.Dtos
{
    public class AutorDto : BaseDto
    {
        public string Nome { get;  set; }
        public int Nacionalidade { get;  set; }
        public string InformacoesRelevantes { get;  set; }
        public bool Ativo { get; set; }       
    }
}
