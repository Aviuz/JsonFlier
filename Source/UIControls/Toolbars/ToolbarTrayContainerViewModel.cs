using System.ComponentModel;
using System.Windows.Controls;

namespace JsonFlier.UIControls.Toolbars
{
    public class ToolbarTrayContainerViewModel
    {
        public BindingList<ToolBar> Toolbars { get; set; } = new BindingList<ToolBar>();
    }
}
