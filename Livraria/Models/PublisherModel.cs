using Livraria.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livraria.Models
{
    public class PublisherModel
    {
        public class PublisherViewModel
        {
            public Publisher SinglePublisher { get; set; }
            public List<Publisher> AllPublishers { get; set; }
            public PublisherViewModel()
            {
                AllPublishers = new List<Publisher>();
                SinglePublisher = new Publisher();
            }
        }

        public class PublisherRegisterModel
        {
            public Guid Id { get; set; }

            [Required]
            [Display(Name = "Nome da Editora")]
            public string Name { get; set; }

            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            [Column(TypeName = "DateTime2")]
            public DateTime CreatedAt { get; set; }

            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            [Column(TypeName = "DateTime2")]
            public DateTime ModifiedAt { get; set; }
        }
    }
}
