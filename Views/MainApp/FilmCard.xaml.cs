using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Nextfliz.Views.MainApp
{
    public partial class FilmCardControl: UserControl
    {
        private VisualBrush videoBackground;
        private MediaElement videoElement;

        // URI property
        public string URI { get; set; }

        // Default constructor
        public FilmCardControl()
        {
            InitializeComponent();

            // Initialize with a default URI
            InitializeVideoElement("C:\\Users\\Hai Nguyen Lam\\source\\repos\\TestNextflix\\Resources\\film_trailer.mp4");
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
            // Ensure the videoElement is not null and it's playing
            if (videoElement != null)
            {
                videoElement.Play();
                grid.Background = videoBackground;
            }
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            // Ensure the videoElement is not null and it's paused
            if (videoElement != null)
            {
                videoElement.Pause();
                videoElement.Position = TimeSpan.Zero;
                grid.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/film_bg.jpg")));
            }
        }
    }
}
