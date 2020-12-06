using JsonFlier.Enums;
using JsonFlier.UIControls.LogTabs.JsonLog;
using JsonFlier.UIControls.Toolbars.PredefinedToolbars;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace JsonFlier.Context.Files.ConcreteFactories
{
    public class JsonLoggerFileContextFactory : IFileControlsFactory
    {
        public TabControlType TabControlType => TabControlType.StructuredLog;

        public bool CanOpenFile(string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var textReader = new StreamReader(fileStream))
                return LogEntryViewModel.CanBeParsedFrom(textReader.ReadLine());
        }

        public (Control, IList<ToolBar>) CreateControlAndToolbars(string filePath)
        {
            // UserControl
            var control = new UIControls.LogTabs.JsonLog.StructuredLogControl();
            control.DataContext.FilePath = filePath;
            _ = control.DataContext.LoadAsync(control.Dispatcher);

            return (control, new[] { StructuredLogToolbar.Create(control) });
        }
    }
}
