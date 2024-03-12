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
    /// Interaction logic for WindowUserMainWindow.xaml
    /// </summary>
    public partial class WindowUserMainWindow : Window
    {
        PageSearch pageSearch;
        TestPageForNewControl testPage; 
        public WindowUserMainWindow()
        {
            InitializeComponent();
            pageSearch = new PageSearch();
            testPage = new TestPageForNewControl();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(pageSearch);
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(testPage);
        }
        private void User_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
