using Newtonsoft.Json.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JsonFlier.UserControls.Logs
{
    /// <summary>
    /// Interaction logic for LogEntry.xaml
    /// </summary>
    public partial class LogEntry : UserControl
    {
        private bool isExpandable = true;
        private bool expanded = true;

        public LogEntry(JObject logEntry)
        {
            InitializeComponent();

            LoadJsonObject(logEntry);

            Expanded = false;
        }

        public bool IsExpandable
        {
            get => isExpandable;
            set
            {
                if (isExpandable != value)
                {
                    treeView.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                    isExpandable = value;
                }
            }
        }

        public bool Expanded
        {
            get => expanded;
            set
            {
                if (expanded != value)
                {
                    if (value)
                    {
                        ExpandGrid.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ExpandGrid.Visibility = Visibility.Collapsed;
                    }

                    expanded = value;
                }
            }
        }

        private void LoadJsonObject(JObject logEntry)
        {
            LogTitle.Text = logEntry["title"].ToString();

            if (logEntry["data"] is null)
            {
                IsExpandable = false;
            }
            else if (logEntry["data"] is JValue)
            {
                var textBox = new TextBox()
                {
                    Text = logEntry["data"].ToString(),
                    Background = null,
                    BorderThickness = new Thickness(0),
                    IsReadOnly = true,
                };

                treeView.Items.Add(textBox);
            }
            else if (logEntry["data"] is JObject)
            {
                foreach (var property in ((JObject)logEntry["data"]).Properties())
                {
                    var treeViewItem = new TreeViewItem() { Header = property.Name };
                    AddRecursive(treeViewItem, property.Value);
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

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Expanded = !Expanded;
        }
    }
}
