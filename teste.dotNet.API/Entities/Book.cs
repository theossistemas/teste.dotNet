using System;
using System.Collections.Generic;

public class Book {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime RegistrationDate { get; set; }
    public ICollection<Writer> Writers { get; set; }
}