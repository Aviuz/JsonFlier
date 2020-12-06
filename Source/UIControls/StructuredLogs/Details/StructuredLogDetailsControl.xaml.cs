using JsonFlier.UIControls.LogTabs.JsonLog.JsonTreeView;
using System.Windows.Controls;

namespace JsonFlier.UIControls.LogTabs.JsonLog
{
    /// <summary>
    /// Interaction logic for JsonPresenter.xaml
    /// </summary>
    public partial class StructuredLogDetailsControl : UserControl
    {
        public new LogEntryViewModel DataContext {
            get => base.DataContext as LogEntryViewModel;
            set => base.DataContext = value;
        }

        public StructuredLogDetailsControl()
        {
            InitializeComponent();
            DataContextChanged += JsonPresenter_DataContextChanged;
        }

        private void JsonPresenter_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            dataTreePresenter.DataContext = TreeViewModelFactory.CreateFromJson(DataContext?.Data);
        }
    }
}
