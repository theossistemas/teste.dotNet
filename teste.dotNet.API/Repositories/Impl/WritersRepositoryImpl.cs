using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using teste.dotNet.API.Data;
using teste.dotNet.API.DTOs.Request;
using teste.dotNet.API.DTOs.Response;
using teste.dotNet.API.Entities;

namespace teste.dotNet.API.Repository.Impl {
    public class WritersRepositoryImpl : WritersRepository {
        
        
        private ApplicationDbContext _context;

        public WritersRepositoryImpl(ApplicationDbContext context)
        {
            _context = context;
        }
        public WriterResponseDTO Get(int writerId)
        {
            var writer =_context.Writers
            .Where(writer => writer.Id == writerId)
            .Select(writer => new WriterResponseDTO() {
                Id = writer.Id,
                Name = writer.Name,
                BooksTitle = writer.BookWriters.Select(book => book.Book.Title).ToList()
            }).FirstOrDefault();
            return writer;
        }

         public ICollection<WriterResponseDTO> List()
        {
            var writers = _context.Writers
            .OrderBy(writer => writer.Name)
            .Select(writer => new WriterResponseDTO() {
                    Id = writer.Id,
                    Name = writer.Name,
                    BooksTitle = writer.BookWriters.Select(book => book.Book.Title).ToList()
                }
            ).ToList();            
            
            return writers;
        }

        public string Add(WriterRequestDTO writer)
        {
            var nameExists = _context.Writers.Any(writer => writer.Name.ToLower().Trim() == writer.Name.ToLower().Trim());
            if(nameExists) 
                return "Não foi possível cadastrar o Autor. Este nome já existe na base de dados.";
            var writerEntity = new Writer {
                Name = writer.Name,
                BookWriters= writer.BooksId.Select(id => new BookWriter(){BookId = id}).ToList()
            };

            _context.Add<Writer>(writerEntity);
            _context.SaveChanges();
            return "";
        }

        public string Update(int writerId, WriterRequestDTO writer)
        {
            var nameExists = _context.Writers.Any(writer => writer.Name.ToLower().Trim() == writer.Name.ToLower().Trim());
            if(nameExists) 
                return "Não foi possível alterar o Autor. Este nome já existe na base de dados.";

            var writerEntity = _context.Writers
            .Include(writer => writer.BookWriters)
            .Where(writer => writer.Id == writerId)
            .FirstOrDefault();
        
            if(writerEntity != null) {
                writerEntity.Name = writer.Name;
                writerEntity.BookWriters = writer.BooksId.Select(bookId => new BookWriter(){BookId= bookId, WriterId = writerId}).ToList();

                _context.Update<Writer>(writerEntity);
                _context.SaveChanges();
                return "";
            }
            return "Autor não encontrado na base de dados";
        }

        public string Delete(int writerId)
        {
            var writer =_context.Writers
            .Include(writer => writer.BookWriters)
            .Where(writer => writer.Id == writerId)
            .FirstOrDefault();
            if(writer != null) {
                _context.Remove(writer);
                _context.SaveChanges();
                return "";
            }
            return "Autor não encontrado na base de dados";
        }
    }
}
