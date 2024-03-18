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
        WindowMainPage mainPage;

        FilmDetailPage filmDetailPage;
        public WindowUserMainWindow()
        {
            InitializeComponent();
            pageSearch = new PageSearch();
            testPage = new TestPageForNewControl();
            mainPage = new WindowMainPage();

            filmDetailPage = new FilmDetailPage();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(pageSearch);
            screentitle.Text = "Search";
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(mainPage);
            screentitle.Text = "Home";
        }
        private void User_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(testPage);
            screentitle.Text = "User";
        }

        public void TestNavi()
        {
            contentFrame.Navigate(filmDetailPage);
            
        }

        public void goBack()
        {
            contentFrame.GoBack();
        }

        public void goForward()
        {
            contentFrame.GoForward();
        }
    }
}
