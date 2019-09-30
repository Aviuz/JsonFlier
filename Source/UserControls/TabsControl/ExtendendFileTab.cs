using System.Windows.Controls;
using JsonFlier.UserControls.Logs;

namespace JsonFlier.UserControls.TabsControl
{
    public class ExtendendFileTab : CloseableTab, IFileTab
    {
        public string FileName
        {
            get => Title;
            set => Title = value;
        }

        public string FilePath { get; set; }
    }
}
