using JsonFlier.UserControls.Toolbar.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JsonFlier.UserControls.Toolbar.ActionFactories
{
    public class TreeViewActionFactory : IActionFactory
    {
        public TreeViewActionFactory(TreeView treeView)
        {
            TreeView = treeView;
        }

        public TreeView TreeView { get; }

        public Control[] CreateActions() => CreateActionsCore().ToArray();

        private IEnumerable<Control> CreateActionsCore()
        {
            yield return new CollapseTreeViewAction(TreeView);
            yield return new ExpandTreeViewAction(TreeView);
        }
    }
}
