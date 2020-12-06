using JsonFlier.Context.Files;
using JsonFlier.Context.Tabs;
using JsonFlier.UIControls.Bookmarks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonFlier.Context.Bookmarks
{
    public class BookmarkManager
    {
        private readonly FileOpener fileOpener;

        public IList<Bookmark> Bookmarks { get; private set; }
        public event EventHandler BookmarksChanged;

        public BookmarkManager(FileOpener fileOpener)
        {
            Bookmarks = new List<Bookmark>();
            LoadSettings();
            this.fileOpener = fileOpener;
        }

        public void OpenBookmark(Bookmark bookmark)
        {
            foreach (var logFile in bookmark.FilePaths)
            {
                fileOpener.OpenFile(logFile/*, logFile.Name*/);
            }
        }

        public void OpenStartupBookmarks()
        {
            foreach (var bookmark in Bookmarks.Where(f => f.OpenOnStartup))
            {
                foreach (var logFile in bookmark.FilePaths)
                {
                    fileOpener.OpenFile(logFile/*, logFile.Name*/);
                }
            }
        }

        /// <summary>
        /// Saves settings
        /// </summary>
        public void SaveSettings()
        {
            var jArray = JArray.FromObject(Bookmarks.Select(f => f.Serialize()).ToArray());
            Properties.Settings.Default.Bookmarks = JsonConvert.SerializeObject(jArray);
            Properties.Settings.Default.Save();

            BookmarksChanged?.Invoke(null, EventArgs.Empty);
        }

        public void OpenBookmarkTab(ApplicationContext context)
        {
            var bookmarksTab = context.TabManager.Tabs.FirstOrDefault(t => t.TabControlType == Enums.TabControlType.Bookmarks);

            if (bookmarksTab == null)
            {
                bookmarksTab = new TabContext()
                {
                    Name = "Bookmarks",
                    TabControlType = Enums.TabControlType.Bookmarks,
                    FilePath = null,
                    TabInternalControl = new BookmarksControl() { DataContext = new BookmarksViewModel(context) },
                };
                context.TabManager.Add(bookmarksTab);
            }

            context.TabManager.CurrentTab = bookmarksTab;
        }

        /// <summary>
        /// Loads or reset settings
        /// </summary>
        public void LoadSettings()
        {
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Bookmarks))
            {
                try
                {
                    var bookmarksArray = JArray.Parse(Properties.Settings.Default.Bookmarks);

                    foreach (var bookmarkJson in bookmarksArray)
                    {
                        Bookmarks.Add(Bookmark.FromJson(bookmarkJson.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading config file: " + ex.ToString());
                }
            }
        }
    }
}
