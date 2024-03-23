using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace Nextfliz.Views.MainApp
{
    public partial class HotMovieSliderControl : UserControl
    {
        private double scrollOffset = 0;
        private int currentIndex = 0;
        private DispatcherTimer slideTimer;
        private EventHandler slideAnimationHandler;

        public HotMovieSliderControl()
        {
            InitializeComponent();
            InitializeSlideTimer();

            //for (int i = 0; i < 10; i++)
            //{
            //    FilmCardControl filmCard = new FilmCardControl();
            //    filmCard.Height = 450;
            //    filmCard.Width = 600;
            //    AddItem(filmCard);
            //}
        }

        public void AddItem(UIElement item)
        {
            FilmCardControl filmCard = item as FilmCardControl;
            if (filmCard != null)
            {
                filmCard.Height = 450;
                filmCard.Width = 600;
                double halfWidth = (1000 - (filmCard.Width)) / 2;
  
                filmCard.Margin = new Thickness(halfWidth, 0, halfWidth, 0); 
                filmCard.HorizontalAlignment = HorizontalAlignment.Left;
            }
            panel.Children.Add(item);

           
        }

        private void SlideLeft_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                AnimateSlide();
            }
        }

        private void SlideRight_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex < panel.Children.Count - 1)
            {
                currentIndex++;
                AnimateSlide();
            }

        }

        private void AnimateSlide()
        {
            double itemWidth = 600; 
            double halfWidth = (1000 - (itemWidth)) / 2;

            double targetOffset = currentIndex * (itemWidth + halfWidth * 2);// spacing between items
            double delta = Math.Sign(targetOffset - scrollOffset) * 20; // Adjust the scrolling speed

           

            slideTimer.Stop();
            slideTimer.Tick -= slideAnimationHandler; 

            slideAnimationHandler = (sender, e) =>
            {
                if (Math.Abs(targetOffset - scrollOffset) < Math.Abs(delta))
                {
                    scrollOffset = targetOffset;
                    scrollViewer.ScrollToHorizontalOffset(scrollOffset);
                    slideTimer.Stop();
                }
                else
                {
                    scrollOffset += delta;
                    scrollViewer.ScrollToHorizontalOffset(scrollOffset);
                }
            };
                
            slideTimer.Tick += slideAnimationHandler;
            slideTimer.Start();
        }

        private void DisableButtons()
        {
            slideLeft.IsEnabled = currentIndex > 0;
            slideRight.IsEnabled = currentIndex < panel.Children.Count - 1;
        }

        private void EnableButtons()
        {
            slideLeft.IsEnabled = true;
            slideRight.IsEnabled = true;
        }
        private void InitializeSlideTimer()
        {
            slideTimer = new DispatcherTimer();
            slideTimer.Interval = TimeSpan.FromMilliseconds(10); // Adjust this value for smoother or slower animation
        }
    }
}
