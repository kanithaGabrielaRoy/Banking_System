using System;

namespace HM_Bank_App.ExceptionLayer
{
    public class OverDraftLimitExceededException : Exception
    {
        public OverDraftLimitExceededException(string message) : base(message) { }
    }
}