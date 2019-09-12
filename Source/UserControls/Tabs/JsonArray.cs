using JsonFlier.UserControls.Tabs.Base;
using Newtonsoft.Json.Linq;

namespace JsonFlier.UserControls.Tabs
{
    public class JsonArray : CloseableTab
    {
        public JsonArray(string title, JArray logArray)
        {
            Title = title;

            var content = new TabContents.JsonArray();

            for (int i = 0; i < logArray.Count; i++)
            {
                content.logArrayControl.AppendUserControl(new LogEntry(logArray[i] as JObject));
            }

            this.Content = content;
        }
    }
}
