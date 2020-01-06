using JsonFlier.UserControls.Logs;
using JsonFlier.UserControls.Toolbar.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JsonFlier.UserControls.Toolbar
{
    /// <summary>
    /// Interaction logic for ToolBar.xaml
    /// </summary>
    public partial class ToolBarContainer : UserControl
    {
        Dictionary<string, ToolBar> toolbars = new Dictionary<string, ToolBar>();

        public ToolBarContainer()
        {
            InitializeComponent();
        }

        public void LoadToolbarActions(string key, Control[] controls)
        {
            if (!toolbars.ContainsKey(key))
            {
                toolbars[key] = new ToolBar();
                toolbarTray.ToolBars.Add(toolbars[key]);
            }
            else
            {
                toolbars[key].Items.Clear();
            }

            foreach (var control in controls)
                toolbars[key].Items.Add(control);
        }

        public void Clear(string key)
        {
            if (toolbars.ContainsKey(key))
            {
                toolbarTray.ToolBars.Remove(toolbars[key]);
                toolbars.Remove(key);
            }
        }
    }
}
