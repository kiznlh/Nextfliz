using Nextfliz.Views.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Nextfliz
{
    internal class SuatChieuVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string date { get; set; }
        public string Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
                }
            }
        }
        private string hour { get; set; }
        public string Hour
        {
            get { return hour; }
            set
            {
                if (hour != value)
                {
                    hour = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("hour"));
                }
            }
        }
        private string minute { get; set; }
        public string Minute
        {
            get { return minute; }
            set
            {
                if (minute != value)
                {
                    minute = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Minute"));
                }
            }
        }
        private string price { get; set; }
        public string Price
        {
            get { return price; }
            set
            {
                if (price != value)
                {
                    price = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
                }
            }
        }
        private string filmId;
        private string suatChieuId;
        private AddSuatChieu dialog;
        public RelayCommand saveButtonCommand { get; set; }

        public SuatChieuVM(AddSuatChieu dialog, string suatChieuId, string filmId) {
            date = "";
            hour = "";
            minute = "";
            price = "";
            this.filmId = filmId;
            this.suatChieuId = suatChieuId;
            this.dialog = dialog;
            saveButtonCommand = new RelayCommand(saveButtonClicked, canPerform);

            if (suatChieuId != null)
            {
                using (var dbContext = new NextflizContext())
                {
                    var item = dbContext.SuatChieus.FirstOrDefault(a => a.SuatChieuId == suatChieuId);
                    date = item.NgayGioChieu.GetValueOrDefault().Day + "-" + item.NgayGioChieu.GetValueOrDefault().Month + "-" + item.NgayGioChieu.GetValueOrDefault().Year;
                    hour = item.NgayGioChieu.GetValueOrDefault().Hour.ToString();
                    minute = item.NgayGioChieu.GetValueOrDefault().Minute.ToString();
                    price = item.GiaVe.ToString().Replace(",", ".");
                }
            }
        }

        private void saveButtonClicked(object value)
        {
            if (date.Length == 0 || hour.Length == 0 || minute.Length == 0 || price.Length == 0)
                return;
            if (date.Contains("/"))
            {
                return;
            }

            if (suatChieuId != null)
                using (var dbContext = new NextflizContext())
                {
                    var itemToUpdate = dbContext.SuatChieus.FirstOrDefault(a => a.SuatChieuId == suatChieuId);

                    if (itemToUpdate != null)
                    {
                        int day = int.Parse(date.Split("-")[0]);
                        int month = int.Parse(date.Split("-")[1]);
                        int year = int.Parse(date.Split("-")[2]);
                        DateTime newData = new DateTime(year, month, day, int.Parse(hour), int.Parse(minute), 0);
                        itemToUpdate.NgayGioChieu = newData;
                        itemToUpdate.GiaVe = decimal.Parse(price);
                        dbContext.SaveChanges();
                    }
                }
            else
                using (var context = new NextflizContext())
                {
                    var newSuatChieu = new SuatChieu();

                    newSuatChieu.MovieId = filmId;
                    int day = int.Parse(date.Split("-")[0]);
                    int month = int.Parse(date.Split("-")[1]);
                    int year = int.Parse(date.Split("-")[2]);
                    DateTime newData = new DateTime(year, month, day, int.Parse(hour), int.Parse(minute), 0);
                    newSuatChieu.NgayGioChieu = newData;
                    newSuatChieu.GiaVe = decimal.Parse(price);
                    context.SuatChieus.Add(newSuatChieu);

                    context.SaveChanges();
                }

            dialog.Close();
        }

        private bool canPerform(object value)
        {
            return true;
        }
    }
}
