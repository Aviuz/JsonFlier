using JsonFlier.Command;
using System.Windows.Controls;

namespace JsonFlier.UserControls.Toolbar.Actions
{
    public class ExpandTreeViewAction : SimpleActionSvg
    {
        public const string SvgData = "M0,0L1,0L1,1L0,1zM0.2,0.5l0.6,0M0.5,0.2l0,0.6";

        public ExpandTreeViewAction(TreeView treeView) : base(SvgData, new Command_ExpandTreeView(treeView)) { }
    }
}
