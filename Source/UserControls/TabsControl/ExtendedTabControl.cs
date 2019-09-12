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

        public event RoutedEventHandler OnOpenButtonClick;

        public ExtendedTabControl()
        {
            welcomePage = new WelcomePage();
            tabControl = new TabControl();

            welcomePage.OpenButton.Click += OpenButton_Click;

            Content = welcomePage;
        }

        public void OpenSimpleText(string title, string text) => Open(title, new PlainText(text));

        public void OpenJArray(string title, JArray jArray) => Open(title, new JsonArray(jArray));

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

        private void OnTabClosed(object sender, EventArgs args)
        {
            if (tabControl.Items.Count == 0)
            {
                Content = welcomePage;
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e) => OnOpenButtonClick?.Invoke(sender, e);
    }
}
