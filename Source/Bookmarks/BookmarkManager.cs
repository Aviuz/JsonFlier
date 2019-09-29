using JsonFlier.UserControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonFlier.Bookmarks
{
    public static class BookmarkManager
    {
        public static IList<Bookmark> Bookmarks { get; private set; }

        public static event EventHandler BookmarksChanged;

        static BookmarkManager()
        {
            Bookmarks = new List<Bookmark>();
            LoadSettings();
        }

        public static void OpenBookmark(Bookmark bookmark, FileExplorer fileExplorer)
        {
            foreach (var logFile in bookmark.LogFiles)
            {
                fileExplorer.OpenFile(logFile.Path, logFile.Name);
            }
        }

        public static void OpenStartupBookmarks(FileExplorer fileManager)
        {
            foreach (var bookmark in Bookmarks.Where(f => f.OpenOnStartup))
            {
                foreach (var logFile in bookmark.LogFiles)
                {
                    fileManager.OpenFile(logFile.Path, logFile.Name);
                }
            }
        }

        /// <summary>
        /// Saves settings
        /// </summary>
        public static void SaveSettings()
        {
            var jArray = JArray.FromObject(Bookmarks.Select(f => f.Serialize()).ToArray());
            Properties.Settings.Default.Bookmarks = JsonConvert.SerializeObject(jArray);
            Properties.Settings.Default.Save();

            BookmarksChanged?.Invoke(null, EventArgs.Empty);
        }

        /// <summary>
        /// Loads or reset settings
        /// </summary>
        public static void LoadSettings()
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
