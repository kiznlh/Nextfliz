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

            viewModel.OnRequestClose += (s, e) => this.Close();

            foreach (var item in viewModel.Vouchers)
            {
                if (item is VoucherCard voucherCard)
                {
                    voucherCard.PropertyChanged += VoucherCard_PropertyChanged;
                }
            }

            seatLayout.SeatClicked += SeatsLayoutControl_SeatClicked;

            Loaded += Window_Loaded;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in viewModel.Vouchers)
            {

                if (item.VoucherID == "sinhnhat")
                {
                    item.VoucherChecked = true;
                    break;
                }

            }
        }
        private void SeatsLayoutControl_SeatClicked(object sender, string e)
        {
            string seatId = e;
            if (seatId != null)
            {
                viewModel.Seat = seatId;
            }
            
        }


        private void VoucherCard_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            checkPrice();
        }
        void checkPrice()
        {
            viewModel.voucherTotalPercent = 0;
            viewModel.VoucherTotalValue = 0;
            viewModel.FinalPrice = viewModel.OriginalPrice;
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
            foreach (var item in viewModel.Vouchers)
            {

                if (item.VoucherID == "sinhnhat")
                {
                    item.VoucherChecked = false;
                    item.VoucherChecked = true;
                    break;
                }

            }
        }

        private void timeStampComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setSeats();
        }

    
    }


}
