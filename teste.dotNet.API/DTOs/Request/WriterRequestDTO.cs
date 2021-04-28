using System.Collections.Generic;

namespace teste.dotNet.API.DTOs.Request {

    public class WriterRequestDTO {
        public string Name { get; set; }
        public ICollection<int> BooksId { get; set; }
    }
}