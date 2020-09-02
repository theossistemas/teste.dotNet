using Livraria.Domain.Entidades;

namespace Livraria.Domain.Dto
{
    public class AutorDto
    {
        public int? Id { get; set; }
        public string Nome { get; set; }

        public static AutorDto ConverterParaDto(Autor autor)
        {
            if (autor == null) return null;
            return new AutorDto()
            {
                Id = autor.Id,
                Nome = autor.Nome
            };
        }
    }
}
