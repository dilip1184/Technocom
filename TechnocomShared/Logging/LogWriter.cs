using System;
using System.Diagnostics;
using TechnocomShared.Enums;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace TechnocomShared.Logging
{
    public sealed class LogWriter
    {
        private static readonly LogWriter Instance = new LogWriter();
        private static readonly LogLevel LoggingLevel = (LogLevel)Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["LogLevel"]);

        private LogWriter()
        {
        }

        public static LogWriter GetLogWriter()
        {
            return Instance;
        }

        public void Log(string message)
        {
            if (LoggingLevel.Equals(LogLevel.Debug))
                Logger.Write(GetLogEntry(LogCategory.Log, message, LogPriortiy.Medium, TraceEventType.Information));
        }

        public void Debug(string message)
        {
            if (LoggingLevel.Equals(LogLevel.Debug))
                Logger.Write(GetLogEntry(LogCategory.Debug, message, LogPriortiy.Medium, TraceEventType.Start));
        }

        public void Exception(Exception ex)
        {
            Exception(ex.Message);
        }

        public void Exception(string message)
        {
            if (LoggingLevel.Equals(LogLevel.Debug) || LoggingLevel.Equals(LogLevel.Exception))
                Logger.Write(GetLogEntry(LogCategory.Exception, message, LogPriortiy.Critical, TraceEventType.Error));
        }

        /// <summary>
        /// Get the Log Entry
        /// </summary>
        /// <param name="logCategory">Technocom Log Category - Log/Debug/Exception</param>
        /// <param name="message">Log Message</param>
        /// <param name="logPriority">LogPriority</param>
        /// <param name="eventType">TraceEventType</param>
        /// <returns>LogEntry</returns>
        private LogEntry GetLogEntry(LogCategory logCategory, string message, LogPriortiy logPriority,
                                     TraceEventType eventType)
        {
            var logEntry = new LogEntry {Message = message};
            logEntry.Categories.Add(logCategory.ToString());
            logEntry.Priority = (int) logPriority;
            logEntry.Severity = eventType;

            //Prepareing extended properties items
            var stackFrames = new StackTrace().GetFrames();

            var method = stackFrames[1].GetMethod().Name;
            var className = stackFrames[1].GetMethod().DeclaringType.ToString();
            //Add the extended properties
            logEntry.ExtendedProperties.Add("Class", className);
            logEntry.ExtendedProperties.Add("Method", method);

            return logEntry;
        }
    }
}