using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Nextfliz.Views.MainApp
{
    /// <summary>
    /// Interaction logic for SliderControl.xaml
    /// </summary>
    public partial class SliderControl : UserControl
    {
        private double scrollOffset = 0;
        private int currentIndex = 0;
        private DispatcherTimer slideTimer;
        private EventHandler slideAnimationHandler;

        public SliderControl()
        {
            InitializeComponent();
            InitializeSlideTimer();

            for (int i = 0; i < 10; i++)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Width = 200;
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.FontSize = 24;
                textBlock.Foreground = Brushes.White;
                textBlock.Text = (i + 1).ToString(); // Index starts from 1
                AddItem(textBlock);
            }
        }

        public void AddItem(UIElement item)
        {
            wrapPanel.Children.Add(item);
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
            if (currentIndex < wrapPanel.Children.Count - 1)
            {
                currentIndex++;
                AnimateSlide();
            }
        }

        private void AnimateSlide()
        {
            double targetOffset = currentIndex * 200; // Adjust this value based on the size of each item
            double delta = Math.Sign(targetOffset - scrollOffset) * 10; // Adjust this value for smoother or slower animation

            slideTimer.Stop();
            slideTimer.Tick -= slideAnimationHandler; // Remove previous event handler

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

        private void InitializeSlideTimer()
        {
            slideTimer = new DispatcherTimer();
            slideTimer.Interval = TimeSpan.FromMilliseconds(10); // Adjust this value for smoother or slower animation
        }
    }
}
