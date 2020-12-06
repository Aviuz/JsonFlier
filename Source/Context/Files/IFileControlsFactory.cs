using JsonFlier.Enums;
using System.Collections.Generic;
using System.Windows.Controls;

namespace JsonFlier.Context.Files
{
    public interface IFileControlsFactory
    {
        TabControlType TabControlType { get; }
        bool CanOpenFile(string filePath);
        (Control, IList<ToolBar>) CreateControlAndToolbars(string filePath);
    }
}
