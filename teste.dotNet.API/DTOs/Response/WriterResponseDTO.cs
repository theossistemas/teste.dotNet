using System;
using System.Collections.Generic;

namespace teste.dotNet.API.DTOs.Response {
    public class WriterResponseDTO {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> BooksTitle { get; set; }
    }

}