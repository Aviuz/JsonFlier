using JsonFlier.UserControls.Toolbar.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JsonFlier.UserControls.Toolbar.ActionFactories
{
    public class FileExplorerActionFactory : IActionFactory
    {
        public FileExplorerActionFactory(IFileExplorer fileExplorer)
        {
            FileExplorer = fileExplorer;
        }

        public IFileExplorer FileExplorer { get; }

        public Control[] CreateActions() => CreateActionsCore().ToArray();

        private IEnumerable<Control> CreateActionsCore()
        {
            yield return new OpenFileAction(FileExplorer);
            yield return new AddToBookmarkAction(FileExplorer);
        }
    }
}
