using System.Collections.Generic;
using teste.dotNet.API.DTOs.Request;
using teste.dotNet.API.DTOs.Response;

namespace teste.dotNet.API.Repository {
    public interface BooksRepository {        
        public BookResponseDTO Get(int bookId);
        public ICollection<BookResponseDTO> List();
        public void Add(BookRequestDTO book);
        public void Update(int bookId, BookRequestDTO book);
        public void Delete(int bookId);      
        
    }
}
