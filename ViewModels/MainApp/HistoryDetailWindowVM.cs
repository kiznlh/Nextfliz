using Nextfliz.Views.MainApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Nextfliz.ViewModels.MainApp
{
    public class HistoryDetailWindowVM: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _movieBG;
        public string MovieBG
        {
            get { return _movieBG; }
            set
            {
                _movieBG = value;
                OnPropertyChanged(nameof(MovieBG));
            }
        }

        private string _name;
        private string _genre;
        private string _duration;
        private int _releaseDate;
        private double _rating;
        private string _certification;

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

        private string _suatChieu;
        public string SuatChieu
        {
            get { return _suatChieu; }
            set
            {
                _suatChieu = value;
                OnPropertyChanged(nameof(_suatChieu));
            }
        }

        private string _seat;
        public string Seat
        {
            get { return _seat; }
            set
            {
                _seat = value;

                OnPropertyChanged(nameof(Seat));
            }
        }

        private double _originalPrice;
        public double OriginalPrice
        {
            get
            {

                return _originalPrice;
            }
            set
            {

                _originalPrice = value;

                OnPropertyChanged(nameof(OriginalPrice));
            }
        }

        private double _voucherValue;
        public double VoucherTotalValue
        {
            get
            {

                return _voucherValue;
            }
            set
            {
                _voucherValue = value;


                OnPropertyChanged(nameof(VoucherTotalValue));
            }
        }
        public double voucherTotalPercent { set; get; }

        private double _finalPrice;
        public double FinalPrice
        {
            get
            {

                return _finalPrice;
            }
            set
            {
                _finalPrice = value;

                OnPropertyChanged(nameof(FinalPrice));
            }
        }

        private string _ticketID;

        public HistoryDetailWindowVM(string ticketID)
        {
           
            _ticketID = ticketID;


            using (var context = new NextflizContext())
            {
                var selectedTicket = context.Tickets.Where(ticket => ticket.TicketId == _ticketID).FirstOrDefault();
                var selectedMovie = context.Movies.Where(movie => movie.MovieId == selectedTicket.MovieId).FirstOrDefault();

                MovieBG = selectedMovie.HinhAnh;
                Name = selectedMovie.TenPhim;

                var genreName = context.Genres.Where(g => g.GenreId == selectedMovie.GenreId).FirstOrDefault();
                Genre = genreName.TenTheLoai;

                Duration = selectedMovie.ThoiLuong.ToString() ?? "0";

                ReleaseDate = selectedMovie.NamPhatHanh ?? 0;

                Rating = selectedMovie.DiemDanhGia ?? 0;

                Certification = selectedMovie.Certification;

                var suatChieuValue = context.SuatChieus.Where(suatchieu => suatchieu.SuatChieuId == selectedTicket.SuatChieuId).FirstOrDefault();
                SuatChieu = String.Format("{0:G}",(DateTime)suatChieuValue.NgayGioChieu);

                Seat = selectedTicket.ViTriGhe;

                OriginalPrice = (double)suatChieuValue.GiaVe;

                FinalPrice = (double)selectedTicket.GiaVe;

                VoucherTotalValue = (FinalPrice - OriginalPrice);
            }
          

        }


    }
}
