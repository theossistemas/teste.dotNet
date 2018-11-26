using System;
using System.Runtime.Serialization;

namespace LC.Domain
{
    [DataContract]
    public class Book : BaseEntity
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "slug")]
        public string Slug { get; set; }

        [DataMember(Name = "description_short")]
        public string DescriptionShort { get; set; }

        [DataMember(Name = "description_long")]
        public string DescriptionLong { get; set; }

        [DataMember(Name = "photo")]
        public string Photo { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatdAt { get; set; }

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

        public override object[] GetKey()
        {
            return new object[] { Id };
        }
    }
}