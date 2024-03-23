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
    /// 
    public static class UserSession
    {
        private static bool isLoggedIn = false;

        public static bool IsLoggedIn
        {
            get { return isLoggedIn; }
            set { isLoggedIn = value; }
        }
    }


    public partial class WindowUserMainWindow : Window
    {
        PageSearch pageSearch;
        TestPageForNewControl testPage;
        WindowMainPage mainPage;

        LoginPage loginPage;
        SignUpPage signUpPage;
        
        public WindowUserMainWindow()
        {
            InitializeComponent();
            pageSearch = new PageSearch();
            testPage = new TestPageForNewControl();
            mainPage = new WindowMainPage();

 

            loginPage = new LoginPage();
            signUpPage = new SignUpPage();

            contentFrame.Navigate(mainPage);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(pageSearch);
            
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(mainPage);
   
        }
        private void User_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(testPage);
            //if (!UserSession.IsLoggedIn)
            //{
            //    contentFrame.Navigate(loginPage); 
            //}
            //screentitle.Text = "User";
        }
      
        public void navigateToAPage(Page page)
        {
            contentFrame.Navigate(page);
        }
     
        public void goToLogin()
        {
            contentFrame.Navigate(loginPage);
        }
        public void goToSignUp()
        {
            contentFrame.Navigate(signUpPage);
        }
        public void goBack()
        {
            if (contentFrame.NavigationService.CanGoBack)
            {
                contentFrame.GoBack();
            }
            
        }

        public void goForward()
        {
            if (contentFrame.NavigationService.CanGoForward)
            {
                contentFrame.GoForward();
            }
           
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            goBack();
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            goForward();
        }
    }
}
