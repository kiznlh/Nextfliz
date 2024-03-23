using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nextfliz.Views.MainApp
{
    public partial class ActorAndDirectorDetailCard : UserControl
    {
        // Define dependency properties
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(string), typeof(ActorAndDirectorDetailCard));

        public static readonly DependencyProperty hoTenProperty =
            DependencyProperty.Register("HoTen", typeof(string), typeof(ActorAndDirectorDetailCard));

        public static readonly DependencyProperty tieuSuProperty =
            DependencyProperty.Register("TieuSu", typeof(string), typeof(ActorAndDirectorDetailCard));

        // Properties to set and get values from dependency properties
        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public string HoTen
        {
            get { return (string)GetValue(hoTenProperty); }
            set { SetValue(hoTenProperty, value); }
        }

        public string TieuSu
        {
            get { return (string)GetValue(tieuSuProperty); }
            set { SetValue(tieuSuProperty, value); }
        }

        public ActorAndDirectorDetailCard()
        {
            InitializeComponent();

        }

    }
}
