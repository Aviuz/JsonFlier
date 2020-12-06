using System.Windows;

namespace JsonFlier.UIControls.Bookmarks
{
    /// <summary>
    /// Interaction logic for BookmarkDetailsWindow.xaml
    /// </summary>
    public partial class BookmarkDetailsWindow : Window
    {
        public MessageBoxResult Result { get; private set; }

        public SingleBookmarkViewModel Bookmark => DataContext as SingleBookmarkViewModel;

        public BookmarkDetailsWindow()
        {
            InitializeComponent();
            Result = MessageBoxResult.Cancel;
        }

        private void Ok_OnClick(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            Close();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            Close();
        }
    }
}
