using Nextfliz.Views.MainApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nextfliz.ViewModels.MainApp
{
    public class WindowMainPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<FilmCardControl> hotMovies { get; set; }

        public ObservableCollection<FilmCardControl> randomMovies { get; set; }
        public WindowMainPageVM()
        {
            hotMovies = new ObservableCollection<FilmCardControl>();
            randomMovies = new ObservableCollection<FilmCardControl>();

            getHotMovies();
            getRandomMovies();
        }

        void getHotMovies()
        {
            hotMovies.Clear();
            var topFilmMax = 5;
            using (var context = new NextflizContext())
            {
                var topMovies = (from movie in context.Movies
                                 join ticket in context.Tickets on movie.MovieId equals ticket.MovieId
                                 group ticket by movie.MovieId into g
                                 orderby g.Count() descending
                                 select new
                                 {
                                     MovieId = g.Key,
                                     Sell = g.Count()
                                 }).Take(topFilmMax).ToList();

                foreach (var movie in topMovies)
                {
                    var saidMovie = context.Movies.Where(m => m.MovieId == movie.MovieId).FirstOrDefault();
                    var filmCard = new FilmCardControl()
                    {
                        MovieID = saidMovie.MovieId,
                        ImageBG = saidMovie.HinhAnh,
                        TenPhim = saidMovie.TenPhim,
                        Certification = saidMovie.Certification,
                        ThoiLuong = saidMovie.ThoiLuong != null ? saidMovie.ThoiLuong.ToString() : "0",
                        NamPhatHanh = saidMovie.NamPhatHanh.ToString(),
                        Rating = saidMovie.DiemDanhGia.ToString(),
                    };

                    hotMovies.Add(filmCard);

                }
            }
        }
        void getRandomMovies()
        {
            randomMovies.Clear();
            var randomFilm = 10;

            using (var context = new NextflizContext())
            {
                var movieIdsOnSale = context.SuatChieus
                         .Where(s => s.MovieId != null)
                        .Select(s => s.MovieId)
                        .Distinct()
                        .OrderBy(m => Guid.NewGuid()) 
                        .Take(randomFilm)
                        .ToList();

                foreach (var movieId in movieIdsOnSale)
                {
                    var saidMovie = context.Movies.FirstOrDefault(m => m.MovieId == movieId);
                    if (saidMovie != null)
                    {
                        var filmCard = new FilmCardControl()
                        {
                            MovieID = saidMovie.MovieId,
                            ImageBG = saidMovie.HinhAnh,
                            TenPhim = saidMovie.TenPhim,
                            Certification = saidMovie.Certification,
                            ThoiLuong = saidMovie.ThoiLuong != null ? saidMovie.ThoiLuong.ToString() : "0",
                            NamPhatHanh = saidMovie.NamPhatHanh.ToString(),
                            Rating = saidMovie.DiemDanhGia.ToString(),
                        };

                        randomMovies.Add(filmCard);
                    }
                }
            }
        }
    }
}

