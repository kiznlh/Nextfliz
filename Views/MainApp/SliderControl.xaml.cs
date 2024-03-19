﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Nextfliz.Views.MainApp
{
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
                FilmCardControl filmCard = new FilmCardControl();
                filmCard.Height = 300;
                filmCard.Width = 400;
                AddItem(filmCard);
            }
        }

        public void AddItem(UIElement item)
        {
            FilmCardControl filmCard = item as FilmCardControl;
            if (filmCard != null)
            {
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
            double itemWidth = 400; 
    
            double targetOffset = currentIndex * (itemWidth + 50 * 2);// spacing between items
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

        private void InitializeSlideTimer()
        {
            slideTimer = new DispatcherTimer();
            slideTimer.Interval = TimeSpan.FromMilliseconds(10); // Adjust this value for smoother or slower animation
        }

       

    }
}
