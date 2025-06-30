using System;

namespace HM_Bank_App.ExceptionLayer
{
    public class InvalidAccountException : Exception
    {
        public InvalidAccountException(string message) : base(message) { }
    }
}