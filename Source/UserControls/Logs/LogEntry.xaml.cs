using Newtonsoft.Json.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
                    interactionRectangle.Cursor = value ? Cursors.Hand : null;


                    if (value)
                    {
                        backgroundRectangle.Stroke = new SolidColorBrush(Color.FromRgb(0x30, 0x30, 0x30));
                        interactionRectangle.MouseLeftButtonDown += InteractionRectangle_MouseLeftButtonDown;
                        interactionRectangle.MouseEnter += InteractionRectangle_MouseEnter;
                        interactionRectangle.MouseLeave += InteractionRectangle_MouseLeave;
                    }
                    else
                    {
                        backgroundRectangle.Stroke = null;
                        interactionRectangle.MouseLeftButtonDown -= InteractionRectangle_MouseLeftButtonDown;
                        interactionRectangle.MouseEnter -= InteractionRectangle_MouseEnter;
                        interactionRectangle.MouseLeave -= InteractionRectangle_MouseLeave;
                    }
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
            SetIcon(logEntry);
            SetColor(logEntry);

            titleBox.Text = logEntry["title"].ToString();
            dateBox.Text = logEntry["date"].ToString();
            timeBox.Text = logEntry["time"].ToString();

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

        private void SetIcon(JObject logEntry)
        {
            switch (logEntry["dataType"]?.ToString())
            {
                case "Text":

                    if (logEntry["data"] != null)
                    {
                        textImage.Visibility = Visibility.Visible;
                        exceptionImage.Visibility = Visibility.Collapsed;
                        objectImage.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        textImage.Visibility = Visibility.Hidden;
                        exceptionImage.Visibility = Visibility.Collapsed;
                        objectImage.Visibility = Visibility.Collapsed;
                    }
                    break;

                case "Exception":
                    textImage.Visibility = Visibility.Collapsed;
                    exceptionImage.Visibility = Visibility.Visible;
                    objectImage.Visibility = Visibility.Collapsed;
                    break;

                case "Object":
                    textImage.Visibility = Visibility.Collapsed;
                    exceptionImage.Visibility = Visibility.Collapsed;
                    objectImage.Visibility = Visibility.Visible;
                    break;

                default:
                    textImage.Visibility = Visibility.Collapsed;
                    exceptionImage.Visibility = Visibility.Collapsed;
                    objectImage.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void SetColor(JObject logEntry)
        {
            switch (logEntry["category"]?.ToString())
            {
                case "Critical":
                    backgroundRectangle.Fill = new SolidColorBrush(Color.FromRgb(0xf5, 0xb8, 0xb8));
                    break;

                case "Warning":
                    backgroundRectangle.Fill = new SolidColorBrush(Color.FromRgb(0xff, 0xf9, 0xc2));
                    break;

                case "Info":
                    backgroundRectangle.Fill = new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0xff));
                    break;

                case "Trace":
                    backgroundRectangle.Fill = new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0xff));
                    break;

                default:
                    backgroundRectangle.Fill = new SolidColorBrush(Colors.White);
                    break;
            }
            detailsRectangle.Fill = backgroundRectangle.Fill;
        }

        private void InteractionRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Expanded = !Expanded;
        }

        private void InteractionRectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            backgroundRectangle.Opacity = 0.8;
        }

        private void InteractionRectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            backgroundRectangle.Opacity = 1;
        }
    }
}
