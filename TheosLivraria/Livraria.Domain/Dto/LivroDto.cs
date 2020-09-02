using Livraria.Domain.Entidades;

namespace Livraria.Domain.Dto
{
    public class LivroDto
    {
        public string Titulo { get; set; }
        public int AnoDePublicacao { get; set; }
        public int Edicao { get; set; }
        public AutorDto Autor { get; set; }

        public static LivroDto ConverterParaDto(Livro livro)
        {
            if (livro == null) return null;
            return new LivroDto()
            {
                AnoDePublicacao = livro.AnoDePublicacao,
                Edicao = livro.Edicao,
                Titulo = livro.Titulo,
                Autor = AutorDto.ConverterParaDto(livro.Autor)
            };
        }
    }
}
