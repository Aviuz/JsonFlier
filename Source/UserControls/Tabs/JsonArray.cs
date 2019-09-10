using JsonFlier.UserControls.Tabs.Base;

namespace JsonFlier.UserControls.Tabs
{
    public class JsonArray : CloseableTab
    {
        public JsonArray()
        {
            Header = "Json";

            var content = new TabContents.JsonArray();

            this.Content = content;
        }
    }
}
