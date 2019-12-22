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
    public partial class ToolBar : UserControl
    {
        public ToolBar()
        {
            InitializeComponent();
        }

        public void LoadToolbarActions(Control[] controls)
        {
            toolbar.Items.Clear();

            foreach (var control in controls)
                toolbar.Items.Add(control);
        }

        public void Clear()
        {
            toolbar.Items.Clear();
        }
    }
}
