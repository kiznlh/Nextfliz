using Nextfliz.Views.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nextfliz
{
    internal class VoucherPopupVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private VoucherPopup dialog;
        private string voucherId;
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
        private string rate { get; set; }
        public string Rate
        {
            get { return rate; }
            set
            {
                if (rate != value)
                {
                    rate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rate"));
                }
            }
        }
        private string amount { get; set; }
        public string Amount
        {
            get { return amount; }
            set
            {
                if (amount != value)
                {
                    amount = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Amount"));
                }
            }
        }
        public RelayCommand saveButtonCommand { get; set; }

        public VoucherPopupVM(VoucherPopup dialog, string voucherId)
        {
            this.dialog = dialog;
            this.voucherId = voucherId;
            name = "";
            rate = "";
            amount = "";
            saveButtonCommand = new RelayCommand(saveButtonClicked, canPerform);

            if (voucherId != null)
            {
                using (var dbContext = new NextflizContext())
                {
                    var item = dbContext.Vouchers.FirstOrDefault(a => a.VoucherId == voucherId);
                    name = item.TenVoucher;
                    rate = item.TiLeGiam.ToString().Replace(",", ".");
                    amount = item.SoLuong.ToString();
                }
            }
        }

        private void saveButtonClicked(object value)
        {
            if (name.Length == 0 || rate.Length == 0 || amount.Length == 0)
                return;

            if (voucherId != null)
                using (var dbContext = new NextflizContext())
                {
                    var itemToUpdate = dbContext.Vouchers.FirstOrDefault(a => a.VoucherId == voucherId);

                    if (itemToUpdate != null)
                    {
                        itemToUpdate.TenVoucher = name;
                        itemToUpdate.TiLeGiam = double.Parse(rate);
                        itemToUpdate.SoLuong = int.Parse(amount);
                        dbContext.SaveChanges();
                    }
                }
            else
                using (var context = new NextflizContext())
                {
                    var newVoucher = new Voucher();

                    newVoucher.TenVoucher = name;
                    newVoucher.TiLeGiam = double.Parse(rate);
                    newVoucher.SoLuong = int.Parse(amount);
                    context.Vouchers.Add(newVoucher);

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
