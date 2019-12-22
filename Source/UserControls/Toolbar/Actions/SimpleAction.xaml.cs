using JsonFlier.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JsonFlier.UserControls.Toolbar.Actions
{
    /// <summary>
    /// Interaction logic for SimpleAction.xaml
    /// </summary>
    public partial class SimpleAction : UserControl
    {
        public SimpleAction(string iconName, ICommand command)
        {
            InitializeComponent();
            Command = command;

            iconImage.Source = new BitmapImage(new Uri("/JsonFlier;component/Resources/" + iconName, UriKind.Relative));
        }

        public ICommand Command { get; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Command.Execute();
        }
    }
}
