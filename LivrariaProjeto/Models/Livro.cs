using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivrariaProjeto.Models
{

    public class Livro
    {

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Autor { get; set; }

        public decimal Preco { get; set; }

        public DateTime Lancamento { get; set; } = DateTime.UtcNow;
    }
}