using System;

namespace JsonFlier.Constants
{
    public static class JsonLoggerLogLevels
    {
        public const string Trace = "Trace";
        public const string Info = "Info";
        public const string Warning = "Warning";
        public const string Critical = "Critical";
        public const string Fatal = "Fatal";

        public static readonly string[] AllLogLevels = new string[] { Trace, Info, Warning, Critical, Fatal };

        public static string[] AtLevelOrAbove(string minimumLogLevel)
        {
            switch (minimumLogLevel)
            {
                case Trace:
                    return new string[] { Trace, Info, Warning, Critical, Fatal };
                case Info:
                    return new string[] { Info, Warning, Critical, Fatal };
                case Warning:
                    return new string[] { Warning, Critical, Fatal };
                case Critical:
                    return new string[] { Critical, Fatal };
                case Fatal:
                    return new string[] { Fatal };
                default:
                    throw new Exception("Invalid log level");
            }
        }
    }
}
