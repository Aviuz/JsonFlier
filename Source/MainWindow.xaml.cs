using JsonFlier.UserControls.Tabs;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace JsonFlier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var ti = new PlainText("test");
            OpenNewTab(ti);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                var text = File.ReadAllText(dialog.FileName);
                OpenNewTab(new PlainText(text));
            }
        }

        private void OpenNewTab(TabItem tab)
        {
            TabControl.Items.Add(tab);
            tab.Focus();
        }
    }
}
