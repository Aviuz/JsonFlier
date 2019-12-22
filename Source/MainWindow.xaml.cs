using JsonFlier.Bookmarks;
using JsonFlier.UserControls.Configuration;
using JsonFlier.UserControls.Logs;
using JsonFlier.UserControls.TabsControl;
using JsonFlier.UserControls.Toolbar.ActionFactories;
using JsonFlier.Utilities;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JsonFlier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IFileExplorer, IDisposable
    {
        // Fields
        private List<object> loadedBookmarks = new List<object>();

        // Constructor
        public MainWindow()
        {
            InitializeComponent();

            LoadBookmarsToMenu();
            BookmarkManager.BookmarksChanged += OnBookmarksChanged;
            ExtendedTabControl.TabControl.SelectionChanged += TabControl_SelectionChanged;

            OpenStartUpBookmarks();
            OpenWelcomePage();
        }

        // Properties
        public ExtendedTabControl ExtendedTabControl => extendedTabControl;

        public IEnumerable<IFileTab> OpenedTabs => ExtendedTabControl.TabControl.Items.Cast<IFileTab>();

        public ItemCollection Items => ExtendedTabControl.TabControl.Items;

        public IFileTab OpenedTab => ExtendedTabControl.TabControl.SelectedItem as IFileTab;

        public bool CanRefreshActiveTab
        {
            get
            {
                var tabContent = OpenedTab?.Content as IRefreshable;

                return tabContent != null && tabContent.CanRefresh;
            }
        }

        // Public methods (IFileExplorer)
        public void ShowOpenFileDialog()
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                OpenFile(dialog.FileName);
            }
        }

        public void OpenFile(string path, string fileName = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                fileName = Path.GetFileName(path);

            IEnumerable<JObject> logsArray;
            if (JArrayParser.TryParse(File.OpenRead(path), Encoding.UTF8, out logsArray))
            {
                OpenJArray(fileName, logsArray, path);
            }
            else
            {
                OpenSimpleText(fileName, File.ReadAllText(path), path);
            }
        }

        public void OpenText(string title, string text, string filePath = null)
        {
            //JArray logsArray;
            //try
            //{
            //    logsArray = JArray.Parse(text);
            //    OpenJArray(title, logsArray, filePath);
            //}
            //catch
            //{
            //    OpenSimpleText(title, text, filePath);
            //}
        }

        public void OpenSimpleText(string title, string text, string filePath = null)
        {
            var control = new PlainText(text, filePath);
            //var toolbarControls = new JsonArrayActionFactory(control).CreateActions();
            OpenTab(title, filePath, control, new Control[] { });
        }

        public void OpenJArray(string title, IEnumerable<JObject> jArray, string filePath = null)
        {
            var control = new JsonArray(jArray, filePath);
            var toolbarControls = new JsonArrayActionFactory(control).CreateActions();
            OpenTab(title, filePath, control, toolbarControls);
        }

        public void OpenTab(string fileName, string path, UserControl userControl, Control[] toolbarControls)
        {
            var tab = new ExtendendFileTab() { FileName = fileName, FilePath = path, Content = userControl, ToolbarControls = toolbarControls };

            if (ExtendedTabControl.CustomPage != null)
            {
                ExtendedTabControl.CustomPage = null;
                ExtendedTabControl.TabControl.SelectedIndex = 0;
            }

            ExtendedTabControl.TabControl.Items.Add(tab);
            tab.OnClosed += OnTabClosed;
            tab.Focus();

            OnTabOpened();
        }

        public void OpenStartUpBookmarks()
        {
            foreach (var bookmark in BookmarkManager.Bookmarks.Where(b => b.OpenOnStartup))
            {
                BookmarkManager.OpenBookmark(bookmark, this);
            }
        }

        // Private methods
        private void OpenWelcomePage()
        {
            var welcomePage = new WelcomePage();
            welcomePage.OpenButton.Click += WelcomePage_OnOpenButton;
            welcomePage.OnOpenBookmark += WelcomePage_OnOpenBookmark;
            ExtendedTabControl.CustomPage = welcomePage;
        }

        private void LoadBookmarsToMenu()
        {
            foreach (var bookmarkMenuItem in loadedBookmarks)
            {
                menuItemBookmark.Items.Remove(bookmarkMenuItem);
            }

            loadedBookmarks.Clear();

            foreach (var bookmark in BookmarkManager.Bookmarks)
            {
                var newMenuItem = new MenuItem() { Header = bookmark.Name, };
                newMenuItem.Click += new RoutedEventHandler((s, e) => BookmarkManager.OpenBookmark(bookmark, this));

                menuItemBookmark.Items.Add(newMenuItem);
                loadedBookmarks.Add(newMenuItem);
            }
        }

        private void RefreshAvailabilityOfControls()
        {
            menuItemRefresh.IsEnabled = CanRefreshActiveTab;
            menuItemClose.IsEnabled = OpenedTab != null;
        }

        private void RefreshToolbar()
        {
            if (OpenedTab != null)
            {
                toolbar.LoadToolbarActions(OpenedTab.ToolbarControls);
            }
            else
            {
                toolbar.Clear();
            }
        }

        // Event Handlers
        private void OpenFile_Click(object sender, RoutedEventArgs e) => ShowOpenFileDialog();

        private void BookmarksManagement_Click(object sender, RoutedEventArgs e)
        {
            var window = new BookmarksManagementWindow();
            window.ShowDialog();
        }

        private void AddBookmark_Click(object sender, RoutedEventArgs e)
        {
            var openedTab = this.OpenedTab;
            if (openedTab != null && openedTab.FileName != null && !string.IsNullOrWhiteSpace(openedTab.FilePath))
            {
                var window = new AddToBookmarkWindow(openedTab.FileName, openedTab.FilePath, this);
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

            if (ExtendedTabControl.CustomPage != null)
                OpenWelcomePage();
        }

        private void OnTabOpened()
        {
            RefreshAvailabilityOfControls();
        }

        private void OnTabClosed(object sender, EventArgs args)
        {
            if (ExtendedTabControl.TabControl.Items.Count == 0)
            {
                OpenWelcomePage();
            }

            RefreshAvailabilityOfControls();
        }

        private void WelcomePage_OnOpenButton(object sender, RoutedEventArgs e) => ShowOpenFileDialog();

        private void WelcomePage_OnOpenBookmark(object sender, Bookmark bookmark) => BookmarkManager.OpenBookmark(bookmark, this);

        private void MenuItemRefresh_Click(object sender, RoutedEventArgs e)
        {
            var tabContent = OpenedTab?.Content as IRefreshable;

            if (tabContent == null)
            {
                throw new Exception("OpenedTab must be not null and implement IRefreshable interface");
            }

            if (tabContent.CanRefresh)
            {
                tabContent.Refresh();
            }
        }

        private void MenuItemClose_Click(object sender, RoutedEventArgs e)
        {
            var tab = OpenedTab as CloseableTab;

            if (tab == null)
                throw new Exception("OpenedTab must be not null");

            tab.Close();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshAvailabilityOfControls();
            RefreshToolbar();
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount == 2)
                {
                    AdjustWindowSize();
                }
                else
                {
                    Application.Current.MainWindow.DragMove();
                }
            }
        }

        private void AdjustWindowSize()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                ResizeMode = ResizeMode.CanResizeWithGrip;
                this.WindowState = WindowState.Normal;
                MaxButton.Content = new Image() { Source = new BitmapImage(new Uri("/JsonFlier;component/Resources/maximize_16.png", UriKind.Relative)) };
            }
            else
            {
                ResizeMode = ResizeMode.NoResize;
                this.WindowState = WindowState.Maximized;
                MaxButton.Content = new Image() { Source = new BitmapImage(new Uri("/JsonFlier;component/Resources/collapse_16.png", UriKind.Relative)) };
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            AdjustWindowSize();
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Dispose
        public void Dispose()
        {
            BookmarkManager.BookmarksChanged -= OnBookmarksChanged;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}
