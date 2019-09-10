using JsonFlier.UserControls.Tabs;
using JsonFlier.UserControls.Tabs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JsonFlier.UserControls
{
    public class ExtendedTabControl : UserControl
    {
        private WelcomePage welcomePage;
        private TabControl tabControl;

        public ExtendedTabControl()
        {
            welcomePage = new WelcomePage();
            tabControl = new TabControl();

            welcomePage.DebugButton.Click += DebugButton_Click;

            Content = welcomePage;
        }

        private void DebugButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Content = tabControl;
            Open(new PlainText("nowy"));
            tabControl.SelectedIndex = 0;
        }

        public void Open(CloseableTab tab)
        {
            tabControl.Items.Add(tab);
            tab.OnClosed += OnTabClosed;
            tab.Focus();
        }

        public void OnTabClosed(object sender, EventArgs args) => TryShowWelcomePage();

        public void TryShowWelcomePage()
        {
            if(tabControl.Items.Count == 0)
            {
                Content = welcomePage;
            }
        }
    }
}
