using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class VerifyEmailRequestDTO
    {
        [Required]
        public string Token { get; set; }
    }
}
