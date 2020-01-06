using JsonFlier.Command;

namespace JsonFlier.UserControls.Toolbar.Actions
{
    public class AddToBookmarkAction : SimpleAction
    {
        public AddToBookmarkAction(IFileExplorer fileExplorer) : base("bookmark_32.png", new Command_AddToBookmark(fileExplorer)) { }
    }
}
