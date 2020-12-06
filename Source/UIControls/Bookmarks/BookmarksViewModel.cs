using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using JsonFlier.Context;
using JsonFlier.Context.Bookmarks;
using System.ComponentModel;

namespace JsonFlier.UIControls.Bookmarks
{
    public class BookmarksViewModel : ViewModelBase
    {
        private readonly ApplicationContext context;

        public BookmarksViewModel()
        {
            Bookmarks = new BindingList<SingleBookmarkViewModel>(
                new[]
                {
                    new SingleBookmarkViewModel( new Bookmark() { Name = "This is fake bookmark", FilePaths = new string[] { "C:/temp.txt" } }),
                    new SingleBookmarkViewModel( new Bookmark() { Name = "This is fake bookmark", FilePaths = new string[] { "C:/temp.txt" } }),
                    new SingleBookmarkViewModel( new Bookmark() { Name = "This is fake bookmark", FilePaths = new string[] { "C:/temp.txt" } }),
                    new SingleBookmarkViewModel( new Bookmark() { Name = "This is fake bookmark", FilePaths = new string[] { "C:/temp.txt" } }),
                    new SingleBookmarkViewModel( new Bookmark() { Name = "This is fake bookmark", FilePaths = new string[] { "C:/temp.txt" } }),
                }
            );
        }

        public BookmarksViewModel(ApplicationContext context)
        {
            this.context = context;

            Bookmarks = new BindingList<SingleBookmarkViewModel>();
            foreach (var bookmark in context.BookmarkManager.Bookmarks)
                Bookmarks.Add(new SingleBookmarkViewModel(bookmark));

            AddBookmarkCommand = new RelayCommand(AddBookmark);
            EditBookmarkCommand = new RelayCommand<SingleBookmarkViewModel>(EditBookmark, IsBookmarkNotNull);
            DeleteBookmarkCommand = new RelayCommand<SingleBookmarkViewModel>(DeleteBookmark, IsBookmarkNotNull);
        }

        private SingleBookmarkViewModel _SelectedBookmark;

        public SingleBookmarkViewModel SelectedBookmark
        {
            get => _SelectedBookmark;
            set
            {
                _SelectedBookmark = value;
                RaisePropertyChanged(nameof(SelectedBookmark));
                EditBookmarkCommand.RaiseCanExecuteChanged();
                DeleteBookmarkCommand.RaiseCanExecuteChanged();
            }
        }

        public BindingList<SingleBookmarkViewModel> Bookmarks { get; }

        public RelayCommand AddBookmarkCommand { get; }
        public RelayCommand<SingleBookmarkViewModel> EditBookmarkCommand { get; }
        public RelayCommand<SingleBookmarkViewModel> DeleteBookmarkCommand { get; }

        private void AddBookmark()
        {
            var window = new BookmarkDetailsWindow() { Title = "Create Bookmark" };
            window.ShowDialog();

            if (window.Result == System.Windows.MessageBoxResult.OK)
            {
                var bookmark = window.Bookmark.CreateBookmark();

                context.BookmarkManager.Bookmarks.Add(bookmark);
                context.BookmarkManager.SaveSettings();

                Bookmarks.Add(new SingleBookmarkViewModel(bookmark));
            }
        }

        private void EditBookmark(SingleBookmarkViewModel bookmark)
        {
            var window = new BookmarkDetailsWindow() { Title = "Edit Bookmark", DataContext = bookmark };
            window.ShowDialog();

            if (window.Result == System.Windows.MessageBoxResult.OK)
            {
                bookmark.UpdateBookmark();
                context.BookmarkManager.SaveSettings();

                RaisePropertyChanged(nameof(Bookmarks));
            }
        }

        private void DeleteBookmark(SingleBookmarkViewModel bookmark)
        {
            context.BookmarkManager.Bookmarks.Remove(bookmark.Bookmark);
            context.BookmarkManager.SaveSettings();

            Bookmarks.Remove(bookmark);
        }

        private bool IsBookmarkNotNull(object parameter)
        {
            return parameter is SingleBookmarkViewModel;
        }
    }
}
