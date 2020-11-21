using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TheosTestAPI.Entity;
using TheosTestAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TheosTestAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        #region Properties

        protected readonly CustomResponse _response;

        private readonly ILogger<BooksController> _logger;

        #endregion Properties



        #region Routes

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IQueryable<Book> Get()
        {
            TheosTestContext context = new TheosTestContext();

            return context.Set<Book>().OrderBy(book => book.Name);
            //return context.Books.ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public Book Get(int id)
        {
            using (TheosTestContext context = new TheosTestContext())
            {
                return context.Books.Find(id);
            }
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Post([FromBody] Book book)
        {
            try
            {
                IActionResult validation = ValidateBook(book);

                if (validation != null)
                {
                    return validation;
                }

                using (TheosTestContext context = new TheosTestContext())
                {
                    context.Add(book);
                    context.SaveChanges();
                }

                _logger.LogInformation("Livro Inserido");

                return _response.Success(book);
            }
            catch (Exception e)
            {
                return HandleError(e);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            try
            {
                book.Id = id;

                IActionResult validation = ValidateBook(book);

                if (validation != null)
                {
                    return validation;
                }

                using (TheosTestContext context = new TheosTestContext())
                {
                    context.Update(book);
                    context.SaveChanges();
                }

                _logger.LogInformation("Livro Alterado");

                return _response.Success(book);
            }
            catch (Exception e)
            {
                return HandleError(e);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Delete(int id)
        {
            try
            {
                using (TheosTestContext context = new TheosTestContext())
                {
                    Book BookToDelete = context.Books.Find(id);

                    if (BookToDelete == null)
                    {
                        return _response.Validation("Livro não existe");
                    }
                    else
                    {
                        context.Remove(BookToDelete);
                        context.SaveChanges();

                        _logger.LogInformation("Livro Excluido");

                        return _response.Success("Livro deletado com sucesso");
                    }
                }
            }
            catch (Exception e)
            {
                return HandleError(e);
            }
        }

        #endregion Routes



        #region Other Methods

        public BooksController(ILogger<BooksController> logger)
        {
            _response = new CustomResponse();
            _logger = logger;
        }

        [ApiExplorerSettings(IgnoreApi = true)]

        public IActionResult ValidateBook(Book book)
        {
            //Name
            if (book.Name == null)
            {
                return _response.Validation("Nome não pode estar vazio");
            }

            book.Name = book.Name.Trim();

            if (book.Name.Length > 100 || book.Name.Length < 10)
            {
                return _response.Validation("Nome deve ter entre 10 e 100 caracteres");
            }

            using (TheosTestContext context = new TheosTestContext())
            {
                Book DoesNameExists = context.Set<Book>()
                    .Where(b => b.Name.ToLower().Trim() == book.Name.ToLower().Trim()
                    && (b.Id != book.Id || book.Id == 0)).FirstOrDefault();

                if (DoesNameExists != null)
                {
                    return _response.Validation("Livro já existe");
                }
            }

            //Author
            if (book.Author == null)
            {
                return _response.Validation("Autor não pode estar vazio");
            }

            book.Author = book.Author.Trim();

            if (book.Author.Length > 50 || book.Author.Length < 10)
            {
                return _response.Validation("Autor deve ter entre 10 e 50 caracteres");
            }

            //LauchYear
            if (book.LaunchYear <= 0)
            {
                return _response.Validation("Ano de lançamento deve ser maior que 0");
            }

            if (book.LaunchYear > DateTime.Now.Year)
            {
                return _response.Validation("Ano de lançamento ainda não aconteceu");
            }

            //Price
            if (book.Price <= 0)
            {
                return _response.Validation("Preço deve ser maior que 0");
            }

            book.Price = Math.Truncate(100 * book.Price) / 100;

            return null;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError(Exception e)
        {
            _logger.LogError(e.Message);

            return _response.Validation("Ocorreu um erro desconhecido executando a operação");
        }

        #endregion Other Methods
    }
}
