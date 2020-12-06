using JsonFlier.Enums;
using JsonFlier.UIControls.LogTabs.PlainText;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace JsonFlier.Context.Files.ConcreteFactories
{
    public class PlainTextFactory : IFileControlsFactory
    {
        public TabControlType TabControlType => TabControlType.StructuredLog;

        public bool CanOpenFile(string filePath)
        {
            return true;
        }

        public (Control, IList<ToolBar>) CreateControlAndToolbars(string filePath)
        {
            // TODO refactor to include async loader
            using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var textReader = new StreamReader(fileStream))
            {
                var control = new PlainText(textReader.ReadToEnd(), filePath);
                return (control, new ToolBar[0]);
            }
        }
    }
}
