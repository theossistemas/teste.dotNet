using livraria.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace livraria.Web.ViewModel
{
    public class LivroResponseVM
    {
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public float Valor { get; set; }
        public Autor Autor { get; set; }
        public Categoria Categoria { get; set; }
        public int AutorId { get; set; }
        public int CategoriaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
