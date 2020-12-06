using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace JsonFlier.UIControls.LogTabs.JsonLog
{
    public class LogEntryViewModel : ViewModelBase
    {
        public event EventHandler OnSelected;

        public LogEntryViewModel()
        {
            SelectCommand = new RelayCommand(() => IsSelected = true);
        }

        public DateTime DateTime { get; set; }

        public string Title { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Level { get; set; }

        public string DataType { get; set; }

        public FontWeight FontWeight { get; set; }

        public FontStyle FontStyle { get; set; }

        public Brush BackgroundBrush { get; set; }

        public JObject Json { get; set; }

        public JToken Data => Json != null ? Json["data"] : null;

        public string Image => DataType switch { "Object" => "Cube", "Exception" => "Bug", "Text" => "Text", _ => null };

        private bool _IsSelected;
        public bool IsSelected
        {
            get => _IsSelected; set
            {
                if (value != _IsSelected)
                {
                    _IsSelected = value;
                    OnSelectionChanged();
                    RaisePropertyChanged(nameof(IsSelected));
                }
            }
        }

        public ICommand SelectCommand { get; }

        private void OnSelectionChanged()
        {
            if (IsSelected)
            {
                OnSelected?.Invoke(this, new EventArgs());
            }
        }

        public static bool CanBeParsedFrom(string logString)
        {
            try
            {
                JObject.Parse(logString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static LogEntryViewModel FromString(string logString)
        {
            var logEntry = ParseBasicInfo(logString);
            CalculateStyle(logEntry);

            return logEntry;
        }

        private static LogEntryViewModel ParseBasicInfo(string logString)
        {
            var logObject = JObject.Parse(logString);

            DateTime dateTime = default;
            DateTime.TryParse(logObject["time"]?.ToString(), out dateTime);

            return new LogEntryViewModel()
            {
                DateTime = dateTime,
                Title = Regex.Replace(logObject["title"]?.ToString(), @"\t|\n|\r", ""),
                Level = logObject["level"]?.ToString(),
                DataType = logObject["data"] != null ? logObject["type"]?.ToString() : null,
                Date = dateTime.ToString("dd.MM.yyyy"),
                Time = dateTime.ToString("HH:mm:ss"),
                Json = logObject
            };
        }

        private static void CalculateStyle(LogEntryViewModel logEntry)
        {
            logEntry.FontWeight = logEntry.Data != null ? FontWeights.SemiBold : FontWeights.Normal;
            logEntry.FontStyle = logEntry.Level == "Trace" ? FontStyles.Italic : FontStyles.Normal;
        }
    }
}
