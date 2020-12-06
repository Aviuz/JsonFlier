using System.Windows;
using System.Windows.Controls;

namespace JsonFlier.UIControls.Toolbars
{
    /// <summary>
    /// Interaction logic for Toolbar.xaml
    /// </summary>
    public partial class ToolbarTrayContainer : UserControl
    {
        public new ToolbarTrayContainerViewModel DataContext { get => base.DataContext as ToolbarTrayContainerViewModel; set => base.DataContext = value; }

        public ToolbarTrayContainer()
        {
            InitializeComponent();
            DataContextChanged += ToolbarTrayContainer_DataContextChanged;
        }

        private void ToolbarTrayContainer_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            toolbarTray.ToolBars.Clear();
            if (DataContext != null)
            {
                AddToolbars();
                DataContext.Toolbars.ListChanged += Toolbars_ListChanged;
            }
        }

        private void Toolbars_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            toolbarTray.ToolBars.Clear();
            AddToolbars();
        }

        private void AddToolbars()
        {
            foreach (var toolbar in DataContext.Toolbars)
                toolbarTray.ToolBars.Add(toolbar);
        }
    }
}
