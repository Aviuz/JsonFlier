using GalaSoft.MvvmLight.Command;
using JsonFlier.Context;
using JsonFlier.Context.Bookmarks;
using JsonFlier.Context.Tabs;
using System.ComponentModel;
using System.Windows.Input;

namespace JsonFlier.UIControls
{
    public class StartPageViewModel
    {
        private readonly ApplicationContext context;

        public StartPageViewModel()
        {
            Bookmarks = new BindingList<Bookmark>()
            {
                new Bookmark() { Name = "this is fake bookmark", FilePaths=new string[]{ "test"} },
                new Bookmark() { Name = "this is bookmark"},
                new Bookmark() { Name = "this is  fake fake bookmark"},
                new Bookmark() { Name = "this is even faker fake bookmark"},
            };
        }

        public StartPageViewModel(ApplicationContext context)
        {
            this.context = context;

            Bookmarks = new BindingList<Bookmark>(context.BookmarkManager.Bookmarks);

            OpenBookmarkCommand = new RelayCommand<Bookmark>(OpenBookmark);
            ManageBookmarksCommand = new RelayCommand(() => context.BookmarkManager.OpenBookmarkTab(context), true);
            OpenFileCommand = new RelayCommand(context.FileOpener.ShowDialog);
        }

        public BindingList<Bookmark> Bookmarks { get; }

        public ICommand OpenBookmarkCommand { get; }
        public ICommand OpenFileCommand { get; }
        public ICommand ManageBookmarksCommand { get; }

        public void OpenBookmark(Bookmark bookmark)
        {
            context.BookmarkManager.OpenBookmark(bookmark);
        }

        private TabContext tabContext;
        public void SetDeletionOnNewTab(TabManager tabManager, TabContext startPageTabContext)
        {
            tabManager.OnAddedTab += SetDeletionOnNewTab_OnTabAdded;
            tabContext = startPageTabContext;
        }

        public void SetDeletionOnNewTab_OnTabAdded(TabManager tabManager, TabContext newTab)
        {
            tabManager.Remove(tabContext);
            tabManager.OnAddedTab -= SetDeletionOnNewTab_OnTabAdded;
        }
    }
}
