using Nextfliz.Views.MainApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Nextfliz.ViewModels.MainApp
{
    public class FilmDetailPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _movieID;
        private string _movieBG;
        private string _name;
        private string _genre;
        private string _duration;
        private int _releaseDate;
        private double _rating;
        private string _certification;
        private TextBlock _selectedSuatChieu;
        private ObservableCollection<string> _actors;
        private ObservableCollection<string> _directors;
        private ObservableCollection<string> _suatChieu;

        public string MovieBG
        {
            get { return _movieBG; }
            set
            {
                _movieBG = value;
                OnPropertyChanged(nameof(MovieBG));
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Genre
        {
            get { return _genre; }
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }

        public string Duration
        {
            get { return _duration; }
            set
            {
                _duration = value + " phút";
                OnPropertyChanged(nameof(Duration));
            }
        }

        public int ReleaseDate
        {
            get { return _releaseDate; }
            set
            {
                _releaseDate = value;
                OnPropertyChanged(nameof(ReleaseDate));
            }
        }

        public double Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }

        public string Certification
        {
            get { return _certification; }
            set
            {
                _certification = value;
                OnPropertyChanged(nameof(Certification));
            }
        }
  
        private int _selectedSuatChieuIndex;
        public int SelectedSuatChieuIndex
        {
            get { return _selectedSuatChieuIndex; }
            set
            {
                _selectedSuatChieuIndex = value;
                getBookedSeats();
                OnPropertyChanged(nameof(SelectedSuatChieuIndex));

            }
        }

        private ObservableCollection<string> _bookedSeatList;
        public ObservableCollection<string> BookedSeatList
        {
            get { return _bookedSeatList; }
            set
            {
                _bookedSeatList = value;
                OnPropertyChanged(nameof(BookedSeatList));
            }
        }

        public ObservableCollection<ActorAndDirectorDetailCard> Actors { get; set; }


        public ActorAndDirectorDetailCard Directors { get; set; }


        public ObservableCollection<SuatChieu> SuatChieu { get; set; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand OrderCommand { get; set; }

        public FilmDetailPageVM(string movieID)
        {
            _movieID = movieID;
            BookedSeatList = new ObservableCollection<string>();
            Actors = new ObservableCollection<ActorAndDirectorDetailCard>();
            
            SuatChieu = new ObservableCollection<SuatChieu>();
            OrderCommand = new RelayCommand(order,canOrder);
            using (var context = new NextflizContext())
            {
                var selectedMovie = context.Movies.Where(m => m.MovieId == movieID).FirstOrDefault();
                MovieBG = selectedMovie.HinhAnh;
                Name = selectedMovie.TenPhim;
                
                var genreName = context.Genres.Where(g => g.GenreId == selectedMovie.GenreId).FirstOrDefault();
                Genre = genreName.TenTheLoai;

                Duration = selectedMovie.ThoiLuong.ToString() ?? "0";

                ReleaseDate = selectedMovie.NamPhatHanh ?? 0;

                Rating = selectedMovie.DiemDanhGia ?? 0;

                Certification = selectedMovie.Certification;

                //Actor
                var castingList = context.FilmCasts.Where(fc => fc.MovieId == selectedMovie.MovieId).ToList();

                var actorlist = new List<Actor>();
                foreach (var casting in castingList)
                {
                    actorlist.Add(context.Actors.Where(a => a.ActorId == casting.ActorId).FirstOrDefault());
                }
                
                foreach (var actor in actorlist)
                {
                    var actorCard = new ActorAndDirectorDetailCard()
                    {
                        ImageSource = actor.HinhAnh,
                        HoTen = actor.HoTen,
                        TieuSu = actor.TieuSu,
                    };

                    Actors.Add(actorCard);
                }

                //Director

                var director = context.Directors.Where(d => d.DirectorId == selectedMovie.DirectorId).FirstOrDefault();

                Directors = new ActorAndDirectorDetailCard()
                {
                    ImageSource = director.HinhAnh,
                    HoTen = director.HoTen,
                    TieuSu = director.TieuSu,
                };

                //SuatChieu

                var suatChieuList = context.SuatChieus.Where(s => s.MovieId == selectedMovie.MovieId).ToList();
                
                foreach (var suatChieu in suatChieuList)
                {
                    SuatChieu.Add(suatChieu);
                }

                SelectedSuatChieuIndex = 0;

             
            }
        }
        public bool canOrder(object value)
        {
            return true;
        }
        public void order(object value)
        {
            if (UserSession.IsLoggedIn)
            {
                OrderTicketWindow orderWindow = new OrderTicketWindow(_movieID);
                orderWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng đăng nhập để đặt vé.","Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                if (Application.Current.MainWindow is WindowUserMainWindow mainWindow)
                {
                    mainWindow.goToLogin();
                }
                return;
            }
        }
        public void getBookedSeats()
        {
            BookedSeatList.Clear();
            var currentSuatChieu = SuatChieu[SelectedSuatChieuIndex];
            if (currentSuatChieu != null)
            {
                using (var context = new NextflizContext())
                {
                    var ticketList = context.Tickets.Where(ticket => ticket.SuatChieuId == currentSuatChieu.SuatChieuId).ToList();

                    foreach (var ticket in ticketList)
                    {
                        BookedSeatList.Add(ticket.ViTriGhe);
                    }
                }
            }
            

        }
    }
}
