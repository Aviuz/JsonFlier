using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System;

namespace JsonFlier.UserControls.TabsControl
{
    public partial class CloseableTab : TabItem
    {
        public event EventHandler OnClosed;

        public string Title
        {
            get
            {
                return Header as string;
            }

            set
            {
                Header = value;
            }
        }

        public CloseableTab()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        // Close method
        public void Close()
        {
            ((TabControl)this.Parent).Items.Remove(this);
            if (OnClosed != null)
            {
                OnClosed.Invoke(this, new EventArgs());
            }
        }

        //
        // - - - Event Handlers  - - -
        //

        // Button Close Click - Remove the Tab - (or raise an event indicating a "CloseTab" event has occurred)
        void button_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TabTitle_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //((CloseableHeader)this.Header).button_close.Margin = new Thickness(((CloseableHeader)this.Header).label_TabTitle.ActualWidth + 5, 3, 4, 0);
        }
    }
}
