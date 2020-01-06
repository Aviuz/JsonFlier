using JsonFlier.UserControls.Configuration;
using System.Windows;

namespace JsonFlier.Command
{
    public class Command_AddToBookmark : ICommand
    {
        public Command_AddToBookmark(IFileExplorer fileExplorer)
        {
            FileExplorer = fileExplorer;
        }

        public IFileExplorer FileExplorer { get; }

        public bool IsEnabled => FileExplorer.OpenedTab?.FilePath != null;

        public void Execute()
        {
            var openedTab = FileExplorer.OpenedTab;
            if (openedTab != null && openedTab.FileName != null && !string.IsNullOrWhiteSpace(openedTab.FilePath))
            {
                var window = new AddToBookmarkWindow(openedTab.FileName, openedTab.FilePath, FileExplorer);
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("You need to have opened file", "Can't add bookmark");
            }
        }
    }
}
