using Livraria.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livraria.Models
{
    public class AuthorModel
    {
        public class AuthorViewModel
        {
            public List<Author> Authors { get; set; }
            public AuthorViewModel()
            {
                Authors = new List<Author>();
            }
        }

        public class AuthorRegisterModel
        {
            public Guid Id { get; set; }

            [Required]
            [Display(Name = "Nome do Autor")]
            public string Name { get; set; }

            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            [Column(TypeName = "DateTime2")]
            public DateTime CreatedAt { get; set; }

            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            [Column(TypeName = "DateTime2")]
            public DateTime ModifiedAt { get; set; }
        }
    }
}
