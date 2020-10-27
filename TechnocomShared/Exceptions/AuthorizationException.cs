using System;

namespace TechnocomShared.Exceptions
{
    public class AuthorizationException : BusinessException
    {
        public AuthorizationException(string message)
            : base(message)
        {
        }
    }
}