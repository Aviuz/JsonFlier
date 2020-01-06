using Microsoft.Win32;

namespace JsonFlier.Command
{
    public class Command_OpenFile : ICommand
    {
        public Command_OpenFile(IFileExplorer fileExplorer)
        {
            FileExplorer = fileExplorer;
        }

        public IFileExplorer FileExplorer { get; }

        public bool IsEnabled => true;

        public void Execute()
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                FileExplorer.OpenFile(dialog.FileName);
            }
        }
    }
}
