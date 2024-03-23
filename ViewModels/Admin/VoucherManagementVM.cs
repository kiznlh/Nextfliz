using Nextfliz.Models;
using Nextfliz.View.Admin;
using Nextfliz.Views.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nextfliz
{
    internal class VoucherManagementVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Voucher> showingList { get; set; } = new ObservableCollection<Voucher>();
        public RelayCommand addItemCommand { get; set; }
        public RelayCommand toNextPage { get; set; }
        public RelayCommand toPreviousPage { get; set; }
        public RelayCommand showAddPanel { get; set; }
        public RelayCommand deleteItemCommand { get; set; }
        public RelayCommand editItemCommand { get; set; }
        private int listSize;
        public int listType { get; set; }

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
        public VoucherManagementVM() {
            toNextPage = new RelayCommand(nextPage, canPerform);
            toPreviousPage = new RelayCommand(previousPage, canPerform);
            showAddPanel = new RelayCommand(showAdd, canPerform);
            deleteItemCommand = new RelayCommand(deleteItem, canPerform);
            editItemCommand = new RelayCommand(editItem, canPerform);
            updateList();
        }

        private void updateList()
        {
            showingList.Clear();
            using (var context = new NextflizContext())
            {
                var items = context.Vouchers.Take(numPerPage).ToList();
                listSize = context.Vouchers.Count();
                foreach (var item in items)
                {
                    showingList.Add(item);
                }
                totalPage = "/ " + Math.Ceiling(context.Vouchers.Count() * 1.0 / numPerPage);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPage"));

                currentPage = listSize != 0 ? 1 : 0;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
            }

            currentPage = listSize != 0 ? 1 : 0;
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
                var items = context.Vouchers.Skip(numPerPage * (currentPage - 1)).Take(numPerPage).ToList();
                foreach (var item in items)
                {
                    showingList.Add(item);
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
                var items = context.Vouchers.Skip(numPerPage * (currentPage - 1)).Take(numPerPage).ToList();
                foreach (var item in items)
                {
                    showingList.Add(item);
                }
            }
        }

        private void deleteItem(object obj)
        {
            if (obj is Voucher itemToDelete)
            {
                using (var dbContext = new NextflizContext())
                {
                    dbContext.Vouchers.Remove(itemToDelete);
                    dbContext.SaveChanges();
                }
            }
            updateList();
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
