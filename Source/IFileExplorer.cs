using JsonFlier.UserControls.TabsControl;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Windows.Controls;

namespace JsonFlier
{
    public interface IFileExplorer
    {
        IFileTab OpenedTab { get; }
        IEnumerable<IFileTab> OpenedTabs { get; }

        void OpenFile(string path, string fileName = null);
        void OpenJArray(string title, JArray jArray, string fileOrigin = null);
        void OpenSimpleText(string title, string text, string filePath = null);
        void OpenTab(string fileName, string path, UserControl userControl);
        void OpenText(string title, string text, string filePath = null);
        void ShowOpenFileDialog();
        void OpenStartUpBookmarks();
    }
}