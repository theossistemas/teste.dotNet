using System;
using System.Collections.Generic;

namespace teste.dotNet.API.DTOs.Request {

    public class BookRequestDTO {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<int> WritersId { get; set; }
    }
}