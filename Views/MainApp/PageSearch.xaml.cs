using Nextfliz.ViewModels.MainApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for PageSearch.xaml
    /// </summary>
    public partial class PageSearch : Page
    {
        PageSearchVM viewModel;
        public PageSearch()
        {
            InitializeComponent();
            viewModel = new PageSearchVM();

            DataContext = viewModel;

        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void listView_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
