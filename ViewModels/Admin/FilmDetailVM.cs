using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using Nextfliz.Views.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Nextfliz
{
    public class SuatChieuItem
    {
        public string id { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public SuatChieuItem(string id, string date, string time)
        {
            this.id = id;
            this.date = date;
            this.time = time;
        }
    }

    class FilmDetailVM : INotifyPropertyChanged
    {
        private const string noImage = "https://hosting.ca/wp-content/uploads/2017/09/broken-image.png";
        private string filmId;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Actor> actorList { get; set; } = new ObservableCollection<Actor>();
        public ObservableCollection<SuatChieuItem> suatChieuList { get; set; } = new ObservableCollection<SuatChieuItem>();
        private string name { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }
        private string image { get; set; }
        public string Image
        {
            get { return image; }
            set
            {
                if (image != value)
                {
                    image = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Image"));
                }
            }
        }
        private string time { get; set; }
        public string Time
        {
            get { return time; }
            set
            {
                if (time != value)
                {
                    time = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Time"));
                }
            }
        }
        private string rating { get; set; }
        public string Rating
        {
            get { return rating; }
            set
            {
                if (rating != value)
                {
                    rating = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rating"));
                }
            }
        }
        private string year { get; set; }
        public string Year
        {
            get { return year; }
            set
            {
                if (year != value)
                {
                    year = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Year"));
                }
            }
        }
        private string certification { get; set; }
        public string Certification
        {
            get { return certification; }
            set
            {
                if (certification != value)
                {
                    certification = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Certification"));
                }
            }
        }
        private string genre { get; set; }
        public string Genre
        {
            get { return genre; }
            set
            {
                if (genre != value)
                {
                    genre = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Genre"));
                }
            }
        }
        private string directorName { get; set; }
        public string DirectorName
        {
            get { return directorName; }
            set
            {
                if (directorName != value)
                {
                    directorName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DirectorName"));
                }
            }
        }
        private string directorImage { get; set; }
        public string DirectorImage
        {
            get { return directorImage; }
            set
            {
                if (directorImage != value)
                {
                    directorImage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DirectorImage"));
                }
            }
        }
        private string directorBio { get; set; }
        public string DirectorBio
        {
            get { return directorBio; }
            set
            {
                if (directorBio != value)
                {
                    directorBio = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DirectorBio"));
                }
            }
        }
        private SuatChieuItem selectedItem { get; set; }
        public SuatChieuItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedItem"));
                }
            }
        }
        public int totalChartType { get; set; } = 0;
        public SeriesCollection totalChartSeries { get; set; }
        public LineSeries totalDoanhThu { get; set; }
        public LineSeries totalLoiNhuan { get; set; }
        public ObservableCollection<string> totalLabels { get; set; }
        public SuatChieuItem selectedComboboxItem {  get; set; }
        public int shChartType { get; set; } = 0;
        public SeriesCollection shChartSeries { get; set; }
        public ColumnSeries shDoanhThu { get; set; }
        public ColumnSeries shLoiNhuan { get; set; }
        public ObservableCollection<string> shLabels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public RelayCommand editFilmCommand { get; set; }
        public RelayCommand addSuatChieuCommand { get; set; }
        public RelayCommand editSuatChieuCommand { get; set; }
        public RelayCommand deleteSuatChieuCommand { get; set; }

        public FilmDetailVM(string filmId)
        {
            selectedItem = null;
            this.filmId = filmId;
            updateContent();
            loadSuatChieu();
            editFilmCommand = new RelayCommand(editFilm, canPerform);
            addSuatChieuCommand = new RelayCommand(addSuatChieu, canPerform);
            editSuatChieuCommand = new RelayCommand(editSuatChieu, canPerform);
            deleteSuatChieuCommand = new RelayCommand(deleteSuatChieu, canPerform);

            totalChartSeries = new SeriesCollection();
            totalLabels = new ObservableCollection<string>();
            totalDoanhThu = new LineSeries
            {
                Title = "Doanh thu",
                Values = new ChartValues<double>()
            };
            totalLoiNhuan = new LineSeries
            {
                Title = "Lợi nhuận",
                Values = new ChartValues<double>()
            };

            totalChartSeries.Add(totalDoanhThu);
            totalChartSeries.Add(totalLoiNhuan);
            loadTotalChartData();

            shChartSeries = new SeriesCollection();
            shLabels = new ObservableCollection<string>();
            shDoanhThu = new ColumnSeries
            {
                Title = "Doanh thu",
                Values = new ChartValues<double>()
            };
            shLoiNhuan = new ColumnSeries
            {
                Title = "Lợi nhuận",
                Values = new ChartValues<double>()
            };

            shChartSeries.Add(shDoanhThu);
            shChartSeries.Add(shLoiNhuan);
            selectedComboboxItem = suatChieuList.Count > 0? suatChieuList[0] : null;
            loadSHChartData();

            YFormatter = value => value.ToString("C");
            this.filmId = filmId;
        }

        private void loadSuatChieu()
        {
            suatChieuList.Clear();
            using (var dbContext = new NextflizContext())
            {
                var suatChieus = dbContext.SuatChieus.Where(s => s.MovieId == filmId);
                foreach (SuatChieu sc in suatChieus)
                {
                    string minute = sc.NgayGioChieu.GetValueOrDefault().Minute < 10 ? "0" + sc.NgayGioChieu.GetValueOrDefault().Minute : sc.NgayGioChieu.GetValueOrDefault().Minute.ToString();
                    suatChieuList.Add(new SuatChieuItem(sc.SuatChieuId, sc.NgayGioChieu.GetValueOrDefault().Day + "/" + sc.NgayGioChieu.GetValueOrDefault().Month + "/" + sc.NgayGioChieu.GetValueOrDefault().Year, sc.NgayGioChieu.GetValueOrDefault().Hour + ":" + minute));
                }
            }
        }
        private void updateContent()
        {
            actorList.Clear();
            using (var dbContext = new NextflizContext())
            {
                var item = dbContext.Movies.FirstOrDefault(a => a.MovieId == filmId);
                image = item.HinhAnh;
                name = item.TenPhim;
                rating = item.DiemDanhGia.ToString();
                year = item.NamPhatHanh.ToString();
                time = item.ThoiLuong.ToString();
                certification = item.Certification;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Image"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rating"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Year"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Time"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Certification"));

                if (item.GenreId != null)
                {
                    var itemGenre = dbContext.Genres.FirstOrDefault(a => a.GenreId == item.GenreId);
                    genre = itemGenre.TenTheLoai;
                }
                else
                {
                    genre = "Không có thể loại";
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Genre"));

                if (item.DirectorId != null)
                {
                    var director = dbContext.Directors.FirstOrDefault(d => d.DirectorId == item.DirectorId);
                    directorImage = director.HinhAnh;
                    directorName = director.HoTen;
                    directorBio = director.TieuSu;
                }
                else
                {
                    directorImage = noImage;
                    directorName = "Không có đạo diễn";
                    directorBio = "Thiếu thông tin đạo diễn ";
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DirectorImage"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DirectorName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DirectorBio"));

                var actorsInMovie = (
                    from actor in dbContext.Actors
                    join filmCast in dbContext.FilmCasts on actor.ActorId equals filmCast.ActorId
                    where filmCast.MovieId == item.MovieId
                    select new
                    {
                        actor.ActorId,
                        actor.HoTen,
                        actor.TieuSu,
                        actor.HinhAnh,
                    }
                ).ToList();
                foreach (var actor in actorsInMovie )
                {
                    Actor actorToAdd = new Actor();
                    actorToAdd.ActorId = actor.ActorId;
                    actorToAdd.HinhAnh = actor.HinhAnh;
                    actorToAdd.HoTen = actor.HoTen;
                    actorToAdd.TieuSu = actor.TieuSu;
                    actorList.Add(actorToAdd);
                }
            }
        }

        private void editFilm(object value)
        {
            AddFilmWindow editWindow = new AddFilmWindow(filmId);
            editWindow.ShowDialog();
            updateContent();
        }


        private void addSuatChieu(object value)
        {
            AddSuatChieu addWindow = new AddSuatChieu(null, filmId);
            addWindow.ShowDialog();
            loadSuatChieu();
        }

        private void editSuatChieu(object value)
        {
            if (selectedItem != null)
            {
                AddSuatChieu addWindow = new AddSuatChieu(selectedItem.id, filmId);
                addWindow.ShowDialog();
                loadSuatChieu();
            }
        }

        private void deleteSuatChieu(object value)
        {
            if (selectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn tiếp tục? \n Lưu ý: xoá suất chiếu sẽ xóa luôn vé của khách hàng đã đặt ở suất chiếu này", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SuatChieu.DeleteSuatChieu(selectedItem.id);
                    loadSuatChieu();
                }
            }
        }

        private bool canPerform(object value)
        {
            return true;
        }

        public void loadTotalChartData()
        {
            totalDoanhThu.Values.Clear();
            totalLoiNhuan.Values.Clear();
            totalLabels.Clear();
            using (var context = new NextflizContext())
            {
                if (totalChartType == 0)
                {
                    var query = context.SuatChieus
                                    .Where(x => x.MovieId == filmId)
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
                        totalDoanhThu.Values.Add((double)item.TongGiaVeSuatChieu);
                        totalLoiNhuan.Values.Add((double)item.TongGiaVeTicket);
                        totalLabels.Add(item.NgayDatVe.Date.ToString().Split(" ")[0]);
                    }
                }
                else if (totalChartType == 1)
                {
                    var query = context.SuatChieus
                                .Where(x => x.MovieId == filmId)
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
                        totalDoanhThu.Values.Add((double)item.TongGiaVeSuatChieu);
                        totalLoiNhuan.Values.Add((double)item.TongGiaVeTicket);
                        totalLabels.Add(item.NgayDauTuan.Day.ToString() + "/" + item.NgayDauTuan.Month.ToString() + "/" + item.NgayDauTuan.Year.ToString() + "-" + item.NgayCuoiTuan.Day.ToString() + "/" + item.NgayCuoiTuan.Month.ToString() + "/" + item.NgayCuoiTuan.Year.ToString());
                    }
                }
                else if (totalChartType == 2)
                {
                    var query = from ticket in context.Tickets
                                join suatChieu in context.SuatChieus
                                on ticket.SuatChieuId equals suatChieu.SuatChieuId
                                where ticket.NgayDatVe.HasValue && ticket.MovieId == filmId
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
                        totalDoanhThu.Values.Add((double)item.TongGiaVeSuatChieu);
                        totalLoiNhuan.Values.Add((double)item.TongGiaVeTicket);
                        totalLabels.Add(item.Month.ToString() + "/" + item.Year.ToString());
                    }
                }
                else if (totalChartType == 3)
                {
                    var query = from ticket in context.Tickets
                                join suatChieu in context.SuatChieus
                                on ticket.SuatChieuId equals suatChieu.SuatChieuId
                                where ticket.NgayDatVe.HasValue && ticket.MovieId == filmId
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
                        totalDoanhThu.Values.Add((double)item.TongGiaVeSuatChieu);
                        totalLoiNhuan.Values.Add((double)item.TongGiaVeTicket);
                        totalLabels.Add(item.Year.ToString());

                    }
                }
            }
        }
        public void loadSHChartData()
        {
            if (selectedComboboxItem == null)
                return;
            shDoanhThu.Values.Clear();
            shLoiNhuan.Values.Clear();
            shLabels.Clear();
            using (var context = new NextflizContext())
            {
                if (shChartType == 0)
                {
                    var query = context.SuatChieus
                                    .Where(x => x.MovieId == filmId && x.SuatChieuId == selectedComboboxItem.id)
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
                        shDoanhThu.Values.Add((double)item.TongGiaVeSuatChieu);
                        shLoiNhuan.Values.Add((double)item.TongGiaVeTicket);
                        shLabels.Add(item.NgayDatVe.Date.ToString().Split(" ")[0]);
                    }
                }
                else if (shChartType == 1)
                {
                    var query = context.SuatChieus
                                .Where(x => x.MovieId == filmId && x.SuatChieuId == selectedComboboxItem.id)
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
                        shDoanhThu.Values.Add((double)item.TongGiaVeSuatChieu);
                        shLoiNhuan.Values.Add((double)item.TongGiaVeTicket);
                        shLabels.Add(item.NgayDauTuan.Day.ToString() + "/" + item.NgayDauTuan.Month.ToString() + "/" + item.NgayDauTuan.Year.ToString() + "-" + item.NgayCuoiTuan.Day.ToString() + "/" + item.NgayCuoiTuan.Month.ToString() + "/" + item.NgayCuoiTuan.Year.ToString());
                    }
                }
                else if (shChartType == 2)
                {
                    var query = from ticket in context.Tickets
                                join suatChieu in context.SuatChieus
                                on ticket.SuatChieuId equals suatChieu.SuatChieuId
                                where ticket.NgayDatVe.HasValue && ticket.MovieId == filmId && ticket.SuatChieuId == selectedComboboxItem.id
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
                        shDoanhThu.Values.Add((double)item.TongGiaVeSuatChieu);
                        shLoiNhuan.Values.Add((double)item.TongGiaVeTicket);
                        shLabels.Add(item.Month.ToString() + "/" + item.Year.ToString());
                    }
                }
                else if (shChartType == 3)
                {
                    var query = from ticket in context.Tickets
                                join suatChieu in context.SuatChieus
                                on ticket.SuatChieuId equals suatChieu.SuatChieuId
                                where ticket.NgayDatVe.HasValue && ticket.MovieId == filmId && ticket.SuatChieuId == selectedComboboxItem.id
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
                        shDoanhThu.Values.Add((double)item.TongGiaVeSuatChieu);
                        shLoiNhuan.Values.Add((double)item.TongGiaVeTicket);
                        shLabels.Add(item.Year.ToString());
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
