using System;

namespace Three_Far_Away.Exceptions
{
    public class CredentialException : Exception
    {
        public CredentialException(string? message) : base(message)
        {
        }
    }
}
