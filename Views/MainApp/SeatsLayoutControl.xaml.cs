using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Nextfliz.Views.MainApp
{
    public partial class SeatsLayoutControl : UserControl
    {
        public SeatsLayoutControl()
        {
            InitializeComponent();
        }

        public event EventHandler<string> SeatClicked;

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
            "IsReadOnly", typeof(bool), typeof(SeatsLayoutControl), new PropertyMetadata(false));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        private void Seat_Click(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            if (grid != null && grid.Background != Brushes.DarkRed && !IsReadOnly)
            {
                // Toggle background color based on occupancy status
                grid.Background = grid.Background == Brushes.DarkGreen ? Brushes.DarkGoldenrod : Brushes.DarkGreen;

                // Extract the seat identifier from the grid name
                string seatId = grid.Name.Substring(0, 2);

                // Get the value of the clicked seat from the Tag property
                string seatValue = grid.Tag as string;

                // Raise the SeatClicked event with the seat identifier and value
                SeatClicked?.Invoke(this, $"{seatId}");
            }
        }

        public void SetSeatOccupied(string seatId)
        {
            Grid grid = FindName(seatId + "Grid") as Grid;
            if (grid != null)
            {
                grid.Background = Brushes.DarkRed;
            }
        }
    }
}
