using JsonFlier.Bookmarks;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace JsonFlier.UserControls.Configuration
{
    /// <summary>
    /// Interaction logic for SingleBookmarkConfiguration.xaml
    /// </summary>
    public partial class SingleBookmarkConfiguration : UserControl
    {
        public bool collapsed = false;

        public event EventHandler OnChanged;
        public event EventHandler OnDeletion;

        public SingleBookmarkConfiguration(Bookmark bookmark)
        {
            InitializeComponent();
            Bookmark = bookmark;

            textBoxName.Text = bookmark.Name;

            var bindingList = new BindingList<LogFileInfo>(bookmark.LogFiles);
            dataGrid.ItemsSource = bindingList;
            bindingList.ListChanged += new ListChangedEventHandler((s, e) => NotifyChange());

            checkBoxStartup.IsChecked = bookmark.OpenOnStartup;

            Collapsed = true;
        }

        public Bookmark Bookmark { get; set; }

        public bool Collapsed
        {
            get => collapsed;
            set
            {
                if (collapsed != value)
                {
                    if (value)
                    {
                        dataGrid.Visibility = Visibility.Collapsed;
                        buttonCollapse_up.Visibility = Visibility.Collapsed;
                        buttonCollapse_down.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        dataGrid.Visibility = Visibility.Visible;
                        buttonCollapse_up.Visibility = Visibility.Visible;
                        buttonCollapse_down.Visibility = Visibility.Collapsed;
                    }

                    collapsed = value;
                }
            }
        }

        private void NotifyChange() => OnChanged?.Invoke(this, new EventArgs());

        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Bookmark.Name = textBoxName.Text;
            NotifyChange();
        }

        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            NotifyChange();
        }

        private void ButtonCollapse_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Collapsed = !Collapsed;
        }

        private void CheckBoxChanged_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBoxStartup.IsChecked.HasValue && Bookmark != null && Bookmark.OpenOnStartup != checkBoxStartup.IsChecked.Value)
            {
                Bookmark.OpenOnStartup = checkBoxStartup.IsChecked.Value;
                NotifyChange();
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e) => OnDeletion?.Invoke(this, new EventArgs());
    }
}
