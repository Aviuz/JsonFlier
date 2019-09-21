using JsonFlier.UserControls.Logs;
using Newtonsoft.Json.Linq;
using System;
using System.Windows;
using System.Windows.Controls;

namespace JsonFlier.UserControls.TabsControl
{
    public class ExtendedTabControl : UserControl
    {
        private WelcomePage welcomePage;
        private TabControl tabControl;

        public ExtendedTabControl()
        {
            welcomePage = new WelcomePage();
            tabControl = new TabControl();

            welcomePage.OpenButton.Click += OpenButton_Click;

            Content = welcomePage;
        }

        public FileManager FileManager { get; set; }

        public void OpenSimpleText(string title, string text) => Open(title, new PlainText(text));

        public void OpenJArray(string title, JArray jArray, string fileOrigin = null) => Open(title, new JsonArray(jArray, fileOrigin));

        public void Open(string title, UserControl userControl)
        {
            var tab = new CloseableTab() { Title = title, Content = userControl };

            if (Content == welcomePage)
            {
                Content = tabControl;
                tabControl.SelectedIndex = 0;
            }

            tabControl.Items.Add(tab);
            tab.OnClosed += OnTabClosed;
            tab.Focus();
        }

        public void LoadBookmarks() => welcomePage.LoadBookmarks(FileManager);

        private void OnTabClosed(object sender, EventArgs args)
        {
            if (tabControl.Items.Count == 0)
            {
                Content = welcomePage;
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e) => FileManager.ShowOpenFileDialog();
    }
}
