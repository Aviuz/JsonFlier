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

namespace JsonFlier.UserControls.TabsControl
{
    /// <summary>
    /// Interaction logic for ExtendedTabControl.xaml
    /// </summary>
    public partial class ExtendedTabControl : UserControl
    {
        private object customPage;

        public ExtendedTabControl()
        {
            InitializeComponent();
        }

        public TabControl TabControl => tabControl;

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
