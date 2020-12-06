using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JsonFlier.ImageVectors
{
    /// <summary>
    /// Interaction logic for VectorImage.xaml
    /// </summary>
    public partial class VectorImage : Control
    {
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Brush), typeof(VectorImage), new PropertyMetadata(Brushes.Black));

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(FrameworkElement), typeof(VectorImage), null);

        public VectorImage()
        {
            InitializeComponent();
        }

        public Brush Color
        {
            get => (Brush)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public FrameworkElement Source
        {
            get => (FrameworkElement)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }
    }
}
