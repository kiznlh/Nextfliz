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

        public static string username { get; set; }
    }

    public partial class WindowUserMainWindow : Window
    {
        PageSearch pageSearch;
        WindowMainPage mainPage;

        LoginPage loginPage;
        SignUpPage signUpPage;
        
        UserProfilePage userProfilePage;
        public WindowUserMainWindow()
        {
            InitializeComponent();
            pageSearch = new PageSearch();
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
            navigateToHome();
   
        }
        private void User_Click(object sender, RoutedEventArgs e)
        {
            if (UserSession.IsLoggedIn)
            {
                userProfilePage = new UserProfilePage(UserSession.username);
                navigateToAPage(userProfilePage);
                

            }
            else
            {
                contentFrame.Navigate(loginPage);

            }
        }
        
        public void navigateToHome()
        {
            mainPage = new WindowMainPage();
            contentFrame.Navigate(mainPage);
            //RemoveOneBackPage();
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

        //public void RemoveOneBackPage()
        //{
        //    if (!contentFrame.NavigationService.CanGoBack)
        //        return;

        //    contentFrame.NavigationService.LoadCompleted += NavigationService_LoadCompletedForRemove;

        //}

        //private void NavigationService_LoadCompletedForRemove(object sender, NavigationEventArgs e)
        //{
        //    contentFrame.NavigationService.LoadCompleted -= NavigationService_LoadCompletedForRemove;
        //    contentFrame.NavigationService.RemoveBackEntry();
        //}
       
    }
}
