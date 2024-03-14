using System;
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
    public partial class FilmCardControl: UserControl
    {
        private double originalWidth;
        private double originalHeight;


        private VisualBrush videoBackground;
        private MediaElement videoElement;

        // URI property
        public string URI { get; set; }

        // Default constructor
        public FilmCardControl()
        {
            InitializeComponent();

            originalWidth = this.Width;
            originalHeight = this.Height;

            // Initialize with a default URI
            InitializeVideoElement("../../../Resources/Images/film_trailer.mp4");
        }

        // Method to initialize the video element
        private void InitializeVideoElement(string uriString)
        {
            // Create a new MediaElement instance
            videoElement = new MediaElement();
            // Set the Source to the provided URI string
            videoElement.Source = new Uri(uriString, UriKind.RelativeOrAbsolute);

            videoElement.LoadedBehavior = MediaState.Manual; // Changed to manual for manual control
                                                                 // Create a new VisualBrush with the new MediaElement
            videoBackground = new VisualBrush(videoElement);
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            ScaleControl(1.05); 
            //Ensure the videoElement is not null and it's playing
            if (videoElement != null)
            {
                videoElement.Play();
                grid.Background = videoBackground;
            }
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            ScaleControl(1.0); // Return to original size
            // Ensure the videoElement is not null and it's paused
            if (videoElement != null)
            {
                videoElement.Pause();
                videoElement.Position = TimeSpan.Zero;
                grid.Background = new ImageBrush(new BitmapImage(new Uri("../../../Resources/Images/film_bg.jpg", UriKind.RelativeOrAbsolute)));
            }
        }

        private void ScaleControl(double scaleFactor)
        {
            ScaleTransform scaleTransform = (ScaleTransform)grid.RenderTransform;
            scaleTransform.ScaleX = scaleFactor;
            scaleTransform.ScaleY = scaleFactor;
            this.Width = originalWidth * scaleFactor;
            this.Height = originalHeight * scaleFactor;
        }
    }
}
