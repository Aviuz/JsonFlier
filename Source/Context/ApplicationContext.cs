using JsonFlier.Context.Bookmarks;
using JsonFlier.Context.Files;
using JsonFlier.Context.Tabs;

namespace JsonFlier.Context
{
    public class ApplicationContext
    {
        public FileOpener FileOpener { get; private set; }
        public BookmarkManager BookmarkManager { get; private set; }
        public TabManager TabManager { get; private set; }

        public ApplicationContext()
        {
            TabManager = new TabManager(this);
            FileOpener = new FileOpener(TabManager);
            BookmarkManager = new BookmarkManager(FileOpener);
        }
    }
}
