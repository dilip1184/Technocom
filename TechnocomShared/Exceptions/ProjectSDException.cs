using System;
using TechnocomShared.Constants;
using TechnocomShared.Logging;


namespace TechnocomShared.Exceptions
{
    public class TechnocomException : Exception
    {
        private static readonly Enums.LogLevel LoggingLevel = (Enums.LogLevel)Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["LogLevel"]);

        /// <summary>
        /// Initializes a new instance of the <see cref="TechnocomException"/> class.
        /// </summary>
        protected TechnocomException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TechnocomException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        protected TechnocomException(string message)
            : base(message)
        {
            LogException("Message:" + message);
        }

        protected static void Log(string message)
        {
            if (LoggingLevel.Equals(Enums.LogLevel.Debug))
                LogWriter.GetLogWriter().Debug(message);
        }

        protected static void LogException(string message)
        {
            if (LoggingLevel.Equals(Enums.LogLevel.Debug) || LoggingLevel.Equals(Enums.LogLevel.Exception))
                LogWriter.GetLogWriter().Exception(message);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TechnocomException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        protected TechnocomException(string message, Exception inner)
            : base(message, inner)
        {
            Log("Message:" + message + "\n Inner Message:" + inner.Message + "\n Inner Stack:" +
                                           inner.StackTrace);
        }

        /// <summary>
        /// Overided Message property - message contains inner exceptions
        /// </summary>
        public override string Message
        {
            get
            {
                return base.Message + (InnerException != null ? GetInnerExceptionMessage(InnerException) : "");
            }
        }

        /// <summary>
        /// Get the inner exception message - underneath all excptions
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns></returns>
        private string GetInnerExceptionMessage(Exception ex)
        {
            return base.Message + (ex.InnerException != null ? GetInnerExceptionMessage(ex.InnerException) : "\n" + ex.Message);
        }

        /// <summary>
        /// Gets the display message.
        /// </summary>
        public string DisplayMessage
        {
            get
            {
                //in case business exception get the description for error code
                if (GetType() == typeof(TechnicalException))
                    return (LoggingLevel.Equals(Enums.LogLevel.Debug)) ? Message : ErrorCodeDescription.GetErrorDescription("Error");
                
                return GetType() == typeof(BusinessException) ? ErrorCodeDescription.GetErrorDescription(Message) : Message;
            }
        }
    }
}