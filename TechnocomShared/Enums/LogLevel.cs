using System;

namespace TechnocomShared.Enums
{
    [Flags]
    public enum LogLevel
    {
        None = 0,
        Exception = 1,
        Debug = 2,
    }
}