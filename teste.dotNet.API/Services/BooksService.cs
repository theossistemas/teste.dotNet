using System.Collections.Generic;
using teste.dotNet.API.DTOs.Response;
using teste.dotNet.API.DTOs.Request;

namespace teste.dotNet.API.Services {
    public interface BooksService {
        public BookResponseDTO Get(int bookId);
        public ICollection<BookResponseDTO> List();
        public void Add(BookRequestDTO book);
        public void Update(int bookId, BookRequestDTO book);
        public void Delete(int bookId);        
    }
}
