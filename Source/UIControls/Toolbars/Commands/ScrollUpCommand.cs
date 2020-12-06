using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace JsonFlier.UIControls.Toolbars.Commands
{
    public class ScrollUpCommand : ICommand
    {
        private readonly Func<ScrollViewer> getScrollViewerFunc;

        public ScrollUpCommand(Func<ScrollViewer> getScrollViewerFunc)
        {
            this.getScrollViewerFunc = getScrollViewerFunc;
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
            getScrollViewerFunc.Invoke()?.ScrollToTop();
        }
    }
}
