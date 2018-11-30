using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LC.Application.Book.DataTransferObject;

namespace LC.Aplication.Book.DataTransferObject
{
    [DataContract]
    public class CreatedBookDTO
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description_short")]
        public string DescriptionShort { get; set; }

        [DataMember(Name = "description_long")]
        public string DescriptionLong { get; set; }

        [DataMember(Name = "photo")]
        public PhotoDTO Photo { get; set; }

        [DataMember(Name = "price")]
        public Decimal Price { get; set; }

        [DataMember(Name = "author")]
        public string Author { get; set; }

        [DataMember(Name = "year")]
        public int Year { get; set; }

        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "publishing")]
        public string Publishing { get; set; }

        [DataMember(Name = "weight")]
        public string Weight { get; set; }

        [DataMember(Name = "quantity_pages")]
        public int QuantityPages { get; set; }
    }
}
