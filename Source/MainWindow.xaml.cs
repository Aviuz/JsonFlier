using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
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

            FileManager = new FileManager(TabControl);

            BindEventHandlers();
        }

        public FileManager FileManager { get; set; }

        private void BindEventHandlers()
        {
            menuItemOpen.Click += Open_Click;
            TabControl.OnOpenButtonClick += Open_Click;
        }

        private void Open_Click(object sender, RoutedEventArgs e) => FileManager.ShowOpenFileDialog();
    }
}
