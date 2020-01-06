using JsonFlier.Bookmarks;
using System.Windows;
using System.Windows.Controls;

namespace JsonFlier.UserControls.Configuration
{
    public class BookmarkComboBoxItem : ComboBoxItem
    {
        protected Bookmark bookmark;

        public Bookmark Bookmark
        {
            get => bookmark;

            set
            {
                bookmark = value;
                Content = bookmark.Name;
            }
        }
    }

    /// <summary>
    /// Interaction logic for AddToBookmarkWindow.xaml
    /// </summary>
    public partial class AddToBookmarkWindow : Window
    {
        public AddToBookmarkWindow(string fileName, string filePath, IFileExplorer fileManager)
        {
            Owner = Application.Current.MainWindow;

            InitializeComponent();

            FileName = fileName;
            FilePath = filePath;
            FileManager = fileManager;

            radioButtonNewBookmark.IsChecked = true;

            LoadBookmarks();
        }

        public IFileExplorer FileManager { get; private set; }

        public string FileName { get; private set; }

        public string FilePath { get; private set; }

        private void RadioButtonNew_Checked(object sender, RoutedEventArgs e)
        {
            radioButtonSelectBookmark.IsChecked = false;

            textBoxNewBookmarkName.IsEnabled = true;
            comboBoxSelectBookmark.IsEnabled = false;
        }

        private void RadioButtonSelect_Checked(object sender, RoutedEventArgs e)
        {
            radioButtonNewBookmark.IsChecked = false;

            textBoxNewBookmarkName.IsEnabled = false;
            comboBoxSelectBookmark.IsEnabled = true;
        }

        private void LoadBookmarks()
        {
            foreach(var bookmark in BookmarkManager.Bookmarks)
            {
                comboBoxSelectBookmark.Items.Add(new BookmarkComboBoxItem() { Bookmark = bookmark});
            }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (radioButtonNewBookmark.IsChecked.Value)
            {
                var bookmark = new Bookmark() { Name = textBoxNewBookmarkName.Text };
                bookmark.LogFiles.Add(new LogFileInfo() { Name = FileName, Path = FilePath });

                BookmarkManager.Bookmarks.Add(bookmark);
            }
            else
            {
                var bookmark = (comboBoxSelectBookmark.SelectedItem as BookmarkComboBoxItem)?.Bookmark;
                bookmark.LogFiles.Add(new LogFileInfo() { Name = FileName, Path = FilePath });
            }

            BookmarkManager.SaveSettings();
            Close();
        }
    }
}
