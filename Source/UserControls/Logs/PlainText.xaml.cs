using System.Text;
using System.Windows.Controls;

namespace JsonFlier.UserControls.Logs
{
    /// <summary>
    /// Interaction logic for PlainText.xaml
    /// </summary>
    public partial class PlainText : UserControl
    {
        public PlainText(string text)
        {
            InitializeComponent();

            TextBlock.Text = text;

            int lineCount = text.Split('\n').Length;

            var sb = new StringBuilder();

            for (int i = 1; i <= lineCount; i++)
            {
                sb.AppendLine(i.ToString());
            }

            LnBlock.Text = sb.ToString();
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            LeftScroller.ScrollToVerticalOffset(RightScroller.VerticalOffset);
        }
    }
}
