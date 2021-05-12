using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheoLivraria.WebAPI.Models
{
    public class LivroViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é requerido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} é requerido")]
        public string Editora { get; set; }

       
    }
}
