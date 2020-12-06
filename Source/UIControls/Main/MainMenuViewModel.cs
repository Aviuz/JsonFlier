using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using JsonFlier.Context;
using JsonFlier.Context.Bookmarks;
using JsonFlier.UIControls.About;
using System.ComponentModel;
using System.Windows.Input;

namespace JsonFlier.UIControls.Main
{
    public class MainMenuViewModel : ViewModelBase
    {
        private readonly ApplicationContext context;

        public MainMenuViewModel()
        {
            OpenBookmarkCommand = new RelayCommand<Bookmark>(OpenBookmark);
            OpenAboutWindowCommand = new RelayCommand(OpenAboutWindow);
        }

        public MainMenuViewModel(ApplicationContext context) : this()
        {
            this.context = context;

            ManageBookmarksCommand = new RelayCommand(() => context.BookmarkManager.OpenBookmarkTab(context), true);
            OpenFileCommand = new RelayCommand(context.FileOpener.ShowDialog);

            Bookmarks = new BindingList<Bookmark>(context.BookmarkManager.Bookmarks);
        }

        public BindingList<Bookmark> Bookmarks { get; set; }

        public ICommand OpenFileCommand { get; }
        public ICommand ManageBookmarksCommand { get; }
        public ICommand OpenBookmarkCommand { get; }
        public ICommand OpenAboutWindowCommand { get; }

        public void OpenBookmark(Bookmark bookmark)
        {
            context.BookmarkManager.OpenBookmark(bookmark);
        }

        public void OpenAboutWindow()
        {
            var window = new AboutWindow();
            window.ShowDialog();
        }
    }
}
