using Nextfliz.ViewModels.MainApp;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nextfliz.Views.MainApp
{
    /// <summary>
    /// Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        public HistoryPageVM historyPageVM;
        public HistoryPage()
        {
            InitializeComponent();
            historyPageVM = new HistoryPageVM();
            DataContext = historyPageVM;
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem listViewItem && listViewItem.DataContext is History history)
            {
                string ticketId = history.TicketID;
                HistoryDetailWindow historyDetailWindow = new HistoryDetailWindow(ticketId);
                historyDetailWindow.ShowDialog();
            }
        }
    }
}
