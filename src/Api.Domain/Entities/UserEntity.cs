using System.Security.Cryptography;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Api.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        private bool? _admin;
        public bool? Admin
        {
            get { return _admin; }
            set { _admin = (value == null ? false : true); }
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Required(AllowEmptyStrings = true)]
        public IEnumerable<BookEntity> Books { get; set; }
    }
}
