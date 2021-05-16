using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class ValidateResetTokenRequestDTO
    {
        [Required]
        public string Token { get; set; }

    }
}
