using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows.Controls;

namespace JsonFlier.UserControls.Logs
{
    /// <summary>
    /// Interaction logic for JsonArray.xaml
    /// </summary>
    public partial class JsonArray : UserControl
    {
        private string filePath;
        private JArray logArray;

        public JsonArray(JArray logArray, string filePath = null)
        {
            InitializeComponent();

            this.filePath = filePath;

            this.logArray = logArray;

            LoadLogArray();
        }

        public bool CanRefresh => filePath != null;


        public DateTime? DateTimeStart
        {
            get { return dateTimePickerStart.Value; }
            set
            {
                dateTimePickerStart.Value = value;
            }
        }

        public DateTime? DateTimeEnd
        {
            get { return dateTimePickerEnd.Value; }
            set
            {
                dateTimePickerEnd.Value = value;
            }
        }

        private void LoadLogArray()
        {
            if (MainDockPanel.Children.Count != 0)
            {
                MainDockPanel.Children.Clear();
            }

            for (int i = 0; i < logArray.Count; i++)
            {
                var log = logArray[i] as JObject;
                if (FilterCriteria(log))
                {
                    AppendUserControl(new LogEntry(log));
                }
            }
        }

        private void AppendUserControl(UserControl userControl)
        {
            DockPanel.SetDock(userControl, Dock.Top);
            MainDockPanel.Children.Add(userControl);
        }

        public void Refresh()
        {
            if (!CanRefresh)
                throw new System.Exception("Can't refresh when there is no file origin");

            try
            {
                string fileContent = File.ReadAllText(filePath);

                logArray = JArray.Parse(fileContent);

                LoadLogArray();
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was error loading log file {filePath}: {e.ToString()}");
            }
        }

        private bool FilterCriteria(JObject logObject)
        {
            if (logObject == null)
                return false;

            if (DateTimeStart.HasValue)
            {
                DateTime logDate;
                TimeSpan logTime;

                var logDateString = logObject["date"]?.ToString();
                var logTimeString = logObject["time"]?.ToString();

                if (logDateString != null && DateTime.TryParse(logDateString, out logDate))
                {
                    if (logDate < DateTimeStart.Value.Date)
                        return false;
                }
                if (logTimeString != null && TimeSpan.TryParse(logTimeString, out logTime))
                {
                    if (logTime < DateTimeStart.Value.TimeOfDay)
                        return false;
                }
            }
            if (DateTimeEnd.HasValue)
            {
                DateTime logDate;
                TimeSpan logTime;

                var logDateString = logObject["date"]?.ToString();
                var logTimeString = logObject["time"]?.ToString();

                if (logDateString != null && DateTime.TryParse(logDateString, out logDate))
                {
                    if (logDate > DateTimeEnd.Value.Date)
                        return false;
                }
                if (logTimeString != null && TimeSpan.TryParse(logTimeString, out logTime))
                {
                    if (logTime > DateTimeEnd.Value.TimeOfDay)
                        return false;
                }
            }
            return true;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            dateTimePickerStart.Value = null;
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            dateTimePickerEnd.Value = null;
        }

        private void DateTimePickerStart_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            LoadLogArray();
        }

        private void DateTimePickerEnd_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            LoadLogArray();
        }

        private void ButtonRefresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Refresh();
        }

        private void ButtonToday_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           dateTimePickerStart.Value = DateTime.Today;
            DateTimeEnd = null;
        }
    }
}
