using livraria.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace livraria.Web.ViewModel
{
    public class LivroRequestVM
    {
        public LivroResponseVM Livro { get; set; }
        public int Id { get; set; }
    }
}
