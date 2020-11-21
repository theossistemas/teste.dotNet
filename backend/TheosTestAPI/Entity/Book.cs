using System;
using System.Collections.Generic;

#nullable disable

namespace TheosTestAPI.Entity
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public short LaunchYear { get; set; }
        public decimal Price { get; set; }
    }
}
