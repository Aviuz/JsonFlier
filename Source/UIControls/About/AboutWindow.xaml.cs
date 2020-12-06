using System.Reflection;
using System.Windows;

namespace JsonFlier.UIControls.About
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            versionText.Text += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
