using JsonFlier.UserControls.Tabs.Base;

namespace JsonFlier.UserControls.Tabs
{
    public class PlainText : CloseableTab
    {
        public PlainText(string text)
        {
            var content = new TabContents.PlainText();

            content.TextBlock.Text = text;

            Content = content;
        }
    }
}
