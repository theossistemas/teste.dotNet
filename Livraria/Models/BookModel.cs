using Livraria.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Livraria.Models
{
    public class BookModel
    {
        public class BookViewModel
        {
            public List<Author> Authors { get; set; }
            public Book SingleBook { get; set; }
            public List<Book> ListBooks { get; set; }
            public List<Publisher> Publishers { get; set; }
            public BookViewModel()
            {
                SingleBook = new Book();
                ListBooks = new List<Book>();
                Authors = new List<Author>();
                Publishers = new List<Publisher>();
            }
        }

        public class BookRegisterModel
        {
            public Guid Id { get; set; }

            [Required]
            [Display(Name = "Título do Livro")]
            public string Title { get; set; }

            [Required]
            [Display(Name = "Nome do Autor")]
            public string AuthorId { get; set; }

            [Required]
            [Display(Name = "Editora")]
            public string PublisherId { get; set; }
            public string Country { get; set; }
            public string Language { get; set; }
            public int? Year { get; set; }
            public int? ISBN { get; set; }
            public int? Edition { get; set; }
            public int? Pages { get; set; }

            [Required]
            public int Stock { get; set; }
            public decimal Price { get; set; }
            public string BookInfo { get; set; }
        }
    }
}
