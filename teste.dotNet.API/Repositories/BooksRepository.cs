using System.Collections.Generic;
using teste.dotNet.API.DTOs.Request;
using teste.dotNet.API.DTOs.Response;

namespace teste.dotNet.API.Repository {
    public interface BooksRepository {        
        public BookResponseDTO Get(int bookId);
        public ICollection<BookResponseDTO> List();
        public string Add(BookRequestDTO book);
        public string Update(int bookId, BookRequestDTO book);
        public string Delete(int bookId);      
        
    }
}
