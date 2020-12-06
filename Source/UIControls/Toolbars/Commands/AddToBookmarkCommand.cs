using JsonFlier.Context;
using JsonFlier.Context.Bookmarks;
using JsonFlier.Context.Tabs;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace JsonFlier.UIControls.Toolbars.Commands
{
    public class AddToBookmarkCommand : ICommand, IDisposable
    {
        private readonly ApplicationContext context;

        public AddToBookmarkCommand(ApplicationContext context)
        {
            this.context = context;
            this.context.TabManager.OnTabChanged += TabManager_OnTabChanged;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return context.TabManager.CurrentTab?.FilePath != null;
        }

        public void Execute(object parameter)
        {
            var tab = context.TabManager.CurrentTab;
            if (tab != null && !string.IsNullOrWhiteSpace(tab.FilePath))
            {
                //var window = new AddToBookmarkWindow(context.FileName, context.FilePath, FileExplorer);
                //window.ShowDialog();
                string filePath = context.TabManager.CurrentTab.FilePath;
                context.BookmarkManager.Bookmarks.Add(new Bookmark() { FilePaths = new string[] { filePath }, Name = Path.GetFileName(filePath), OpenOnStartup = false });
                context.BookmarkManager.SaveSettings();
            }
            else
            {
                MessageBox.Show("You need to have opened file", "Can't add bookmark");
            }
        }
        private void TabManager_OnTabChanged(TabManager sender, TabContext oldTab, TabContext newTab)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public void Dispose()
        {
            context.TabManager.OnTabChanged -= TabManager_OnTabChanged;
        }
    }
}
