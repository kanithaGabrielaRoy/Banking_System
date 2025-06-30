using System;

namespace HM_Bank_App.ExceptionLayer
{
    public class InsufficientFundException : Exception
    {
        public InsufficientFundException(string message) : base(message) { }
    }
}