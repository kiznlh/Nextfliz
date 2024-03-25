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
using System.Windows.Shapes;

namespace Nextfliz.Views.MainApp
{
    /// <summary>
    /// Interaction logic for HistoryDetailWindow.xaml
    /// </summary>
    public partial class HistoryDetailWindow : Window
    {
        HistoryDetailWindowVM viewModel;
        public HistoryDetailWindow(string ticketID)
        {
            InitializeComponent();
            viewModel = new HistoryDetailWindowVM(ticketID);
            DataContext = viewModel;

        }
    }
}
