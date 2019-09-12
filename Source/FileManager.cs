using JsonFlier.UserControls;
using JsonFlier.UserControls.Tabs;
using JsonFlier.UserControls.Tabs.Base;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System.IO;

namespace JsonFlier
{
    public class FileManager
    {
        public FileManager(ExtendedTabControl extendedTabControl)
        {
            TabControl = extendedTabControl;
        }

        public ExtendedTabControl TabControl { get; set; }

        public void ShowOpenFileDialog()
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                OpenFile(dialog.FileName);
            }
        }

        public void OpenFile(string path)
        {
            OpenText(Path.GetFileName(path), File.ReadAllText(path));
        }

        public void OpenText(string title, string text)
        {
            JArray logsArray;
            try
            {
                logsArray = JArray.Parse(text);
                OpenNewTab(new JsonArray(title, logsArray));
            }
            catch
            {
                OpenNewTab(new PlainText(title, text));
            }
        }

        public void OpenNewTab(CloseableTab tab) => TabControl.Open(tab);
    }
}
