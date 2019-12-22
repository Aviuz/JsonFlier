using JsonFlier.Command;
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
    /// Interaction logic for SelectDateAction.xaml
    /// </summary>
    public partial class SelectDateAction : UserControl
    {
        private bool enabled = true;

        public SelectDateAction(string text, DateTimeListener dateTimeListener)
        {
            InitializeComponent();

            textBlock.Text = text;

            DateTimeListener = dateTimeListener;
            DateTimeListener.OnValueChanged += DateTimeListener_OnValueChanged;
        }

        public DateTimeListener DateTimeListener { get; }

        public bool Enabled
        {
            get => enabled;
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    datePicker.IsEnabled = enabled;
                    buttonUp.IsEnabled = enabled;
                    buttonDown.IsEnabled = enabled;
                    buttonClose.IsEnabled = enabled;
                }
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTimeListener.DateTime = datePicker.SelectedDate;
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            if (!DateTimeListener.DateTime.HasValue)
            {
                DateTimeListener.DateTime = DateTime.Today.AddDays(1);
            }
            else
            {
                DateTimeListener.DateTime = DateTimeListener.DateTime.Value.AddDays(1);
            }
        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            if (!DateTimeListener.DateTime.HasValue)
            {
                DateTimeListener.DateTime = DateTime.Today.Subtract(TimeSpan.FromDays(1));
            }
            else
            {
                DateTimeListener.DateTime = DateTimeListener.DateTime.Value.Subtract(TimeSpan.FromDays(1));
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            DateTimeListener.DateTime = null;
        }

        private void DateTimeListener_OnValueChanged(DateTime? oldValue, DateTime? newValue)
        {
            datePicker.SelectedDate = newValue;
        }
    }
}
