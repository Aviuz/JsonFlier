using JsonFlier.UserControls.Tabs.Base;
using System.Text;

namespace JsonFlier.UserControls.Tabs
{
    public class PlainText : CloseableTab
    {
        public PlainText(string title, string text)
        {
            Title = title;

            var content = new TabContents.PlainText();

            content.TextBlock.Text = text;

            int lineCount = text.Split('\n').Length;

            var sb = new StringBuilder();

            for (int i = 1; i <= lineCount; i++)
            {
                sb.AppendLine(i.ToString());
            }

            content.LnBlock.Text = sb.ToString();

            Content = content;
        }
    }
}
