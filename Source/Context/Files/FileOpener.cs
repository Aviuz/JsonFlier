using JsonFlier.Context.Tabs;
using Microsoft.Win32;
using System.IO;

namespace JsonFlier.Context.Files
{
    public class FileOpener
    {
        public TabManager TabManager { get; }

        public FileOpener(TabManager tabManager)
        {
            TabManager = tabManager;
        }

        public void OpenFile(string filePath)
        {
            var factory = FileControlsFactoriesRegistry.DetectDefaultFactory(filePath);

            var (control, toolbars) = factory.CreateControlAndToolbars(filePath);

            var tabContext = new TabContext()
            {
                Name = Path.GetFileName(filePath),
                FilePath = filePath,
                TabInternalControl = control,
                TabControlType = factory.TabControlType,
                Toolbars = toolbars,
            };

            TabManager.Add(tabContext);
            TabManager.CurrentTab = tabContext;
        }

        public void ShowDialog()
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                OpenFile(dialog.FileName);
            }
        }
    }
}
