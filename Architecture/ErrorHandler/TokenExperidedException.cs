using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture
{
    public class TokenExperidedException: Exception
    {
        public TokenExperidedException(string message): base(message)
        {
        }
    }
}
