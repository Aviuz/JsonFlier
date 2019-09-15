using JsonFlier.UserControls.TabsControl;
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
            OpenText(Path.GetFileName(path), File.ReadAllText(path), path);
        }

        public void OpenText(string title, string text, string fileOrigin = null)
        {
            JArray logsArray;
            try
            {
                logsArray = JArray.Parse(text);
                TabControl.OpenJArray(title, logsArray, fileOrigin);
            }
            catch
            {
                TabControl.OpenSimpleText(title, text);
            }
        }
    }
}
