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

        public ObservableCollection<VoucherCard> Vouchers { get; set; }

     
        public event EventHandler OnRequestClose;
        private string _ticketID;
        public HistoryDetailWindowVM(string ticketID)
        {
            BookedSeatList = new ObservableCollection<string>();
            _ticketID = ticketID;
            Vouchers = new ObservableCollection<VoucherCard>();
            SuatChieu = new ObservableCollection<SuatChieu>();


            getVouchers();

        }

        private void close()
        {
            OnRequestClose(this, new EventArgs());
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
