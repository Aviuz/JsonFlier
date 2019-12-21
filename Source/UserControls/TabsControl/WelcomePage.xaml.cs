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
        public delegate void OpenBookmarkEvent(object sender, Bookmark bookmark);

        public event OpenBookmarkEvent OnOpenBookmark;

        public WelcomePage()
        {
            InitializeComponent();
            LoadBookmarks();
        }

        private void LoadBookmarks()
        {
            foreach (var bookmark in BookmarkManager.Bookmarks)
            {
                var newButton = new Button() { Content = bookmark.Name };
                newButton.Click += new RoutedEventHandler((s, e) => OnOpenBookmark(this, bookmark));

                stackPanel.Children.Add(newButton);
            }
        }
    }
}
