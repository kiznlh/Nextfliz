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

namespace Nextfliz.Views.Admin
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Page
    {
        private AdminDashboardVM viewModel;
        public AdminDashboard()
        {
            InitializeComponent();
            viewModel = new AdminDashboardVM();
            this.DataContext = viewModel;
        }

        private void SalesCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.loadChartData();
        }
    }
}
