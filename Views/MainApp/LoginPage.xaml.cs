﻿using Nextfliz.ViewModels.MainApp;
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
    public partial class LoginPage : Page
    {
        LoginPageVM viewModel;
        public LoginPage()
        {
            InitializeComponent();

            viewModel = new LoginPageVM();

            DataContext = viewModel;

        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword; }
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
