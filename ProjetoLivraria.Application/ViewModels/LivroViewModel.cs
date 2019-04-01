using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ProjetoLivraria.Application.ViewModels
{
    [DataContract]
    public class LivroViewModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Isbn { get; set; }

        [DataMember]
        public string Autor { get; set; }

        [DataMember]
        public string Titulo { get; set; }

        [DataMember]
        public double Preco { get;  set; }

        [DataMember]
        public DateTime Publicacao { get;  set; }
        [DataMember]
        public string ImagemCapa { get;  set; }
    }
}
