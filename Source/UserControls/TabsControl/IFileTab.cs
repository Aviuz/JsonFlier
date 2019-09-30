using System.Windows.Controls;

namespace JsonFlier.UserControls.TabsControl
{
    public interface IFileTab
    {
        string FileName { get; }

        string FilePath { get; }

        bool IsFocused { get; }

        object Content { get; }
    }
}