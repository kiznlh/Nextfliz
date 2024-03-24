using Nextfliz.ViewModels.MainApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Nextfliz.Views.MainApp
{
    /// <summary>
    /// Interaction logic for OrderTicketWindow.xaml
    /// </summary>
    /// 

    public partial class OrderTicketWindow : Window
    {
        public OrderTicketWindowVM viewModel;
        public OrderTicketWindow(string movieID)
        {
            InitializeComponent();
            viewModel = new OrderTicketWindowVM(movieID);
            DataContext = viewModel;

            foreach (var item in viewModel.Vouchers)
            {
                if (item is VoucherCard voucherCard)
                {
                    voucherCard.PropertyChanged += VoucherCard_PropertyChanged;
                }
            }
        }
        private void VoucherCard_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VoucherChecked")
            {
                var card = sender as VoucherCard;
                if (card.VoucherChecked)
                {
                    viewModel.VoucherTotalValue += card.VoucherValue;
                }
            }
        }
        void setSeats()
        {
            seatLayout.reset();
            foreach (var seat in viewModel.BookedSeatList)
            {
                seatLayout.SetSeatOccupied(seat);
            }
        }

        private void timeStampComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setSeats();
        }
    }

    
}
