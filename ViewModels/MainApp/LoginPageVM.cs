using Microsoft.IdentityModel.Tokens;
using Nextfliz.Views.Admin;
using Nextfliz.Views.MainApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Nextfliz.ViewModels.MainApp
{

    public class LoginPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _userName;
        private SecureString _secureString;
        public SecureString SecurePassword 
        { 
            get { return _secureString; }
            set
            {
                _secureString = value;
                OnPropertyChanged(nameof(SecurePassword));
            }
        }

        public string UserName 
        { 
            get { return _userName; } 
            set 
            { 
                _userName = value;
                OnPropertyChanged(UserName);
            } 
        }
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand SignUpCommand { get; set; }
        public LoginPageVM() 
        {
            LoginCommand = new RelayCommand(login,canLogin);
            SignUpCommand = new RelayCommand(signUp,canSignUp);

            //using (var context = new NextflizContext())
            //{
            //    var exampleUser = new User()
            //    {
            //        Username = "un1",
            //        Password = SecurePasswordHasher.Hash("123"),
            //    };

            //    context.Users.Add(exampleUser);
            //    context.SaveChanges();
            //}
        }

        public bool canLogin(object value)
        {
            return !string.IsNullOrEmpty(UserName) && SecurePassword != null && SecurePassword.Length > 0;
        }
        public bool canSignUp(object value) 
        {
            return true;
        }
        public void signUp(object value)
        {
            if (Application.Current.MainWindow is WindowUserMainWindow mainWindow)
            {
                mainWindow.goToSignUp();
            }
        }

        public void login(object value)
        {
            using (var context = new NextflizContext()) 
            {
                var saidUser = context.Users.Where(user => user.Username == UserName).FirstOrDefault();
                if (saidUser != null)
                {
                    var password = saidUser.Password;

                    if (SecurePasswordHasher.Verify(ConvertSecureStringToString(SecurePassword), password))
                    {
                        if (Application.Current.MainWindow is WindowUserMainWindow mainWindow)
                        {
                            UserSession.IsLoggedIn = true;
                            UserSession.username = UserName;
                            MessageBox.Show("Đăng nhập thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                            if (saidUser.Role == "Admin")
                            {
                                AdminWindow admin = new AdminWindow();
                                admin.Show();
                                mainWindow.Close();
                            }
                            else
                            {
                                mainWindow.navigateToHome();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!","Lỗi",MessageBoxButton.OK,MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }

        private string ConvertSecureStringToString(SecureString secureString)
        {
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(secureString);
            try
            {
                return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
        }
    }
}
