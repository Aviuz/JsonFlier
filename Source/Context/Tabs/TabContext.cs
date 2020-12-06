using JsonFlier.Enums;
using System.Collections.Generic;
using System.Windows.Controls;

namespace JsonFlier.Context.Tabs
{
    public class TabContext
    {
        public string Name { get; init; }
        public string FilePath { get; init; }
        public TabControlType TabControlType { get; init; }
        public Control TabInternalControl { get; init; }
        public IList<ToolBar> Toolbars { get; init; } = new List<ToolBar>();
    }
}
