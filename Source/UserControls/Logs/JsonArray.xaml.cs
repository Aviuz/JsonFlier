using JsonFlier.UserControls.TabsControl;
using JsonFlier.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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

        private void AppendUserControl(UserControl userControl)
        {
            DockPanel.SetDock(userControl, Dock.Top);
            listView.Items.Add(userControl);
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

        private void LoadJsonObject(JObject logEntry)
        {
            if (logEntry["data"] is null)
            {
                treeViewTitle.Visibility = Visibility.Collapsed;
                treeView.Visibility = Visibility.Collapsed;
                textBoxLongText.Visibility = Visibility.Collapsed;
                DetailsGrid.Background = new SolidColorBrush(Color.FromRgb(0xdd, 0xdd, 0xdd));

                return;
            }

            DetailsGrid.Background = new SolidColorBrush(Color.FromArgb(0x0, 0x0, 0x0, 0x0));
            treeView.Items.Clear();

            treeViewTitle.Text = logEntry?["title"].ToString();
            treeViewTitle.Visibility = Visibility.Visible;

            if (logEntry["data"] is JValue)
            {
                //var textBox = new TextBox()
                //{
                //    Text = logEntry["data"].ToString(),
                //    TextWrapping = TextWrapping.Wrap,
                //    Background = null,
                //    BorderThickness = new Thickness(0),
                //    IsReadOnly = true,
                //};

                //treeView.Items.Add(textBox);
                treeView.Visibility = Visibility.Collapsed;
                textBoxLongText.Visibility = Visibility.Visible;
                textBoxLongText.Text = logEntry["data"].ToString();
            }
            else if (logEntry["data"] is JObject)
            {
                treeView.Visibility = Visibility.Visible;
                textBoxLongText.Visibility = Visibility.Collapsed;

                foreach (var property in ((JObject)logEntry["data"]).Properties())
                {
                    var treeViewItem = new TreeViewItem() { Header = property.Name };
                    AddRecursive(treeViewItem, property.Value);
                    treeView.Items.Add(treeViewItem);
                }
            }
            else if (logEntry["data"] is JArray)
            {
                treeView.Visibility = Visibility.Visible;
                textBoxLongText.Visibility = Visibility.Collapsed;

                var array = (JArray)logEntry["data"];
                for (int i = 0; i < array.Count; i++)
                {
                    var treeViewItem = new TreeViewItem() { Header = $"[{i}]" };
                    AddRecursive(treeViewItem, array[i]);
                    treeView.Items.Add(treeViewItem);
                }
            }
        }

        private void AddRecursive(TreeViewItem parent, JToken item)
        {
            if (item == null)
            {
                return;
            }

            if (item is JValue)
            {
                if (((JValue)item).Value == null)
                {
                    var treeViewItem = new TreeViewItem() { Header = "<< null >>", FontStyle = FontStyles.Italic };
                    parent.Items.Add(treeViewItem);
                }
                else
                {
                    var treeViewItem = new TreeViewItem() { Header = item.ToString() };
                    parent.Items.Add(treeViewItem);
                }
            }
            else if (item is JArray)
            {
                var array = (JArray)item;
                for (int i = 0; i < array.Count; i++)
                {
                    var treeViewItem = new TreeViewItem() { Header = $"[{i}]" };
                    AddRecursive(treeViewItem, array[i]);
                    parent.Items.Add(treeViewItem);
                }
            }
            else if (item is JObject)
            {
                foreach (var property in ((JObject)item).Properties())
                {
                    var treeViewItem = new TreeViewItem() { Header = property.Name };
                    AddRecursive(treeViewItem, property.Value);
                    parent.Items.Add(treeViewItem);
                }
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            var logData = item?.Content as LogEntryModel;
            if (logData != null)
            {
                LoadJsonObject(logData.Json);
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
