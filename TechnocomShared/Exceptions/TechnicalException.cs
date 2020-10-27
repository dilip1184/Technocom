using System;

namespace TechnocomShared.Exceptions
{
    public class TechnicalException : BaseException
    {
        public TechnicalException(string message, Exception inner)
            : base(message, inner)
        {
            LogException("Message:" + message + "\n Inner Message:" + inner.Message + "\n Inner Stack:" +
                                           inner.StackTrace);
        }
    }
}