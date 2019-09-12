using JsonFlier.UserControls.Tabs.Base;
using System;
using System.Windows;
using System.Windows.Controls;

namespace JsonFlier.UserControls
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

        public void Open(CloseableTab tab)
        {
            if (Content == welcomePage)
            {
                Content = tabControl;
                tabControl.SelectedIndex = 0;
            }

            tabControl.Items.Add(tab);
            tab.OnClosed += OnTabClosed;
            tab.Focus();
        }

        public void OnTabClosed(object sender, EventArgs args)
        {
            if (tabControl.Items.Count == 0)
            {
                Content = welcomePage;
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e) => OnOpenButtonClick?.Invoke(sender, e);
    }
}
