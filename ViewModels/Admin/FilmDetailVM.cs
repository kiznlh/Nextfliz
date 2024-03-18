using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nextfliz
{
    /*class SuatChieu
    {
        public string Date { get; set; }
        public string Time { get; set; }

    }*/
    class FilmDetailVM
    {
       // public ObservableCollection<SuatChieu> list { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public FilmDetailVM()
        {
            /*list = new ObservableCollection<SuatChieu>();
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });
            list.Add(new SuatChieu()
            {
                Date = "14/03/2024",
                Time = "1h30"
            });*/

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<double> { 100432424, 60342424, 50234234, 2024230 ,4124242 }
                },
                new LineSeries
                {
                    Title = "Lợi nhuận",
                    Values = new ChartValues<double> { 232424, 50342424, 30234234, 1024230 ,524242 }
                }
            };

            YFormatter = value => value.ToString("C");
            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
        }

        //Su dung listView cho danh sach dien vien de do mac cong
    }
}
