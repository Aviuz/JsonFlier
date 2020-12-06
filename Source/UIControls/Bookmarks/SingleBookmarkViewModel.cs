using GalaSoft.MvvmLight;
using JsonFlier.Context.Bookmarks;
using System.Linq;

namespace JsonFlier.UIControls.Bookmarks
{
    public class SingleBookmarkViewModel : ViewModelBase
    {
        public SingleBookmarkViewModel() { }

        public SingleBookmarkViewModel(Bookmark bookmark)
        {
            Bookmark = bookmark;

            Name = bookmark.Name;
            FilePath = string.Join("; ", bookmark.FilePaths);
            OpenOnStartup = bookmark.OpenOnStartup;
        }

        public Bookmark Bookmark { get; private set; }

        private string _Name;
        public string Name { get => _Name; set { _Name = value; RaisePropertyChanged(nameof(Name)); } }

        private string _FilePath;
        public string FilePath { get => _FilePath; set { _FilePath = value; RaisePropertyChanged(nameof(FilePath)); } }

        private bool? _OpenOnStartup = false;

        public bool? OpenOnStartup { get => _OpenOnStartup; set { _OpenOnStartup = value; RaisePropertyChanged(nameof(OpenOnStartup)); } }

        public void UpdateBookmark()
        {
            Bookmark.Name = Name;
            Bookmark.FilePaths = FilePath.Split(";").Select(f => f.Trim()).ToArray();
            Bookmark.OpenOnStartup = OpenOnStartup ?? false;
        }

        public Bookmark CreateBookmark()
        {
            Bookmark = new Bookmark();

            UpdateBookmark();

            return Bookmark;
        }
    }
}
