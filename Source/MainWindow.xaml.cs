using JsonFlier.WindowUtilities;
using System;
using System.Windows;
using System.Windows.Media;

namespace JsonFlier
{
    /// <summary>
    /// Interaction logic for WindowFrame.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly WindowResizer resizer;

        public MainWindow()
        {
            InitializeComponent();
            resizer = new WindowResizer(this);
        }

        private void ToggleWindowMaximized()
        {
            WindowState ^= WindowState.Maximized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleWindowMaximized();
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                sizeIcon.Data = Geometry.Parse("M0,0L1,0L1,1L0,1L0,0L1,0");
            }
            else if (WindowState == WindowState.Maximized)
            {
                sizeIcon.Data = Geometry.Parse("M0,0L1,0L1,1L0,1L0,0L1,0M0.4,0L0.4,-0.4L1.4,-0.4L1.4,0.6L1,0.6");
            }
        }

    }
}
