using JsonFlier.UIControls.LogTabs.JsonLog;
using JsonFlier.UIControls.Toolbars.ActionControls;
using JsonFlier.UIControls.Toolbars.Commands;
using System;
using System.Windows;
using System.Windows.Controls;

namespace JsonFlier.UIControls.Toolbars.PredefinedToolbars
{
    public static class StructuredLogToolbar
    {
        public static ToolBar Create(StructuredLogControl logControl)
        {
            return new ToolBar()
            {
                ItemsSource = new FrameworkElement[]
                {
                    new ToolbarButton() { Command = new ScrollUpCommand(GetScrollViewerFunc(logControl)), Source = "UpDoubleChevron" },
                    new ToolbarButton() { Command = new ScrollDownCommand(GetScrollViewerFunc(logControl)), Source = "DownDoubleChevron" },

                    new Separator(),

                    new ToolbarButton() { Command = new RefreshCommand(GetViewModel(logControl)), Source = "Refresh" },

                    new Separator(),

                    new ToolbarButton() { Command = new SetFilterToTodayCommand(GetViewModel(logControl).DateFilter), Source = "Calendar" },
                    new ToolbarButton() { Command = new SetFilterToNowCommand(GetViewModel(logControl).DateFilter), Source = "Time" },

                    new SelectDateControl("Start", new StartDateFilterListener(GetViewModel(logControl).DateFilter)),
                    new SelectDateControl("End", new EndDateFilterListener(GetViewModel(logControl).DateFilter)),

                    new Separator(),

                    new SelectLogLevelControl(GetViewModel(logControl).LevelFilter),
                },
            };
        }

        private static StructuredLogViewModel GetViewModel(StructuredLogControl logControl)
        {
            return logControl.DataContext;
        }

        private static Func<ScrollViewer> GetScrollViewerFunc(StructuredLogControl logControl)
        {
            var logsItemsControl = logControl.logList.itemsControl;
            return () => (ScrollViewer)logsItemsControl.Template.FindName("scrollViewer", logsItemsControl);
        }
    }
}
