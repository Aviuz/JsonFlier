using JsonFlier.Bookmarks;
using JsonFlier.UserControls;
using JsonFlier.UserControls.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace JsonFlier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        private List<object> loadedBookmarks = new List<object>();

        public MainWindow()
        {
            InitializeComponent();

            LoadBookmarsToMenu();
            BookmarkManager.BookmarksChanged += OnBookmarksChanged;

            OpenStartUpBookmarks();
        }

        public FileExplorer FileExplorer => fileExplorer;

        private void LoadBookmarsToMenu()
        {
            foreach(var bookmarkMenuItem in loadedBookmarks)
            {
                menuItemBookmark.Items.Remove(bookmarkMenuItem);
            }

            loadedBookmarks.Clear();

            foreach (var bookmark in BookmarkManager.Bookmarks)
            {
                var newMenuItem = new MenuItem() { Header = bookmark.Name, };
                newMenuItem.Click += new RoutedEventHandler((s, e) => BookmarkManager.OpenBookmark(bookmark, FileExplorer));

                menuItemBookmark.Items.Add(newMenuItem);
                loadedBookmarks.Add(newMenuItem);
            }
        }

        public void OpenStartUpBookmarks()
        {
            foreach (var bookmark in BookmarkManager.Bookmarks.Where(b => b.OpenOnStartup))
            {
                BookmarkManager.OpenBookmark(bookmark, fileExplorer);
            }
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e) => FileExplorer.ShowOpenFileDialog();

        private void BookmarksManagement_Click(object sender, RoutedEventArgs e)
        {
            var window = new BookmarksManagementWindow();
            window.ShowDialog();
        }

        private void AddBookmark_Click(object sender, RoutedEventArgs e)
        {
            var openedTab = FileExplorer.OpenedTab;
            if (openedTab != null && openedTab.FileName != null && !string.IsNullOrWhiteSpace(openedTab.FilePath))
            {
                var window = new AddToBookmarkWindow(openedTab.FileName, openedTab.FilePath, FileExplorer);
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("You need to have opened file", "Can't add bookmark");
            }
        }

        private void OnBookmarksChanged(object sender, EventArgs e)
        {
            LoadBookmarsToMenu();
        }

        public void Dispose()
        {
            BookmarkManager.BookmarksChanged -= OnBookmarksChanged;
        }
    }
}
