using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  teste.dotNet.API.Entities {
    public class Book {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime RegistrationDate { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        public virtual ICollection<Writer> Writers { get; set; }
    }
}
