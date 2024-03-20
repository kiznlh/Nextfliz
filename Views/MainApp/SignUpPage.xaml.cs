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
    /// Interaction logic for SignInPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // Handle login logic here
            // For example:
            // string username = txtUsername.Text;
            // string password = txtPassword.Password;
            // Validate credentials and perform authentication

            if (Application.Current.MainWindow is WindowUserMainWindow mainWindow)
            {
                mainWindow.goToLogin();
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the sign-up page
            // For example:
            // SignUpPage signUpPage = new SignUpPage();
            // NavigationService.Navigate(signUpPage);

            if (Application.Current.MainWindow is WindowUserMainWindow mainWindow)
            {
                //Update to database then move to login
                mainWindow.goToLogin();
            }
        }


        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (Application.Current.MainWindow is WindowUserMainWindow mainWindow)
        //    {
        //        mainWindow.TestNavi();
        //    }
        //}
    }


}
