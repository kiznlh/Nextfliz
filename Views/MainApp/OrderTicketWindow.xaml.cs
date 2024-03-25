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
using System.Xml.Linq;

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

            seatLayout.SeatClicked += SeatsLayoutControl_SeatClicked;
        }

        private void SeatsLayoutControl_SeatClicked(object sender, string e)
        {
            string seatId = e;
            if (seatId != null)
            {
                viewModel.Seat = seatId;
            }
            else
            {
                viewModel.Seat = "";
            }
        }


        private void VoucherCard_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            viewModel.voucherTotalPercent = 0;
            viewModel.VoucherTotalValue = 0;
            foreach (var card in viewModel.Vouchers)
            {  
                if (card.VoucherChecked)
                {

                    viewModel.voucherTotalPercent += card.VoucherValue;
                    viewModel.VoucherTotalValue = -(viewModel.OriginalPrice * viewModel.voucherTotalPercent / 100);
                    viewModel.FinalPrice = viewModel.OriginalPrice + viewModel.VoucherTotalValue;
                }
            }


        }
        void setSeats()
        {
            seatLayout.reset();
            foreach (var seat in viewModel.BookedSeatList)
            {
                viewModel.Seat = null;
                seatLayout.SetSeatOccupied(seat);
            }
            viewModel.setOriginalPrice();
            viewModel.FinalPrice = viewModel.OriginalPrice;
        }

        private void timeStampComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setSeats();
        }
    }


}
