using System;

namespace TechnocomShared.Exceptions
{
    public class SessionTimeoutException : TechnocomException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionTimeoutException"/> class.
        /// </summary>
        public SessionTimeoutException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionTimeoutException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public SessionTimeoutException(string message)
            : base(message)
        {
        }
    }
}