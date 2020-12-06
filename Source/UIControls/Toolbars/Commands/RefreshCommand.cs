using JsonFlier.Utilities;
using System;
using System.Windows.Input;

namespace JsonFlier.UIControls.Toolbars.Commands
{
    public class RefreshCommand : ICommand
    {
        private readonly IRefreshable refreshable;

        public RefreshCommand(IRefreshable refreshable)
        {
            this.refreshable = refreshable;
        }

#pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;
#pragma warning restore

        public bool CanExecute(object parameter)
        {
            return refreshable.CanRefresh();
        }

        public void Execute(object parameter)
        {
            refreshable.Refresh();
        }
    }
}
