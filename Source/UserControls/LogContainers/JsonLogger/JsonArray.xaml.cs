using JsonFlier.Command;
using JsonFlier.Constants;
using JsonFlier.UserControls.TabsControl;
using JsonFlier.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        private string[] visibleLogLevels = JsonLoggerLogLevels.AllLogLevels;

        public JsonArray(IEnumerable<JObject> logArray, string filePath = null)
        {
            InitializeComponent();

            DateTimeStartListener.OnValueChanged += OnDateTimeChanged;
            DateTimeEndListener.OnValueChanged += OnDateTimeChanged;
            MinimumLogLevelListener.OnValueChanged += OnLogLevelChanged;

            if (filePath != null)
            {
                this.filePath = filePath;
            }

            LoadLogArray(logArray);
        }

        public bool CanRefresh => filePath != null;

        public DateTimeListener DateTimeStartListener { get; set; } = new DateTimeListener(null);

        public DateTimeListener DateTimeEndListener { get; set; } = new DateTimeListener(null);

        public StringListener MinimumLogLevelListener { get; set; } = new StringListener(null);

        public DateTime? DateTimeStart
        {
            get { return DateTimeStartListener.DateTime; }
            set
            {
                DateTimeStartListener.DateTime = value;
            }
        }

        public DateTime? DateTimeEnd
        {
            get { return DateTimeEndListener.DateTime; }
            set
            {
                DateTimeEndListener.DateTime = value;
            }
        }

        public String MinimumLogLevel
        {
            get => MinimumLogLevelListener.Value;
            set => MinimumLogLevelListener.Value = value;
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
            listView.Items.Clear();
            NumberOfEntries = 0;

            loadingThread = new Thread(() =>
            {
                foreach (var log in logObjects)
                {
                    try
                    {
                        Dispatcher.Invoke(() =>
                        {
                            if (FilterCriteria(log))
                            {
                                var entry = LogEntryModel.FromJObject(log);
                                listView.Items.Add(entry);

                                NumberOfEntries++;
                            }
                        });
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            });
            loadingThread.Priority = ThreadPriority.BelowNormal;
            loadingThread.Start();
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

        public void PanToToday()
        {
            DateTimeStart = DateTime.Today;
            DateTimeEnd = null;
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
            if (MinimumLogLevel != null)
            {

                if (!visibleLogLevels.Contains(logObject["category"]?.ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        private void OnDateTimeChanged(DateTime? oldValue, DateTime? newValue)
        {
            if (CanRefresh)
            {
                Refresh();
            }
        }

        private void OnLogLevelChanged(string oldValue, string newValue)
        {
            if (CanRefresh)
            {
                visibleLogLevels = JsonLoggerLogLevels.AtLevelOrAbove(newValue);
                Refresh();
            }
        }

        public void Dispose()
        {
            if (IsLoading)
            {
                loadingThread.Abort();
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            var logData = item?.Content as LogEntryModel;
            if (logData != null)
            {
                jsonPresenter.Data = logData.Json;
            }
        }

        private void ButtonScrollTop_Click(object sender, RoutedEventArgs e)
        {
            if (listView.Items.Count > 0)
            {
                listView.ScrollIntoView(listView.Items[0]);
            }
        }

        private void ButtonScrollBottom_Click(object sender, RoutedEventArgs e)
        {
            if (listView.Items.Count > 0)
            {
                listView.ScrollIntoView(listView.Items[listView.Items.Count - 1]);
            }
        }
    }
}
