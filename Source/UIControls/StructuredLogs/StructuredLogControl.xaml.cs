using System.Windows.Controls;

namespace JsonFlier.UIControls.LogTabs.JsonLog
{
    /// <summary>
    /// Interaction logic for JsonLogViewer.xaml
    /// </summary>
    public partial class StructuredLogControl : UserControl
    {
        public new StructuredLogViewModel DataContext => base.DataContext as StructuredLogViewModel;

        public StructuredLogControl()
        {
            InitializeComponent();
        }
    }
}
