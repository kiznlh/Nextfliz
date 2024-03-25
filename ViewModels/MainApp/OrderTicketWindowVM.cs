using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Nextfliz.Views.MainApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Nextfliz.ViewModels.MainApp
{
    public class OrderTicketWindowVM : INotifyPropertyChanged
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

        public ObservableCollection<SuatChieu> SuatChieu { get; set; }

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
            get {
               
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
            get {
                
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
            get {
               
                return _finalPrice; 
            } 
            set
            {
                _finalPrice = value;
                
                OnPropertyChanged(nameof(FinalPrice));
            }
        }
   
        public ObservableCollection<VoucherCard> Vouchers { get; set; }

        public RelayCommand ConfirmCommand { get; set; }
        public event EventHandler OnRequestClose;
        private string _movieID;
        public OrderTicketWindowVM(string movieID)
        {
            BookedSeatList = new ObservableCollection<string>();
            _movieID = movieID;
            Vouchers = new ObservableCollection<VoucherCard>();
            SuatChieu = new ObservableCollection<SuatChieu>();

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

               
                //SuatChieu

                var suatChieuList = context.SuatChieus.Where(s => s.MovieId == selectedMovie.MovieId).ToList();

                foreach (var suatChieu in suatChieuList)
                {
                    SuatChieu.Add(suatChieu);
                }

                SelectedSuatChieuIndex = 0;
                
               
            }
            getVouchers();

            ConfirmCommand = new RelayCommand(confirm, canConfirm);
           
        }

        private void close()
        {
            OnRequestClose(this, new EventArgs());
            if (Application.Current.MainWindow is WindowUserMainWindow mainWindow) 
            {
                mainWindow.navigateToHome();
            }
        }
        public bool canConfirm(object value)
        {
            return true;
        }
        public void confirm(object value)
        {
            if (string.IsNullOrEmpty(Seat))
            {
                MessageBox.Show("Hãy chọn 1 ghế trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (var context = new NextflizContext())
            {
                bool checkSinhNhat = false;
                var newTicket = new Ticket()
                {
                    MovieId = _movieID,
                    Username = UserSession.username,
                    NgayDatVe = DateTime.Now,
                    SuatChieuId = SuatChieu[SelectedSuatChieuIndex].SuatChieuId,
                    GiaVe = (decimal?)FinalPrice ?? 0,
                    VoucherSinhNhat = checkSinhNhat,
                    ViTriGhe = Seat,
                    TicketId = UserSession.username + "_" + _movieID + "_" + Seat,
                };
                context.Tickets.Add(newTicket);
                foreach (var card in Vouchers)
                {
                    if (card.VoucherChecked)
                    {
                        if (card.VoucherID == "sinhnhat")
                        {
                            checkSinhNhat = true;
                        }
                        else 
                        {
                            var usedVoucher = context.Vouchers.Where(voucher => voucher.VoucherId == card.VoucherID).FirstOrDefault();
                            int numberLeft = (int)(usedVoucher.SoLuong - 1);
                            usedVoucher.SoLuong = numberLeft;
                            

                            var newVoucerUsage = new VoucherUsage()
                            {
                                TicketId = newTicket.TicketId,
                                VoucherId = usedVoucher.VoucherId,
                                
                            };
                            context.VoucherUsages.Add(newVoucerUsage);

                        }
                    }

                }
                
                
                context.SaveChanges();
            }
            MessageBox.Show("Đặt vé thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

            close();
        }
       
        public void getVouchers()
        {
            Vouchers.Clear();
            var currentSuatChieu = SuatChieu[SelectedSuatChieuIndex];

            using (var context = new NextflizContext())
            {
                var voucherList = context.Vouchers.ToList();
            
                var findUser = context.Users.Where(user => user.Username == UserSession.username).FirstOrDefault();
                if (findUser != null)
                {
                    DateOnly userBirthday = findUser.NgaySinh ?? DateOnly.FromDateTime(DateTime.Now);
                    if (userBirthday.Month == DateTime.Now.Month)
                    {
                        VoucherCard voucherCard = new VoucherCard()
                        {
                            VoucherImage = "../../../Resources/Icons/voucher_birthday.png",
                            VoucherName = "Voucher Sinh Nhật",
                            VoucherValue = 20,
                            VoucherChecked = false,
                            VoucherID = "sinhnhat",
                        };
                        Vouchers.Add(voucherCard);
                    }
                    
                }
                foreach (var voucher in voucherList)
                {
                    if (voucher.SoLuong > 0)
                    {
                        VoucherCard voucherCard = new VoucherCard()
                        {
                            VoucherImage = "../../../Resources/Icons/voucher_normal.png",
                            VoucherName = voucher.TenVoucher ?? "",
                            VoucherValue = voucher.TiLeGiam ?? 0,
                            VoucherChecked = false,
                            VoucherID = voucher.VoucherId,
                        };
                        Vouchers.Add(voucherCard);
                    }
                    
                }
            }
        }
        public void getBookedSeats()
        {
            BookedSeatList.Clear();
            var currentSuatChieu = SuatChieu[SelectedSuatChieuIndex];

            using (var context = new NextflizContext())
            {
                var ticketList = context.Tickets.Where(ticket => ticket.SuatChieuId == currentSuatChieu.SuatChieuId).ToList();

                foreach (var ticket in ticketList)
                {
                    BookedSeatList.Add(ticket.ViTriGhe);
                }
            }

        }
        public void setOriginalPrice()
        {
            OriginalPrice = (double)(SuatChieu[SelectedSuatChieuIndex].GiaVe ?? 0);
        }
    }
}
