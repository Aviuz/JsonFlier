using JsonFlier.UserControls.Logs;
using Newtonsoft.Json.Linq;
using System;
using System.Windows;
using System.Windows.Controls;

namespace JsonFlier.UserControls.TabsControl
{
    public class ExtendedTabControl : UserControl
    {
        private object customPage;

        public ExtendedTabControl()
        {
            TabControl = new TabControl();
        }

        public TabControl TabControl { get; private set; }

        public object CustomPage
        {
            get => customPage;
            set
            {
                if (value != null)
                {
                    customPage = value;
                    Content = customPage;
                }
                else
                {
                    customPage = null;
                    Content = TabControl;
                }
            }
        }
    }
}
