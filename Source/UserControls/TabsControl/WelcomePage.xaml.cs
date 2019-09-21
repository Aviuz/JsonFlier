using JsonFlier.Bookmarks;
using System.Windows;
using System.Windows.Controls;

namespace JsonFlier.UserControls.TabsControl
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : UserControl
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        public void LoadBookmarks(FileManager fileManager)
        {
            foreach (var bookmark in BookmarkManager.Bookmarks)
            {
                var newButton = new Button() { Content = bookmark.Name, Style = (Style)Resources["ButtonTemplate"] };
                newButton.Click += new RoutedEventHandler((s, e) => BookmarkManager.OpenBookmark(bookmark, fileManager));

                stackPanel.Children.Add(newButton);
            }
        }
    }
}
