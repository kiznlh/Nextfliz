using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nextfliz.Views.MainApp
{
    /// <summary>
    /// Interaction logic for TestPageForNewControl.xaml
    /// </summary>
    public partial class TestPageForNewControl : Page
    {
        public TestPageForNewControl()
        {
            InitializeComponent();

            SeatsLayoutControl seatsLayoutControl = new SeatsLayoutControl();
            seatsLayoutControl.IsReadOnly = false;
            // Subscribe to the SeatClicked event
            seatsLayoutControl.SeatClicked += SeatsLayoutControl_SeatClicked;
            container.Children.Add(seatsLayoutControl);
            
            
        }
       

        // Event handler for the SeatClicked event
        private void SeatsLayoutControl_SeatClicked(object sender, string e)
        {
            // Extract the seat identifier and value from the event arguments
            string[] parts = e.Split(':');
            string seatId = parts[0].Trim();
            

            // Now you have the seat identifier and its value, you can use them as needed
            MessageBox.Show($"Clicked Seat: {seatId}");
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (Application.Current.MainWindow is WindowUserMainWindow mainWindow)
        //    {
        //        mainWindow.TestNavi();
        //    }
        //}
    }
}
