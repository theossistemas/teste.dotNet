using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ProjetoLivraria.Application.ViewModels
{
    [DataContract]
    public class UsuarioViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
