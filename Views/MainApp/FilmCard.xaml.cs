using Nextfliz.ViewModels.MainApp;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Nextfliz.Views.MainApp
{
    public class CenterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double size)
                return size / 2;
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class FilmCardControl : UserControl
    {
        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
            "Width", typeof(double), typeof(FilmCardControl), new PropertyMetadata(double.NaN));

        public static readonly DependencyProperty HeightProperty = DependencyProperty.Register(
            "Height", typeof(double), typeof(FilmCardControl), new PropertyMetadata(double.NaN));

        public double ControlWidth
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }
        public double ControlHeight
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        private double originalWidth;
        private double originalHeight;

        private VisualBrush videoBackground;
        private MediaElement videoElement;

        // For Binding filmcontrol
        public static readonly DependencyProperty ImageBGProperty = DependencyProperty.Register(
        "ImageBG", typeof(string), typeof(FilmCardControl), new PropertyMetadata(null));

        public static readonly DependencyProperty TenPhimProperty = DependencyProperty.Register(
            "TenPhim", typeof(string), typeof(FilmCardControl), new PropertyMetadata(null));

        public static readonly DependencyProperty CertificationProperty = DependencyProperty.Register(
            "Certification", typeof(string), typeof(FilmCardControl), new PropertyMetadata(null));

        public static readonly DependencyProperty ThoiLuongProperty = DependencyProperty.Register(
            "ThoiLuong", typeof(string), typeof(FilmCardControl), new PropertyMetadata(null));

        private string _movieID;

        public string MovieID
        {
            get { return _movieID; }

            set { _movieID = value; }
        }
        public string ImageBG
        {
            get { return (string)GetValue(ImageBGProperty); }
            set { SetValue(ImageBGProperty, value); }
        }

        public string TenPhim
        {
            get { return (string)GetValue(TenPhimProperty); }
            set { SetValue(TenPhimProperty, value); }
        }

        public string Certification
        {
            get { return (string)GetValue(CertificationProperty); }
            set { SetValue(CertificationProperty, value); }
        }

        public string ThoiLuong
        {
            get { return (string)GetValue(ThoiLuongProperty); }
            set { SetValue(ThoiLuongProperty, value + " phút"); }
        }

        public FilmCardControl()
        {
            InitializeComponent();

            originalWidth = this.ControlWidth;
            originalHeight = this.ControlHeight;

            // Initialize video trailer
            InitializeVideoElement("../../../Resources/Images/film_trailer.mp4");

        }

        private void InitializeVideoElement(string uriString)
        {
        
            videoElement = new MediaElement();
         
            videoElement.Source = new Uri(uriString, UriKind.RelativeOrAbsolute);

            videoElement.LoadedBehavior = MediaState.Manual;

            videoBackground = new VisualBrush(videoElement);
        }

        private void Container_MouseEnter(object sender, MouseEventArgs e)
        {
            ScaleControl(1.3);
            normalShadow.Visibility = Visibility.Collapsed;
            normalName.Visibility = Visibility.Collapsed;
            normalDetail.Visibility = Visibility.Collapsed;

            highlightShadow.Visibility = Visibility.Visible;
            highlightName.Visibility = Visibility.Visible;
            highlightDetail.Visibility = Visibility.Visible;
            highlightMoreDetail.Visibility = Visibility.Visible;
            if (videoElement != null)
            {
                videoElement.Play();
                container.Background = videoBackground;
            }
        }

        private void Container_MouseLeave(object sender, MouseEventArgs e)
        {
            ScaleControl(1.0);
            normalShadow.Visibility = Visibility.Visible;
            normalName.Visibility = Visibility.Visible;
            normalDetail.Visibility = Visibility.Visible;

            highlightShadow.Visibility = Visibility.Collapsed;
            highlightName.Visibility = Visibility.Collapsed;
            highlightDetail.Visibility = Visibility.Collapsed;
            highlightMoreDetail.Visibility = Visibility.Collapsed;
            if (videoElement != null)
            {
                videoElement.Pause();
                videoElement.Position = TimeSpan.Zero;
                container.Background = ImageBackground;
            }
        }

        private void ScaleControl(double scaleFactor)
        {
            ScaleTransform scaleTransform = (ScaleTransform)container.RenderTransform;
            scaleTransform.ScaleX = scaleFactor;
            scaleTransform.ScaleY = scaleFactor;
            this.ControlWidth = originalWidth * scaleFactor;
            this.ControlHeight = originalHeight * scaleFactor;
        }

        private void container_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            FilmDetailPage filmDetailPage = new FilmDetailPage(MovieID);

            if (Application.Current.MainWindow is WindowUserMainWindow mainWindow)
            {
                mainWindow.navigateToAPage(filmDetailPage);
            }
        }
    }
}