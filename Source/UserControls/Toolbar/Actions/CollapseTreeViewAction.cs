using JsonFlier.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JsonFlier.UserControls.Toolbar.Actions
{
    public class CollapseTreeViewAction : SimpleActionSvg
    {
        public const string SvgData = "M0,0L1,0L1,1L0,1zM0.2,0.5l0.6,0";

        public CollapseTreeViewAction(TreeView treeView) : base(SvgData, new Command_CollapseTreeView(treeView)) { }
    }
}
