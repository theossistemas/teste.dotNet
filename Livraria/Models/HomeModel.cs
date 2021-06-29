using Livraria.Entities;
using System.Collections.Generic;

namespace Livraria.Models
{
    public class HomeModel
    {
        public class HomeViewModel
        {
            public List<Author> ListAuthors { get; set; }
            public List<Book> ListBooks { get; set; }
            public List<Publisher> ListPublishers { get; set; }
            public HomeViewModel()
            {
                ListAuthors = new List<Author>();
                ListBooks = new List<Book>();
                ListPublishers = new List<Publisher>();
            }
        }
    }
}
