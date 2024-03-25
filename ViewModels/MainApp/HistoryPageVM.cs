using Nextfliz.Views.MainApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Nextfliz.ViewModels.MainApp
{

    public class History
    {
        public string TicketID { get; set; }
        public string MovieName { get; set; }
        public DateTime NgayDat { get; set; }
        public string Seat { get; set; }
        public History() 
        {

        }
    }
    public class HistoryPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand onViewDetailClick { get; set; }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<History> PurchaseHistory { get; set; }

        public RelayCommand ItemClickCommand { get; set; }
        public HistoryPageVM()
        {
            PurchaseHistory = new ObservableCollection<History>();
            onViewDetailClick = new RelayCommand(viewDetailClick, canPerform);
            using (var context = new NextflizContext())
            {
                var username = UserSession.username;

                var ticketList = context.Tickets.Where(ticket => ticket.Username == username).ToList();

                foreach (var ticket in ticketList)
                {
                    History history = new History();
                    history.TicketID = ticket.TicketId;

                    var saidMovie = context.Movies.Where(movie => movie.MovieId == ticket.MovieId).FirstOrDefault();

                    history.MovieName = saidMovie.TenPhim;

                    history.NgayDat = (DateTime)ticket.NgayDatVe;

                    history.Seat = ticket.ViTriGhe;

                    PurchaseHistory.Add(history);
                }
            }
           
        }

        private void viewDetailClick(object sender)
        {
            if (sender is History history)
            {
                string ticketId = history.TicketID;
                HistoryDetailWindow historyDetailWindow = new HistoryDetailWindow(ticketId);
                historyDetailWindow.ShowDialog();
            }
        }

        private bool canPerform (object value)
        {
            return true;
        }
       
    }
}
