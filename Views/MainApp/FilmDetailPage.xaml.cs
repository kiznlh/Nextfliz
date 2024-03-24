using Nextfliz.ViewModels.MainApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    /// Interaction logic for FilmDetailPage.xaml
    /// </summary>
    /// 
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.ToString("HH:mm dd/MM/yyyy");
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public partial class FilmDetailPage : Page
    {
        FilmDetailPageVM FilmDetailPageVM;
 
        public FilmDetailPage(string movieID)
        {
            InitializeComponent();

            FilmDetailPageVM = new FilmDetailPageVM(movieID);

            DataContext = FilmDetailPageVM;

            directorCard.ImageSource = FilmDetailPageVM.Directors.ImageSource;
            directorCard.HoTen = FilmDetailPageVM.Directors.HoTen;
            directorCard.TieuSu = FilmDetailPageVM.Directors.TieuSu;

            setSeats();
        }

        void setSeats()
        {
            seatLayout.reset();
            foreach (var seat in FilmDetailPageVM.BookedSeatList)
            {
                seatLayout.SetSeatOccupied(seat);
            }
        }

        private void timeStampComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setSeats();
        }
    }
}
