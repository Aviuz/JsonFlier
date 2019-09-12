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

namespace JsonFlier.UserControls.Logs
{
    /// <summary>
    /// Interaction logic for LogArrayControl.xaml
    /// </summary>
    public partial class LogArrayControl : UserControl
    {
        public LogArrayControl()
        {
            InitializeComponent();
        }

        public void AppendUserControl(UserControl userControl)
        {
            DockPanel.SetDock(userControl, Dock.Top);
            MainDockPanel.Children.Add(userControl);
        }
    }
}
