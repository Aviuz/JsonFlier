using JsonFlier.Bookmarks;
using JsonFlier.UserControls.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace JsonFlier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FileManager = new FileManager();

            FileManager.TabControl = tabControl;
            tabControl.FileManager = FileManager;

            LoadBookmarsToMenu();
            tabControl.LoadBookmarks();

            OpenStartUpBookmarks();
        }

        public FileManager FileManager { get; set; }

        private void LoadBookmarsToMenu()
        {
            foreach (var bookmark in BookmarkManager.Bookmarks)
            {
                var newMenuItem = new MenuItem() { Header = bookmark.Name, };
                newMenuItem.Click += new RoutedEventHandler((s, e) => BookmarkManager.OpenBookmark(bookmark, FileManager));

                menuItemBookmark.Items.Add(newMenuItem);
            }
        }

        public void OpenStartUpBookmarks()
        {
            foreach (var bookmark in BookmarkManager.Bookmarks.Where(b => b.OpenOnStartup))
            {
                BookmarkManager.OpenBookmark(bookmark, FileManager);
            }
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e) => FileManager.ShowOpenFileDialog();

        private void BookmarksManagement_Click(object sender, RoutedEventArgs e)
        {
            var window = new BookmarksManagementWindow();
            window.ShowDialog();
        }
    }
}
