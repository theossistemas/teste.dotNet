using System;
using System.Collections.Generic;

namespace teste.dotNet.API.DTOs.Response {
    public class BookResponseDTO {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<string> WritersName { get; set; }
    }

}