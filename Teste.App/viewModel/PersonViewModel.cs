using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Teste.App.viewModel
{
    [DataContract]
    public class PersonViewModel
    {
        [DataMember(Name = "id")]
        public int? Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "birthDate")]
        public DateTime BirthDate { get; set; }

        [DataMember(Name = "cpf")]
        public string Cpf { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "genderId")]
        public int GenderId { get; set; }

        //[DataMember(Name = "gender")]

        //public Gender Gender { get; set; }


        public PersonViewModel(int Id, string Name, DateTime BirthDate, string Cpf, string Email, int GenderId)
        {
            this.Id = Id;
            this.Name = Name;
            this.BirthDate = BirthDate;
            this.Cpf = Cpf;
            this.Email = Email;
            this.GenderId = GenderId;
        }

        public PersonViewModel() { }

    }
}
