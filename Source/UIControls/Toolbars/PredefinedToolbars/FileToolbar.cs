using GalaSoft.MvvmLight.Command;
using JsonFlier.Context;
using JsonFlier.UIControls.Toolbars.ActionControls;
using JsonFlier.UIControls.Toolbars.Commands;
using System.Windows.Controls;

namespace JsonFlier.UIControls.Toolbars.PredefinedToolbars
{
    public static class FileToolbar
    {
        public static ToolBar Create(ApplicationContext context)
        {
            return new ToolBar()
            {
                ItemsSource = new[]
                {
                    new ToolbarButton() { Command = new RelayCommand(context.FileOpener.ShowDialog), Source = "Folder" },
                    new ToolbarButton() { Command = new AddToBookmarkCommand(context), Source = "Star" },
                },
            };
        }
    }
}
