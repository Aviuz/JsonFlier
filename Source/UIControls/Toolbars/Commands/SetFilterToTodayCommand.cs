
using JsonFlier.Utilities;
using System;
using System.Windows.Input;

namespace JsonFlier.UIControls.Toolbars.Commands
{
    public class SetFilterToTodayCommand : ICommand
    {
        private readonly DateFilter dateFilter;

        public SetFilterToTodayCommand(DateFilter dateFilter)
        {
            this.dateFilter = dateFilter;
        }

#pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;
#pragma warning restore

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            dateFilter.SetFilter(DateTime.Today, null);
        }
    }
}
