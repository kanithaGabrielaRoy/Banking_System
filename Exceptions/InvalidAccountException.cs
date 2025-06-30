using System;

namespace HM_Bank_CoreApp.Exceptions
{
    public class InvalidAccountException : Exception
    {
        public InvalidAccountException(string message) : base(message) { }
    }
}
