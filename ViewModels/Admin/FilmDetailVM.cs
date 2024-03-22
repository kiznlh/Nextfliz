using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nextfliz
{
    class FilmDetailVM : INotifyPropertyChanged
    {
        private const string noImage = "https://hosting.ca/wp-content/uploads/2017/09/broken-image.png";
        private string filmId;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Actor> actorList { get; set; } = new ObservableCollection<Actor>();
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
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public FilmDetailVM(string filmId)
        {
            this.filmId = filmId;
            updateContent();

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
            this.filmId = filmId;
        }
        private void updateContent()
        {
            using (var dbContext = new NextflizContext())
            {
                var item = dbContext.Movies.FirstOrDefault(a => a.MovieId == filmId);
                image = item.HinhAnh;
                name = item.TenPhim;
                rating = item.DiemDanhGia.ToString();
                year = item.NamPhatHanh.ToString();
                time = item.ThoiLuong.ToString();
                certification = item.Certification;
                var itemGenre = dbContext.Genres.FirstOrDefault(a => a.GenreId == item.GenreId);
                genre = itemGenre.TenTheLoai;
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

    }
}
