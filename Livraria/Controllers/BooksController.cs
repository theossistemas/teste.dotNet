using AutoMapper;
using Livraria.Data;
using Livraria.Data.Utils;
using Livraria.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Livraria.Models.BookModel;
using static System.Reflection.MethodBase;

namespace Livraria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorUtils _error;

        public BooksController(
            ApplicationDbContext context,
            ErrorUtils error)
        {
            _context = context;
            _error = error;
        }

        [HttpGet]
        [Route("ListBooks")]
        public async Task<ActionResult<BookViewModel>> ListBooks()
        {
            return new BookViewModel
            {
                ListBooks = await _context.Books
                                 .Include(b => b.Author)
                                 .Include(b => b.Publisher)
                                 .ToListAsync()
            };
        }

        [HttpGet]
        [Route("GetBook")]
        public async Task<ActionResult<BookViewModel>> GetBook(Guid bookId)
        {
            if (!BookExistsById(bookId))
            {
                var errorMessage = "Livro não encontrado";
                var function = GetType().Name.ToString() + "|GetBook";
                await _error.GenerateErrorLog(errorMessage, function);
                TempData["error"] = errorMessage;
                return RedirectToAction("Index", "Home");
            }
            BookViewModel iView = await GetBookByIdAsync(bookId);
            return View(iView);
        }

        [HttpPost]
        [Route("FindBook")]
        [AllowAnonymous]
        public async Task<ActionResult<BookViewModel>> FindBook([FromForm] string bookTitle)
        {
            if (!BookExistsByTitleUsingContains(bookTitle))
            {
                var errorMessage = "Livro não encontrado";
                var function = GetType().Name.ToString() + "|GetBook";
                await _error.GenerateErrorLog(errorMessage, function);
                TempData["error"] = errorMessage;
                return RedirectToAction("Index", "Home");
            }
            BookViewModel iView = await GetBookByTitleAsync(bookTitle);
            return RedirectToAction("Index", "Home", new BookViewModel { ListBooks = iView.ListBooks });
        }

        [HttpGet]
        [Route("EditBook")]
        public async Task<ActionResult<BookViewModel>> EditBook(Guid bookId)
        {
            if (!BookExistsById(bookId))
            {
                var errorMessage = "Livro não encontrado";
                var function = GetType().Name.ToString() + "|GetBook";
                await _error.GenerateErrorLog(errorMessage, function);
                TempData["error"] = errorMessage;
                return RedirectToAction("Index", "Home");
            }
            BookViewModel iView = await GetBookByIdAsync(bookId);
            return View(iView);
        }

        [HttpPost]
        [Authorize]
        [Route("EditBook")]
        public async Task<IActionResult> EditBook([FromForm] BookRegisterModel model)
        {
            if (!BookExistsById(model.Id))
            {
                var errorMessage = "Livro não encontrado";
                var function = GetType().Name.ToString() + "|DeleteBook";
                await _error.GenerateErrorLog(errorMessage, function);
                TempData["error"] = errorMessage;
                return RedirectToAction("EditBook", "Books", new { bookId = model.Id });
            }
            else
            {
                Book iBook = await _context.Books.FirstOrDefaultAsync(b => b.Id.Equals(model.Id));
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BookRegisterModel, Book>()
                        .ForMember(x => x.CreatedAt, opt => opt.MapFrom(o => iBook.CreatedAt))
                        .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(o => DateTime.Now));
                });
                IMapper mapper = configuration.CreateMapper();
                Book editedBook = mapper.Map(model, iBook);
                _context.Entry(editedBook).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Livro editado com sucesso";
                    return RedirectToAction("Index", "Home");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var errorMessage = "Erro ao atualizar o banco de dados -> " + ex.Message;
                    var function = GetType().Name.ToString() + "|DeleteBook";
                    await _error.GenerateErrorLog(errorMessage, function);
                    TempData["error"] = errorMessage;
                    return RedirectToAction("EditBook", "Books", new { bookId = model.Id });
                }
            }
        }

        [HttpPost]
        [Authorize]
        [Route("CreateBook")]
        public async Task<ActionResult> CreateBook([FromForm] BookRegisterModel model)
        {
            if (BookExistsByTitleUsingEqual(model.Title))
            {
                var errorMessage = "Livro já cadastrado";
                var function = GetType().Name.ToString() + "|DeleteBook";
                await _error.GenerateErrorLog(errorMessage, function);
                return BadRequest(errorMessage);
            }

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BookRegisterModel, Book>()
                    .ForMember(x => x.CreatedAt, opt => opt.MapFrom(o => DateTime.Now))
                    .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(o => DateTime.Now))
                    .ForMember(x => x.AuthorId, opt => opt.MapFrom(o => model.AuthorId))
                    .ForMember(x => x.PublisherId, opt => opt.MapFrom(o => model.PublisherId));
            });
            IMapper mapper = configuration.CreateMapper();
            Book newBook = mapper.Map(model, new Book());
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return Ok(newBook);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [Route("DeleteBook")]
        public async Task<IActionResult> DeleteBook(Guid bookId)
        {
            if (!BookExistsById(bookId))
            {
                var errorMessage = "Livro não encontrado";
                var function = GetType().Name.ToString() + "|DeleteBook";
                await _error.GenerateErrorLog(errorMessage, function);
                TempData["error"] = errorMessage;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var book = await _context.Books.FindAsync(bookId);
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            TempData["success"] = "Livro removido com sucesso";
            return RedirectToAction("Index", "Home");
        }

        private bool BookExistsById(Guid id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

        private bool BookExistsByTitleUsingContains(string bookName)
        {
            return _context.Books.Any(e => e.Title.Contains(bookName));
        }

        private bool BookExistsByTitleUsingEqual(string bookName)
        {
            return _context.Books.Any(e => e.Title.Equals(bookName));
        }

        private async Task<BookViewModel> GetBookByIdAsync(Guid bookId)
        {
            return new BookViewModel
            {
                SingleBook = await _context.Books
                                   .Include(b => b.Author)
                                   .Include(b => b.Publisher)
                                   .FirstOrDefaultAsync(b => b.Id.Equals(bookId)),
                Authors = await _context.Authors.ToListAsync(),
                Publishers = await _context.Publishers.ToListAsync()
            };
        }

        private async Task<BookViewModel> GetBookByTitleAsync(string bookName)
        {
            return new BookViewModel
            {
                ListBooks = await _context.Books
                                   .Include(b => b.Author)
                                   .Include(b => b.Publisher)
                                   .Where(b => b.Title.Equals(bookName))
                                   .ToListAsync(),
            };
        }
    }
}
