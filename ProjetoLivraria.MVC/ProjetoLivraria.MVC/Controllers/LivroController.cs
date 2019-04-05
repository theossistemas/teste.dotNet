using ProjetoLivraria.Application.Interface;
using ProjetoLivraria.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProjetoLivraria.MVC.Controllers
{
    public class LivroController : ApiController
    {

        private readonly ILivroApplication _livroApp;

        public LivroController(ILivroApplication livroApp)
        {
            _livroApp = livroApp;

        }


        // GET: api/Livro
        public IEnumerable<Livro> Get()
        {
            return _livroApp.LivrosOrdenados();
        }

        // GET: api/Livro/5
        public IHttpActionResult Get(int id)
        {
            var book = _livroApp.FindGetById(id);

            if (book != null)
                return Ok(book);
            return NotFound();
        }

        // POST: api/Livro
        [HttpPost]
        public void Post(Livro value)
        {
            if (value.IdLivro > 0)
            {
                var book = _livroApp.FindGetById(value.IdLivro);
                if (book != null)
                {
                    book.Autor = value.Autor;
                    book.Ano = value.Ano;
                    book.Categoria = value.Categoria;
                    book.Editora = value.Editora;
                    book.Nome = value.Nome;
                    _livroApp.Update(book);
                }
                
            }
            else
                _livroApp.Add(value);
        }

        
        // DELETE: api/Livro/5
        public IHttpActionResult Delete(int id)
        {
            var book = _livroApp.FindGetById(id);

            if (book != null)
                _livroApp.Remove(book);
            else
              return  NotFound();

            return Ok();
        }
    }
}
