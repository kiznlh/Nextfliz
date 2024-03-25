using Nextfliz.View.Admin;
using Nextfliz.Views.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Nextfliz
{
    public class TicketItem
    {
        public string id { get; set; }
        public string HoTen { get; set; }
        public string TenPhim { get; set; }
        public string NgayDatVe { get; set; }
        public string NgayChieu { get; set; }
        public string GioChieu { get; set; }
        public TicketItem(string id, string Hoten, string TenPhim, string NgayDatVe, string NgayChieu, string GioChieu)
        {
            this.id = id;
            this.HoTen = Hoten;
            this.TenPhim = TenPhim;
            this.NgayDatVe = NgayDatVe;
            this.NgayChieu = NgayChieu;
            this.GioChieu = GioChieu;
        }
    }
    internal class VoucherManagementVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Voucher> showingList { get; set; } = new ObservableCollection<Voucher>();
        public ObservableCollection<TicketItem> showingTicketList { get; set; } = new ObservableCollection<TicketItem>();
        public RelayCommand addItemCommand { get; set; }
        public RelayCommand toNextPage { get; set; }
        public RelayCommand toPreviousPage { get; set; }
        public RelayCommand showAddPanel { get; set; }
        public RelayCommand deleteItemCommand { get; set; }
        public RelayCommand editItemCommand { get; set; }
        public RelayCommand toNextTicketPage { get; set; }
        public RelayCommand toPreviousTicketPage { get; set; }
        public RelayCommand deleteTicketCommand { get; set; }
        private int listSize;
        private int currentPage { get; set; }
        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                if (currentPage != value)
                {
                    currentPage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
                }
            }
        }
        private string totalPage { get; set; }
        public string TotalPage
        {
            get { return totalPage; }
            set
            {
                if (totalPage != value)
                {
                    totalPage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPage"));
                }
            }
        }

        public const int numPerPage = 10;
        private int ticketListSize = 0;
        private int currentTicketPage { get; set; }
        public int CurrentTicketPage
        {
            get { return currentTicketPage; }
            set
            {
                if (currentTicketPage != value)
                {
                    currentTicketPage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentTicketPage"));
                }
            }
        }
        private string totalTicketPage { get; set; }
        public string TotalTicketPage
        {
            get { return totalTicketPage; }
            set
            {
                if (totalTicketPage != value)
                {
                    totalTicketPage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalTicketPage"));
                }
            }
        }

        public const int numPerTicketPage = 1;
        public VoucherManagementVM() {
            toNextPage = new RelayCommand(nextPage, canPerform);
            toPreviousPage = new RelayCommand(previousPage, canPerform);

            toNextTicketPage = new RelayCommand(nextTicketPage, canPerform);
            toPreviousTicketPage = new RelayCommand(previousTicketPage, canPerform);
            deleteTicketCommand = new RelayCommand(deleteTicket, canPerform);

            showAddPanel = new RelayCommand(showAdd, canPerform);
            deleteItemCommand = new RelayCommand(deleteItem, canPerform);
            editItemCommand = new RelayCommand(editItem, canPerform);
            updateList();
            updateTicketList();
        }

        private void updateList()
        {
            showingList.Clear();
            using (var context = new NextflizContext())
            {
                var items = context.Vouchers.Where(v => v.SoLuong > 0).Take(numPerPage).ToList();
                listSize = context.Vouchers.Where(v => v.SoLuong > 0).Count();
                foreach (var item in items)
                {
                    showingList.Add(item);
                }
                totalPage = "/ " + Math.Ceiling(context.Vouchers.Where(v => v.SoLuong > 0).Count() * 1.0 / numPerPage);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPage"));

                currentPage = listSize != 0 ? 1 : 0;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
            }

            currentPage = listSize != 0 ? 1 : 0;
        }

        private void updateTicketList()
        {
            showingTicketList.Clear();
            using (var context = new NextflizContext())
            {
                var items = (from ticket in context.Tickets
                            where ticket.NgayDatVe != null 
                            join suatChieu in context.SuatChieus
                                on ticket.SuatChieuId equals suatChieu.SuatChieuId
                            join movie in context.Movies
                                on suatChieu.MovieId equals movie.MovieId 
                            join user in context.Users
                                on ticket.Username equals user.Username
                            orderby ticket.NgayDatVe descending
                            select new
                            {
                                TicketId = ticket.TicketId,
                                TenPhim = movie.TenPhim,
                                NgayDatVe = ticket.NgayDatVe,
                                NgayGioChieu = suatChieu.NgayGioChieu,
                                HoTen = user.HoTen 
                            }).Take(numPerTicketPage).ToList();
                ticketListSize = context.Tickets.Count();
                foreach (var item in items)
                {
                    showingTicketList.Add(new TicketItem(item.TicketId, 
                                                         item.HoTen, 
                                                         item.TenPhim, 
                                                         item.NgayDatVe.Value.Day.ToString() + "/" + item.NgayDatVe.Value.Month.ToString() + "/" + item.NgayDatVe.Value.Year.ToString(), 
                                                         item.NgayGioChieu.Value.Day.ToString() + "/" + item.NgayGioChieu.Value.Month.ToString() + "/" + item.NgayGioChieu.Value.Year.ToString(), 
                                                         item.NgayGioChieu.Value.Hour.ToString() + ":" + item.NgayGioChieu.Value.Minute.ToString()));
                }
                totalTicketPage = "/ " + Math.Ceiling(ticketListSize * 1.0 / numPerTicketPage);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalTicketPage")); 

                currentTicketPage = ticketListSize != 0 ? 1 : 0;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentTicketPage"));
            }

            currentTicketPage = ticketListSize != 0 ? 1 : 0;
        }

        private void nextPage(object value)
        {
            if (currentPage == Math.Ceiling(listSize * 1.0 / numPerPage))
                return;

            showingList.Clear();
            currentPage++;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
            using (var context = new NextflizContext())
            {
                var items = context.Vouchers.Where(v => v.SoLuong > 0).Skip(numPerPage * (currentPage - 1)).Take(numPerPage).ToList();
                foreach (var item in items)
                {
                    showingList.Add(item);
                }
            }
        }

        private void nextTicketPage(object value)
        {
            if (currentTicketPage == Math.Ceiling(ticketListSize * 1.0 / numPerTicketPage))
                return;

            showingTicketList.Clear();
            currentTicketPage++;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentTicketPage"));
            using (var context = new NextflizContext())
            {
                var items = (from ticket in context.Tickets
                             where ticket.NgayDatVe != null
                             join suatChieu in context.SuatChieus
                                 on ticket.SuatChieuId equals suatChieu.SuatChieuId
                             join movie in context.Movies
                                 on suatChieu.MovieId equals movie.MovieId
                             join user in context.Users
                                 on ticket.Username equals user.Username
                             orderby ticket.NgayDatVe descending
                             select new
                             {
                                 TicketId = ticket.TicketId,
                                 TenPhim = movie.TenPhim,
                                 NgayDatVe = ticket.NgayDatVe,
                                 NgayGioChieu = suatChieu.NgayGioChieu,
                                 HoTen = user.HoTen
                             }).Skip(numPerTicketPage * (currentTicketPage - 1)).Take(numPerTicketPage).ToList();
                foreach (var item in items)
                {
                    showingTicketList.Add(new TicketItem(item.TicketId,
                                                         item.HoTen,
                                                         item.TenPhim,
                                                         item.NgayDatVe.Value.Day.ToString() + "/" + item.NgayDatVe.Value.Month.ToString() + "/" + item.NgayDatVe.Value.Year.ToString(),
                                                         item.NgayGioChieu.Value.Day.ToString() + "/" + item.NgayGioChieu.Value.Month.ToString() + "/" + item.NgayGioChieu.Value.Year.ToString(),
                                                         item.NgayGioChieu.Value.Hour.ToString() + ":" + item.NgayGioChieu.Value.Minute.ToString()));
                }
            }
        }

        private void previousPage(object value)
        {
            if (currentPage <= 1)
                return;

            showingList.Clear();
            currentPage--;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
            using (var context = new NextflizContext())
            {
                var items = context.Vouchers.Where(v => v.SoLuong > 0).Skip(numPerPage * (currentPage - 1)).Take(numPerPage).ToList();
                foreach (var item in items)
                {
                    showingList.Add(item);
                }
            }
        }

        private void previousTicketPage(object value)
        {
            if (currentTicketPage <= 1)
                return;

            showingTicketList.Clear();
            currentTicketPage--;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentTicketPage"));
            using (var context = new NextflizContext())
            {
                var items = (from ticket in context.Tickets
                             where ticket.NgayDatVe != null
                             join suatChieu in context.SuatChieus
                                 on ticket.SuatChieuId equals suatChieu.SuatChieuId
                             join movie in context.Movies
                                 on suatChieu.MovieId equals movie.MovieId
                             join user in context.Users
                                 on ticket.Username equals user.Username
                             orderby ticket.NgayDatVe descending
                             select new
                             {
                                 TicketId = ticket.TicketId,
                                 TenPhim = movie.TenPhim,
                                 NgayDatVe = ticket.NgayDatVe,
                                 NgayGioChieu = suatChieu.NgayGioChieu,
                                 HoTen = user.HoTen
                             }).Skip(numPerTicketPage * (currentTicketPage - 1)).Take(numPerTicketPage).ToList();
                foreach (var item in items)
                {
                    showingTicketList.Add(new TicketItem(item.TicketId, 
                                                         item.HoTen, 
                                                         item.TenPhim, 
                                                         item.NgayDatVe.Value.Day.ToString() + "/" + item.NgayDatVe.Value.Month.ToString() + "/" + item.NgayDatVe.Value.Year.ToString(), 
                                                         item.NgayGioChieu.Value.Day.ToString() + "/" + item.NgayGioChieu.Value.Month.ToString() + "/" + item.NgayGioChieu.Value.Year.ToString(), 
                                                         item.NgayGioChieu.Value.Hour.ToString() + ":" + item.NgayGioChieu.Value.Minute.ToString()));
                }
            }
        }

        private void deleteItem(object obj)
        {
            if (obj is Voucher itemToDelete)
            {
                using (var dbContext = new NextflizContext())
                {
                    var item = dbContext.Vouchers.FirstOrDefault(x => x.VoucherId == itemToDelete.VoucherId);
                    item.SoLuong = 0;
                    dbContext.SaveChanges();
                }
            }
            updateList();
        }

        private void deleteTicket(object obj)
        {
            if (obj is TicketItem itemToDelete)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa vé này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (var dbContext = new NextflizContext())
                    {
                        var ticketToDelete = dbContext.Tickets.FirstOrDefault(t => t.TicketId == itemToDelete.id);
                        var usage = dbContext.VoucherUsages.Where(s => s.TicketId == ticketToDelete.TicketId);
                        dbContext.RemoveRange(usage);
                        dbContext.SaveChanges();
                        dbContext.Tickets.Remove(ticketToDelete);
                        dbContext.SaveChanges();
                    }
                }
            }
            updateTicketList();
        }

        private void showAdd(object value)
        {
            VoucherPopup addPopup = new VoucherPopup(null);
            addPopup.ShowDialog();
            updateList();
        }

        private void editItem(object obj)
        {
            if (obj is Voucher itemToEdit)
            {
                VoucherPopup addPopup = new VoucherPopup(itemToEdit.VoucherId);
                addPopup.ShowDialog();
            }
            updateList();
        }

        private bool canPerform(object value)
        {
            return true;
        }
    }
}
