using Livraria.Application.DTOs;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Livraria.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public async Task<Guid> CreateBookAsync(CreateBookDTO createBookDto)
        {
            var existingBook = await _bookRepository.GetByTitleAsync(createBookDto.Title);
            if (existingBook != null && existingBook.Title == createBookDto.Title && existingBook.Author == createBookDto.Author && existingBook.PublicationYear == createBookDto.PublicationYear)
            {
                _logger.LogWarning("Ocorreu uma tentativa de criação de um livro existente: {0}, {1}, {2}", createBookDto.Title, createBookDto.Author, createBookDto.PublicationYear);
                throw new InvalidOperationException(string.Format("Um livro inserido já existe na base de dados.", createBookDto.Title));
            }

            try
            {
                var book = Book.Create(
                    createBookDto.Title,
                    createBookDto.Author,
                    createBookDto.PublicationYear
                );

                await _bookRepository.AddAsync(book);
                await _bookRepository.SaveChangesAsync();

                _logger.LogInformation("Livro criado com sucesso, ID: {0}", book.Id);
                return book.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateBookAsync(Guid id, UpdateBookDTO updateBookDto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                throw new InvalidOperationException("Livro não encontrado.");
            }

            var titleToUpdate = updateBookDto.Title == "" ? book.Title : updateBookDto.Title;
            var authorToUpdate = updateBookDto.Author == "" ? book.Author : updateBookDto.Author;
            var publicationYearToUpdate = updateBookDto.PublicationYear == 0 ? book.PublicationYear : updateBookDto.PublicationYear;

            book.Update(titleToUpdate, authorToUpdate, publicationYearToUpdate);
            _bookRepository.Update(book);
            await _bookRepository.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                throw new InvalidOperationException("Livro não encontrado.");
            }
            _bookRepository.Delete(book);
            await _bookRepository.SaveChangesAsync();
        }

        public async Task<BookDTO?> GetBookByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;
            return MapToDTO(book);
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllSortedByNameAsync();
            return books.Select(MapToDTO).ToList();
        }

        private static BookDTO MapToDTO(Book book)
        {
            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublicationYear = book.PublicationYear
            };
        }
    }
}