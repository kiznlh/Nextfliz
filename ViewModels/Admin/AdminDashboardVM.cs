using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Nextfliz
{
    public class TopFilm
    {
        public string no { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string directorName { get; set; }
        public string directorImage { get; set; }
        public string sell { get; set; }
    }

    class AdminDashboardVM
    {
        public int sellingFilms { get; set; } = 0;
        public int sellingFilmsInDay { get; set; } = 0;
        public int sellingFilmsInWeek { get; set; } = 0;
        public int sellingFilmsInMonth { get; set; } = 0;
        public int chartType { get; set; } = 0;
        public SeriesCollection chartSeries { get; set; }
        public LineSeries DoanhThu {  get; set; }
        public LineSeries LoiNhuan { get; set; }
        public ObservableCollection<string> labels {  get; set; }
        public Func<double, string> YFormatter { get; set; }
        public ObservableCollection<TopFilm> list { get; set; } = new ObservableCollection<TopFilm>();
        private const int topFilmMax = 5;

        public AdminDashboardVM() {
            chartSeries = new SeriesCollection();
            labels = new ObservableCollection<string>();
            DoanhThu = new LineSeries
            {
                Title = "Doanh thu",
                Values = new ChartValues<double>()
            };
            LoiNhuan = new LineSeries
            {
                Title = "Lợi nhuận",
                Values = new ChartValues<double>()
            };

            chartSeries.Add(DoanhThu);
            chartSeries.Add(LoiNhuan);
            YFormatter = value => value.ToString("C");
            loadData();
            loadChartData();
        }

        private void loadData()
        {
            list.Clear();
            using (var context = new NextflizContext())
            {
                var topMovies = (from movie in context.Movies
                                 join ticket in context.Tickets on movie.MovieId equals ticket.MovieId
                                 group ticket by movie.MovieId into g
                                 orderby g.Count() descending
                                 select new {
                                     MovieId = g.Key,
                                     Sell = g.Count()
                                 }).Take(topFilmMax).ToList();
                int top = 1;
                if (topMovies.Count != 0)
                    foreach (var topMovie in topMovies)
                    {
                        string directorName = "Không có đạo diễn";
                        string directorImage = "";
                        var movie = context.Movies.FirstOrDefault(m => m.MovieId == topMovie.MovieId);

                        if (movie.DirectorId != null)
                        {
                            var director = context.Directors.FirstOrDefault(d => d.DirectorId == movie.DirectorId);

                            directorName = director.HoTen;
                            directorImage = director.HinhAnh;
                        }

                        list.Add(new TopFilm()
                        {
                            no = top.ToString(),
                            name = movie.TenPhim,
                            image = movie.HinhAnh,
                            directorName = directorName,
                            directorImage = directorImage,
                            sell = topMovie.Sell.ToString()
                        });
                        top++;
                    }

                DateTime current = DateTime.Now;
                CultureInfo culture = CultureInfo.CurrentCulture;
                Calendar calendar = culture.Calendar;
                var suatChieus = context.SuatChieus.ToList();
                sellingFilms = (from suatChieu in context.SuatChieus
                               where suatChieu.NgayGioChieu != null && suatChieu.NgayGioChieu >= DateTime.Now
                               orderby suatChieu.MovieId 
                               group suatChieu by suatChieu.MovieId into grouped
                               select grouped.FirstOrDefault()).Count();


                foreach (var suatChieu in suatChieus)
                {
                    if (suatChieu.NgayGioChieu.GetValueOrDefault().Day == current.Day && suatChieu.NgayGioChieu.GetValueOrDefault().Month == current.Month && suatChieu.NgayGioChieu.GetValueOrDefault().Year == current.Year)
                        sellingFilmsInDay++;
                    if (calendar.GetWeekOfYear(current, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek) == calendar.GetWeekOfYear(suatChieu.NgayGioChieu.Value, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek))
                        sellingFilmsInWeek++;
                    if (suatChieu.NgayGioChieu.GetValueOrDefault().Month == current.Month && suatChieu.NgayGioChieu.GetValueOrDefault().Year == current.Year)
                        sellingFilmsInMonth++;
                }
            }
        }

        public void loadChartData()
        {
            DoanhThu.Values.Clear();
            LoiNhuan.Values.Clear();
            labels.Clear();
            using (var context = new NextflizContext())
            {
                if (chartType == 0)
                {
                    var query = context.SuatChieus
                                    .Join(
                                        context.Tickets,
                                        suatChieu => suatChieu.SuatChieuId,
                                        ticket => ticket.SuatChieuId,
                                        (suatChieu, ticket) => new { SuatChieu = suatChieu, Ticket = ticket }
                                    )
                                    .GroupBy(
                                        x => x.Ticket.NgayDatVe.HasValue ? x.Ticket.NgayDatVe.Value.Date : DateTime.MinValue
                                    )
                                    .Select(group => new
                                    {
                                        NgayDatVe = group.Key,
                                        TongGiaVeSuatChieu = group.Sum(x => x.SuatChieu.GiaVe),
                                        TongGiaVeTicket = group.Sum(x => x.Ticket.GiaVe)
                                    })
                                    .ToList();
                    foreach (var item in query)
                    {
                        DoanhThu.Values.Add((double)item.TongGiaVeSuatChieu);
                        LoiNhuan.Values.Add((double)item.TongGiaVeTicket);
                        labels.Add(item.NgayDatVe.Date.ToString().Split(" ")[0]);
                    }
                }
                else if (chartType == 1)
                {
                    var query = context.SuatChieus
                                .Join(
                                    context.Tickets,
                                    suatChieu => suatChieu.SuatChieuId,
                                    ticket => ticket.SuatChieuId,
                                    (suatChieu, ticket) => new { SuatChieu = suatChieu, Ticket = ticket }
                                )
                                .AsEnumerable()
                                .GroupBy(ti => ti.Ticket.NgayDatVe.HasValue ? GetStartOfWeek(ti.Ticket.NgayDatVe.Value) : DateTime.MinValue)
                                .Select(group => new
                                {
                                    NgayDauTuan = group.Key,
                                    NgayCuoiTuan = group.Key.AddDays(6),
                                    TongGiaVeSuatChieu = group.Sum(x => x.SuatChieu.GiaVe),
                                    TongGiaVeTicket = group.Sum(x => x.Ticket.GiaVe)
                                })
                                .ToList();
                    foreach (var item in query)
                    {
                        DoanhThu.Values.Add((double)item.TongGiaVeSuatChieu);
                        LoiNhuan.Values.Add((double)item.TongGiaVeTicket);
                        labels.Add(item.NgayDauTuan.Day.ToString() + "/" + item.NgayDauTuan.Month.ToString() + "/" + item.NgayDauTuan.Year.ToString() + "-" + item.NgayCuoiTuan.Day.ToString() + "/" + item.NgayCuoiTuan.Month.ToString() + "/" + item.NgayCuoiTuan.Year.ToString());
                    }
                }
                else if (chartType == 2)
                {
                    var query = from ticket in context.Tickets
                                join suatChieu in context.SuatChieus
                                on ticket.SuatChieuId equals suatChieu.SuatChieuId
                                where ticket.NgayDatVe.HasValue
                                group new { ticket, suatChieu } by new
                                {
                                    Year = ticket.NgayDatVe.Value.Year,
                                    Month = ticket.NgayDatVe.Value.Month
                                } into grouped
                                orderby grouped.Key.Year, grouped.Key.Month
                                select new
                                {
                                    Year = grouped.Key.Year,
                                    Month = grouped.Key.Month,
                                    TongGiaVeTicket = grouped.Sum(x => x.ticket.GiaVe),
                                    TongGiaVeSuatChieu = grouped.Sum(x => x.suatChieu.GiaVe)
                                };
                    foreach (var item in query)
                    {
                        DoanhThu.Values.Add((double)item.TongGiaVeSuatChieu);
                        LoiNhuan.Values.Add((double)item.TongGiaVeTicket);
                        labels.Add(item.Month.ToString() + "/" + item.Year.ToString());
                    }
                }
                else if (chartType == 3)
                {
                    var query = from ticket in context.Tickets
                                join suatChieu in context.SuatChieus
                                on ticket.SuatChieuId equals suatChieu.SuatChieuId
                                where ticket.NgayDatVe.HasValue 
                                group new { ticket, suatChieu } by new
                                {
                                    Year = ticket.NgayDatVe.Value.Year
                                } into grouped
                                orderby grouped.Key.Year
                                select new
                                {
                                    Year = grouped.Key.Year,
                                    TongGiaVeTicket = grouped.Sum(x => x.ticket.GiaVe),
                                    TongGiaVeSuatChieu = grouped.Sum(x => x.suatChieu.GiaVe)
                                };
                    foreach (var item in query)
                    {
                        DoanhThu.Values.Add((double)item.TongGiaVeSuatChieu);
                        LoiNhuan.Values.Add((double)item.TongGiaVeTicket); 
                        labels.Add(item.Year.ToString());

                    }
                }
            }
        }

        public DateTime GetStartOfWeek(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

    }
}