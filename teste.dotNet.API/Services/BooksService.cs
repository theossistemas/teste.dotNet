using System.Collections.Generic;
using teste.dotNet.API.DTOs.Response;
using teste.dotNet.API.DTOs.Request;

namespace teste.dotNet.API.Services {
    public interface BooksService {
        public BookResponseDTO Get(int bookId);
        public ICollection<BookResponseDTO> List();
        public string Add(BookRequestDTO book);
        public string Update(int bookId, BookRequestDTO book);
        public string Delete(int bookId);        
    }
}
