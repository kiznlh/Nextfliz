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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Nextfliz.Views.MainApp
{
    /// <summary>
    /// Interaction logic for UserProfilePage.xaml
    /// </summary>
    /// 

    public class NameRule : ValidationRule
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

    public partial class UserProfilePage : Page
    {
        UserProfilePageVM viewModel; 
        public UserProfilePage(string username)
        {
            InitializeComponent();
            viewModel = new UserProfilePageVM(username);
            DataContext = viewModel;
        }
        private void oldPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).OldPassword = ((PasswordBox)sender).SecurePassword; }
        }

        private void newPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).NewPassword = ((PasswordBox)sender).SecurePassword; }
        }

        private void confirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).ConfirmPassword = ((PasswordBox)sender).SecurePassword; }
        }

 
    }   


}
