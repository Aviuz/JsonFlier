using System.Windows.Controls;

namespace JsonFlier.UserControls.Tabs.TabContents
{
    /// <summary>
    /// Interaction logic for PlainText.xaml
    /// </summary>
    public partial class PlainText : UserControl
    {
        public PlainText()
        {
            InitializeComponent();
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            LeftScroller.ScrollToVerticalOffset(RightScroller.VerticalOffset);
        }
    }
}
