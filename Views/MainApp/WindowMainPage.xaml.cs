using System.Windows;
using System.Windows.Controls;

namespace Nextfliz.Views.MainApp
{
    public partial class WindowMainPage : Page
    {
        public WindowMainPage()
        {
            InitializeComponent();

            // Subscribe to the Loaded event of HotMovieSliderControl
            hotMovieSliderControl.Loaded += HotMovieSliderControl_Loaded;
        }

        private void HotMovieSliderControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Access the ActualWidth property of HotMovieSliderControl
            double hotMovieSliderWidth = hotMovieSliderControl.ActualWidth;
            // Use the width as needed
            MessageBox.Show($"HotMovieSliderControl Width: {hotMovieSliderWidth}");
        }
    }
}
