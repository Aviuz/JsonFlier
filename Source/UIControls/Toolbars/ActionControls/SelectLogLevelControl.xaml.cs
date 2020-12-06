using JsonFlier.Utilities;
using System.Windows.Controls;

namespace JsonFlier.UIControls.Toolbars.ActionControls
{
    /// <summary>
    /// Interaction logic for MinimumLogLevelSelector.xaml
    /// </summary>
    public partial class SelectLogLevelControl : UserControl
    {
        private readonly LevelFilter levelFilter;
        private bool suspendEvents = false;

        public SelectLogLevelControl(LevelFilter levelFilter)
        {
            this.levelFilter = levelFilter;
            levelFilter.OnChanged += LevelFilter_OnChanged;

            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!suspendEvents)
                levelFilter.LogLevel = comboBox.SelectedItem?.ToString();
        }

        private void LevelFilter_OnChanged(bool narrowingDown)
        {
            suspendEvents = true;
            comboBox.SelectedItem = levelFilter.LogLevel;
            suspendEvents = false;
        }
    }
}
