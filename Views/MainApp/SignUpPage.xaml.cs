using Nextfliz.ViewModels.MainApp;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class NameRuleSignUp : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        try
        {
            string name = value as string;

            if (string.IsNullOrEmpty(name))
            {

                return new ValidationResult(false, "Không được để trống!");
            }


            return ValidationResult.ValidResult;
        }
        catch (Exception ex)
        {
            return new ValidationResult(false, ex.Message);
        }
    }

}
public partial class SignUpPage : Page
    {
        SignUpPageVM viewModel;
        public SignUpPage()
        {
            InitializeComponent();

            viewModel = new SignUpPageVM();

            DataContext = viewModel;
        }

      
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword; }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).ConfirmPassword = ((PasswordBox)sender).SecurePassword; }
        }

    }

}
