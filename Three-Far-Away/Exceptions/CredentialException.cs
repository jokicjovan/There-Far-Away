using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Three_Far_Away.Exceptions
{
    public class CredentialException : Exception
    {
        public CredentialException(string? message) : base(message)
        {
        }
    }
}
