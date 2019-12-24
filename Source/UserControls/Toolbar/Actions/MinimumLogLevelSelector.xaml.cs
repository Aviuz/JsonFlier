using JsonFlier.Command;
using JsonFlier.UserControls.Logs;
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

namespace JsonFlier.UserControls.Toolbar.Actions
{
    /// <summary>
    /// Interaction logic for MinimumLogLevelSelector.xaml
    /// </summary>
    public partial class MinimumLogLevelSelector : UserControl
    {
        public MinimumLogLevelSelector(JsonArray jsonArrayControl)
        {
            InitializeComponent();
            StringListener = jsonArrayControl.MinimumLogLevelListener;
            StringListener.OnValueChanged += StringListener_OnValueChanged;
        }

        private void StringListener_OnValueChanged(string oldValue, string newValue)
        {
            comboBox.SelectedItem = newValue;
        }

        public StringListener StringListener { get; }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedItem != null && StringListener != null)
            {
                StringListener.Value = comboBox.SelectedItem.ToString();
            }
        }
    }
}
