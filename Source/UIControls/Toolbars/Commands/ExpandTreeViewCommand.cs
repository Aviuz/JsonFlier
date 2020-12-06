using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace JsonFlier.UIControls.Toolbars.Commands
{
    public class ExpandTreeViewCommand : ICommand
    {
        public ExpandTreeViewCommand(TreeView treeView)
        {
            TreeView = treeView;
        }

        public TreeView TreeView { get; }

#pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;
#pragma warning restore

        public bool CanExecute(object parameter)
        {
            return TreeView != null;
        }

        public void Execute(object parameter)
        {
            foreach (TreeViewItem item in TreeView.Items)
            {
                item.ExpandSubtree();
            }
        }
    }
}
