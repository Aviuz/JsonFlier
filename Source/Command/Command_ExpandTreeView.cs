using System.Windows.Controls;

namespace JsonFlier.Command
{
    public class Command_ExpandTreeView : ICommand
    {
        public Command_ExpandTreeView(TreeView treeView)
        {
            TreeView = treeView;
        }

        public TreeView TreeView { get; }

        public bool IsEnabled => TreeView != null;

        public void Execute()
        {
            foreach (TreeViewItem item in TreeView.Items)
            {
                item.ExpandSubtree();
            }
        }
    }
}
