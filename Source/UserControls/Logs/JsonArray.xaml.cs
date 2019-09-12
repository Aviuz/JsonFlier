using Newtonsoft.Json.Linq;
using System.Windows.Controls;

namespace JsonFlier.UserControls.Logs
{
    /// <summary>
    /// Interaction logic for JsonArray.xaml
    /// </summary>
    public partial class JsonArray : UserControl
    {
        public JsonArray(JArray logArray)
        {
            InitializeComponent();

            for (int i = 0; i < logArray.Count; i++)
            {
                AppendUserControl(new LogEntry(logArray[i] as JObject));
            }
        }

        private void AppendUserControl(UserControl userControl)
        {
            DockPanel.SetDock(userControl, Dock.Top);
            MainDockPanel.Children.Add(userControl);
        }

        public void Refresh()
        {
            MainDockPanel.Children.Clear();
        }
    }
}
