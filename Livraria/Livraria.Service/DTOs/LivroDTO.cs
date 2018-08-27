using System;

namespace Livraria.Service.DTOs
{
    public class LivroDTO
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public AutorDTO Autor { get; set; }
        public EditoraDTO Editora { get; set; }
        public int? Edicao { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
