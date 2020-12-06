using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JsonFlier.UIControls.Toolbars.ActionControls
{
    /// <summary>
    /// Interaction logic for SimpleToolbarButton.xaml
    /// </summary>
    public partial class ToolbarButton : UserControl
    {
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(string), typeof(ToolbarButton), new PropertyMetadata(null));

        public static readonly DependencyProperty CommandProperty =
           DependencyProperty.Register("Command", typeof(ICommand), typeof(ToolbarButton), new PropertyMetadata(null));

        public ToolbarButton()
        {
            InitializeComponent();
        }

        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}
