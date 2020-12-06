using JsonFlier.Constants;
using System.Linq;

namespace JsonFlier.Utilities
{
    public class LevelFilter
    {
        private string logLevel;
        private string[] acceptableLevels;

        public delegate void DateFilterChangeEventHandler(bool narrowingDown);
        public event DateFilterChangeEventHandler OnChanged;

        public LevelFilter()
        {
            LogLevel = JsonLoggerLogLevels.Trace;
        }

        public string LogLevel
        {
            get => logLevel;
            set
            {
                if (value != logLevel)
                {
                    logLevel = value;
                    acceptableLevels = JsonLoggerLogLevels.AtLevelOrAbove(value);
                    OnChanged?.Invoke(false);
                }
            }
        }

        public bool Filter(string logLevel)
        {
            return acceptableLevels.Any(level => level == logLevel);
        }
    }
}
