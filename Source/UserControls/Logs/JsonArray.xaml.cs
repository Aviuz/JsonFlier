using JsonFlier.UserControls.TabsControl;
using JsonFlier.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Controls;

namespace JsonFlier.UserControls.Logs
{
    /// <summary>
    /// Interaction logic for JsonArray.xaml
    /// </summary>
    public partial class JsonArray : UserControl, IDisposable, IRefreshable
    {
        private string filePath;
        private int numberOfEntries;
        private Thread loadingThread;

        public JsonArray(IEnumerable<JObject> logArray, string filePath = null)
        {
            InitializeComponent();

            if (filePath != null)
            {
                this.filePath = filePath;
            }
            else
            {
                dateTimePickerStart.IsEnabled = false;
                dateTimePickerEnd.IsEnabled = false;
            }


            LoadLogArray(logArray);
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

        public int NumberOfEntries
        {
            get => numberOfEntries;
            set
            {
                if (numberOfEntries != value)
                {
                    numberOfEntries = value;
                    textBlockNumberOfLogs.Text = numberOfEntries.ToString();
                }
            }
        }

        public bool IsLoading => loadingThread != null && loadingThread.IsAlive;

        private void LoadLogArray(IEnumerable<JObject> logObjects)
        {
            if (MainDockPanel.Items.Count != 0)
            {
                MainDockPanel.Items.Clear();
                NumberOfEntries = 0;
            }

            loadingThread = new Thread(() =>
            {
                foreach (var log in logObjects)
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (FilterCriteria(log))
                        {
                            AppendUserControl(new LogEntry(log));
                            NumberOfEntries++;
                        }
                    });
                }
            });
            loadingThread.Priority = ThreadPriority.BelowNormal;
            loadingThread.Start();
        }

        private void AppendUserControl(UserControl userControl)
        {
            DockPanel.SetDock(userControl, Dock.Top);
            MainDockPanel.Items.Add(userControl);
        }

        public void Refresh()
        {
            if (!CanRefresh)
                throw new System.Exception("Can't refresh when there is no file origin");

            try
            {
                var logArray = JArrayParser.Parse(File.OpenRead(filePath), Encoding.UTF8);

                LoadLogArray(logArray);
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
            if (CanRefresh)
                Refresh();
        }

        private void DateTimePickerEnd_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if (CanRefresh)
                Refresh();
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

        public void Dispose()
        {
            if (IsLoading)
            {
                loadingThread.Abort();
            }
        }
    }
}
