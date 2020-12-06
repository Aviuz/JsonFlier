using JsonFlier.UIControls.Toolbars.Commands;
using System;
using System.Windows;
using System.Windows.Controls;

namespace JsonFlier.UIControls.Toolbars.ActionControls
{
    /// <summary>
    /// Interaction logic for SelectDateAction.xaml
    /// </summary>
    public partial class SelectDateControl : UserControl
    {
        private bool enabled = true;
        private readonly IDateTimeListener dateTimeListener;
        private bool externalChange = false;

        public SelectDateControl(string text, IDateTimeListener dateTimeListener)
        {
            InitializeComponent();

            this.dateTimeListener = dateTimeListener;

            textBlock.Text = text;

            dateTimeListener.OnValueChanged += DateTimeListener_OnValueChanged;
        }

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
            if (!externalChange)
            {
                dateTimeListener.DateTime = datePicker.SelectedDate;
            }
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            if (!dateTimeListener.DateTime.HasValue)
            {
                dateTimeListener.DateTime = DateTime.Today.AddDays(1);
            }
            else
            {
                dateTimeListener.DateTime = dateTimeListener.DateTime.Value.AddDays(1);
            }
        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            if (!dateTimeListener.DateTime.HasValue)
            {
                dateTimeListener.DateTime = DateTime.Today.Subtract(TimeSpan.FromDays(1));
            }
            else
            {
                dateTimeListener.DateTime = dateTimeListener.DateTime.Value.Subtract(TimeSpan.FromDays(1));
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            dateTimeListener.DateTime = null;
        }

        private void DateTimeListener_OnValueChanged(DateTime? oldValue, DateTime? newValue)
        {
            externalChange = true;
            datePicker.SelectedDate = newValue;
            externalChange = false;
        }
    }
}
