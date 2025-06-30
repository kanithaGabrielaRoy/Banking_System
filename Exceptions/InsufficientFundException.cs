using System;

namespace HM_Bank_CoreApp.Exceptions
{
    public class InsufficientFundException : Exception
    {
        public InsufficientFundException(string message) : base(message) { }
    }
}
