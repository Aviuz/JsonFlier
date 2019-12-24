using JsonFlier.Command;
using JsonFlier.UserControls.TabsControl;
using JsonFlier.UserControls.Toolbar.ActionFactories;
using JsonFlier.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JsonFlier.UserControls.LogContainers.JsonLogger
{
    /// <summary>
    /// Interaction logic for JsonPresenter.xaml
    /// </summary>
    public partial class JsonPresenter : UserControl
    {
        private JObject data;

        public JsonPresenter()
        {
            InitializeComponent();

            foreach (var action in new TreeViewActionFactory(treeView).CreateActions())
                toolBar.Children.Add(action);
        }

        public JObject Data
        {
            get => data;
            set
            {
                data = value;
                LoadJsonObject(value);
            }
        }

        private void LoadJsonObject(JObject logEntry)
        {
            if (logEntry["data"] is null)
            {
                treeViewTitle.Visibility = Visibility.Collapsed;
                treeView.Visibility = Visibility.Collapsed;
                textBoxLongText.Visibility = Visibility.Collapsed;
                toolBar.Visibility = Visibility.Hidden;
                DetailsGrid.Background = new SolidColorBrush(Color.FromRgb(0xdd, 0xdd, 0xdd));

                return;
            }

            DetailsGrid.Background = new SolidColorBrush(Color.FromArgb(0x0, 0x0, 0x0, 0x0));
            treeView.Items.Clear();

            treeViewTitle.Text = Regex.Replace(logEntry["title"]?.ToString(), @"\t|\n|\r", "");
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
                toolBar.Visibility = Visibility.Hidden;
                textBoxLongText.Text = logEntry["data"].ToString();
            }
            else if (logEntry["data"] is JObject)
            {
                treeView.Visibility = Visibility.Visible;
                textBoxLongText.Visibility = Visibility.Collapsed;
                toolBar.Visibility = Visibility.Visible;

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
                toolBar.Visibility = Visibility.Visible;

                var array = (JArray)logEntry["data"];
                for (int i = 0; i < array.Count; i++)
                {
                    var treeViewItem = new TreeViewItem() { Header = $"[{i}]" };
                    AddRecursive(treeViewItem, array[i]);
                    treeView.Items.Add(treeViewItem);
                }
            }

            ExpandAll();
        }

        private void ExpandAll()
        {
            foreach (TreeViewItem item in treeView.Items)
                item.ExpandSubtree();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ExpandAll();
        }

        private void LoadToolBar(Control[] controls)
        {
            toolBar.Children.Clear();

            foreach (var control in controls)
            {
                toolBar.Children.Add(control);
            }
        }
    }
}
