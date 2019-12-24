using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonFlier.Constants
{
    public static class JsonLoggerLogLevels
    {
        public const string Trace = "Trace";
        public const string Info = "Info";
        public const string Warning = "Warning";
        public const string Critical = "Critical";

        public static readonly string[] AllLogLevels = new string[] { Trace, Info, Warning, Critical };

        public static string[] AtLevelOrAbove(string minimumLogLevel)
        {
            switch (minimumLogLevel)
            {
                case Trace:
                    return new string[] { Trace, Info, Warning, Critical };
                case Info:
                    return new string[] { Info, Warning, Critical };
                case Warning:
                    return new string[] { Warning, Critical };
                case Critical:
                    return new string[] { Critical };
                default:
                    throw new Exception("Invalid log level");
            }
        }
    }
}
