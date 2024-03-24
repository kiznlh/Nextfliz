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
    /// Interaction logic for FilmDetail.xaml
    /// </summary>
    public partial class FilmDetail : Page
    {
        FilmDetailVM viewModel;
        public FilmDetail(string filmId)
        {
            InitializeComponent();
            viewModel = new FilmDetailVM(filmId);
            this.DataContext = viewModel;
        }

        private void SalesCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.loadTotalChartData();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.loadSHChartData();
        }
    }
}
