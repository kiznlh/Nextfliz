using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nextfliz.Views.MainApp
{
    public partial class ActorDetailCard : UserControl
    {
        // Define dependency properties
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ActorDetailCard));

        public static readonly DependencyProperty Text1Property =
            DependencyProperty.Register("Text1", typeof(string), typeof(ActorDetailCard));

        public static readonly DependencyProperty Text2Property =
            DependencyProperty.Register("Text2", typeof(string), typeof(ActorDetailCard));

        // Properties to set and get values from dependency properties
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public string Text1
        {
            get { return (string)GetValue(Text1Property); }
            set { SetValue(Text1Property, value); }
        }

        public string Text2
        {
            get { return (string)GetValue(Text2Property); }
            set { SetValue(Text2Property, value); }
        }

        public ActorDetailCard()
        {
            InitializeComponent();

            
        }
    }
}
