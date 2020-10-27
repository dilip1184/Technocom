using System;

namespace TechnocomShared.Exceptions
{
    public class BusinessException : BaseException
    {
        public BusinessException()
        {
        }

        public BusinessException(string message)
            : base(message)
        {
        }

       public BusinessException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}