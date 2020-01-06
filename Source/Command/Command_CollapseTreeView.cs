using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JsonFlier.Command
{
    public class Command_CollapseTreeView : ICommand
    {
        public Command_CollapseTreeView(TreeView treeView)
        {
            TreeView = treeView;
        }

        public TreeView TreeView { get; }

        public bool IsEnabled => TreeView != null;

        public void Execute()
        {
            foreach(TreeViewItem item in TreeView.Items)
            {
                item.IsExpanded = false;
            }
        }
    }
}
