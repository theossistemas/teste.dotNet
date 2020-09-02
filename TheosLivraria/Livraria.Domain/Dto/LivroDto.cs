using Livraria.Domain.Entidades;

namespace Livraria.Domain.Dto
{
    public class LivroDto
    {
        public int? Id { get; set; }
        public string Titulo { get; set; }
        public int AnoDePublicacao { get; set; }
        public int Edicao { get; set; }
        public int AutorId { get; set; }
        public AutorDto Autor { get; set; }

        public static LivroDto ConverterParaDto(Livro livro)
        {
            if (livro == null) return null;
            return new LivroDto()
            {
                Id = livro.Id,
                AnoDePublicacao = livro.AnoDePublicacao,
                Edicao = livro.Edicao,
                Titulo = livro.Titulo,
                AutorId = livro.Autor.Id,
                Autor = AutorDto.ConverterParaDto(livro.Autor)
            };
        }
    }
}
