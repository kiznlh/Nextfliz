using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Nextfliz.Views
{
    /// <summary>
    /// Interaction logic for FilmManagement.xaml
    /// </summary>
    public partial class FilmManagement : Window
    {
        public FilmManagement()
        {
            InitializeComponent();
            ObservableCollection<TopFilm> list = new ObservableCollection<TopFilm>();
            list.Add(new TopFilm()
            {
                no = "1",
                name = "Chàng trai năm ấy",
                director = "Nguyễn Văn A",
                date = "31-01-2023",
                sell = "1000.001"
            });
            list.Add(new TopFilm()
            {
                no = "2",
                name = "Chàng trai năm ấy",
                director = "Nguyễn Văn A",
                date = "31-01-2023",
                sell = "1000.001"
            });
            list.Add(new TopFilm()
            {
                no = "3",
                name = "Chàng trai năm ấy",
                director = "Nguyễn Văn A",
                date = "31-01-2023",
                sell = "1000.001"
            });
            list.Add(new TopFilm()
            {
                no = "4",
                name = "Chàng trai năm ấy",
                director = "Nguyễn Văn A",
                date = "31-01-2023",
                sell = "1000.001"
            });
            list.Add(new TopFilm()
            {
                no = "4",
                name = "Chàng trai năm ấy",
                director = "Nguyễn Văn A",
                date = "31-01-2023",
                sell = "1000.001"
            });
            list.Add(new TopFilm()
            {
                no = "1",
                name = "Chàng trai năm ấy",
                director = "Nguyễn Văn A",
                date = "31-01-2023",
                sell = "1000.001"
            });
            list.Add(new TopFilm()
            {
                no = "2",
                name = "Chàng trai năm ấy",
                director = "Nguyễn Văn A",
                date = "31-01-2023",
                sell = "1000.001"
            });
            list.Add(new TopFilm()
            {
                no = "3",
                name = "Chàng trai năm ấy",
                director = "Nguyễn Văn A",
                date = "31-01-2023",
                sell = "1000.001"
            });
            list.Add(new TopFilm()
            {
                no = "4",
                name = "Chàng trai năm ấy",
                director = "Nguyễn Văn A",
                date = "31-01-2023",
                sell = "1000.001"
            });
            list.Add(new TopFilm()
            {
                no = "4",
                name = "Chàng trai năm ấy",
                director = "Nguyễn Văn A",
                date = "31-01-2023",
                sell = "1000.001"
            });
            list.Add(new TopFilm()
            {
                no = "1",
                name = "Chàng trai năm ấy",
                director = "Nguyễn Văn A",
                date = "31-01-2023",
                sell = "1000.001"
            });
            list.Add(new TopFilm()
            {
                no = "2",
                name = "Chàng trai năm ấy",
                director = "Nguyễn Văn A",
                date = "31-01-2023",
                sell = "1000.001"
            });
            list.Add(new TopFilm()
            {
                no = "3",
                name = "Chàng trai năm ấy",
                director = "Nguyễn Văn A",
                date = "31-01-2023",
                sell = "1000.001"
            });
            

            TopFilmDataGrid.ItemsSource = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
