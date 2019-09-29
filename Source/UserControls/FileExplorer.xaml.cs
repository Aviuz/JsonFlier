using JsonFlier.Bookmarks;
using JsonFlier.UserControls.Logs;
using JsonFlier.UserControls.TabsControl;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace JsonFlier.UserControls
{
    /// <summary>
    /// Interaction logic for FileExplorer.xaml
    /// </summary>
    public partial class FileExplorer : UserControl, IDisposable
    {
        // Constructors

        public FileExplorer()
        {
            InitializeComponent();

            BookmarkManager.BookmarksChanged += OnBookmarksChanged;

            OpenWelcomePage();
        }

        // Properties

        public ExtendedTabControl ExtendedTabControl => extendedTabControl;

        public IEnumerable<IFileControl> OpenedTabs => ExtendedTabControl.TabControl.Items.Cast<IFileControl>();

        public ItemCollection Items => ExtendedTabControl.TabControl.Items;

        public IFileControl OpenedTab => ExtendedTabControl.TabControl.SelectedItem as IFileControl;

        // Public methods

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

            OpenText(fileName, File.ReadAllText(path), path);
        }

        public void OpenText(string title, string text, string filePath = null)
        {
            JArray logsArray;
            try
            {
                logsArray = JArray.Parse(text);
                OpenJArray(title, logsArray, filePath);
            }
            catch
            {
                OpenSimpleText(title, text, filePath);
            }
        }

        public void OpenSimpleText(string title, string text, string filePath = null) => OpenTab(title, filePath, new PlainText(text));

        public void OpenJArray(string title, JArray jArray, string fileOrigin = null) => OpenTab(title, fileOrigin, new JsonArray(jArray, fileOrigin));

        public void OpenTab(string fileName, string path, UserControl userControl)
        {
            var tab = new ExtendendFileTab() { FileName = fileName, FilePath = path, Content = userControl };

            if (ExtendedTabControl.CustomPage != null)
            {
                ExtendedTabControl.CustomPage = null;
                ExtendedTabControl.TabControl.SelectedIndex = 0;
            }

            ExtendedTabControl.TabControl.Items.Add(tab);
            tab.OnClosed += OnTabClosed;
            tab.Focus();
        }

        // Private methods

        private void OpenWelcomePage()
        {
            var welcomePage = new WelcomePage();
            welcomePage.OpenButton.Click += OpenButton_Click;
            welcomePage.OnOpenBookmark += WelcomePage_OnOpenBookmark;
            ExtendedTabControl.CustomPage = welcomePage;
        }

        // Events

        private void OnTabClosed(object sender, EventArgs args)
        {
            if (ExtendedTabControl.TabControl.Items.Count == 0)
            {
                OpenWelcomePage();
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e) => ShowOpenFileDialog();

        private void WelcomePage_OnOpenBookmark(object sender, Bookmark bookmark) => BookmarkManager.OpenBookmark(bookmark, this);

        private void OnBookmarksChanged(object sender, EventArgs e)
        {
            OpenWelcomePage();
        }

        // Dispose

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}