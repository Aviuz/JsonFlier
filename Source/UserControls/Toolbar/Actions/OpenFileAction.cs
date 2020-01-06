using JsonFlier.Command;

namespace JsonFlier.UserControls.Toolbar.Actions
{
    public class OpenFileAction : SimpleAction
    {
        public OpenFileAction(IFileExplorer fileExplorer) : base("open_32.png", new Command_OpenFile(fileExplorer)) { }
    }
}
