using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Shapes;

using LiveCharts;
using LiveCharts.Wpf;

namespace Nextfliz.Views
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public Admin()
        {
            InitializeComponent();

            ObservableCollection<TopFilm> list = new ObservableCollection<TopFilm>();
            list.Add(new TopFilm()
            {
                no = "1",
                name = "Chàng trai năm ấy",
                director = "Vũ",
                date = "31-01-2023",
                sell = "1000.001"
            });
            list.Add(new TopFilm()
            {
                no = "2",
                name = "Chàng trai năm ấy",
                director = "Vũ",
                date = "31-01-2023",
                sell = "1000.001"
            });
            list.Add(new TopFilm()
            {
                no = "3",
                name = "Chàng trai năm ấy",
                director = "Vũ",
                date = "31-01-2023",
                sell = "1000.001"
            }); 
            list.Add(new TopFilm()
            {
                no = "4",
                name = "Chàng trai năm ấy",
                director = "Vũ",
                date = "31-01-2023",
                sell = "1000.001"
            });
            list.Add(new TopFilm()
            {
                no = "4",
                name = "Chàng trai năm ấy",
                director = "Vũ",
                date = "31-01-2023",
                sell = "1000.001"
            });

            TopFilmDataGrid.ItemsSource = list;

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> { 432424, 60342424, 50234234, 2024230 ,4124242 }
                },
                new LineSeries
                {
                    Title = "Lợi nhuận",
                    Values = new ChartValues<double> { 232424, 50342424, 30234234, 1024230 ,524242 }
                }
            };

            YFormatter = value => value.ToString("C");
            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            DataContext = this;

            SalesCombobox.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class TopFilm
    {
        public string no { get; set; }
        public string name { get; set; }
        public string director { get; set; }
        public string date { get; set; }
        public string sell { get; set; }
    }
}
