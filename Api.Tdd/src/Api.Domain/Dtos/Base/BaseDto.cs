using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos.Base
{
    public abstract class BaseDto
    {        
        public int Id { get; set; }
        public bool Ativo { get; set; }
    }
}
