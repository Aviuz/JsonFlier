using JsonFlier.Bookmarks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JsonFlier.UserControls.Configuration
{
    /// <summary>
    /// Interaction logic for BookmarksManagment.xaml
    /// </summary>
    public partial class BookmarksManagementWindow : Window
    {
        private int bookmarksCount = 0;
        private bool changesMade = false;

        public BookmarksManagementWindow()
        {
            InitializeComponent();

            foreach(var bookmark in BookmarkManager.Bookmarks)
                AppendBookmark(bookmark);
        }

        private void AppendBookmark(Bookmark bookmark)
        {
            var guiItem = new SingleBookmarkConfiguration(bookmark);
            guiItem.OnChanged += GuiItem_OnChanged;
            guiItem.OnDeletion += GuiItem_OnDeletion;
            DockPanel.SetDock(guiItem, Dock.Top);
            dockPanel.Children.Insert(bookmarksCount++, guiItem);
        }

        private void AddBookmark_Click(object sender, RoutedEventArgs e)
        {
            var newBookmark = new Bookmark() { Name = "New bookmark" };
            BookmarkManager.Bookmarks.Add(newBookmark);
            AppendBookmark(newBookmark);

            changesMade = true;
        }

        private void GuiItem_OnChanged(object sender, EventArgs e)
        {
            changesMade = true;
        }

        private void GuiItem_OnDeletion(object sender, EventArgs e)
        {
            var guiItem = sender as SingleBookmarkConfiguration;

            if(guiItem == null || guiItem.Bookmark == null || !BookmarkManager.Bookmarks.Contains(guiItem.Bookmark))
            {
                throw new Exception("Cannot delete bookmark");
            }

            BookmarkManager.Bookmarks.Remove(guiItem.Bookmark);
            dockPanel.Children.Remove(guiItem);
            bookmarksCount--;

            changesMade = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (changesMade)
            {
                var result = MessageBox.Show("There were changes made. Do you want to save changes?", "Save changes", MessageBoxButton.YesNoCancel);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        BookmarkManager.SaveSettings();
                        break;

                    case MessageBoxResult.No:
                        BookmarkManager.LoadSettings();
                        break;

                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }
    }
}
