using System;

namespace HM_Bank_CoreApp.Exceptions
{
    public class OverDraftLimitExceededException : Exception
    {
        public OverDraftLimitExceededException(string message) : base(message) { }
    }
}
