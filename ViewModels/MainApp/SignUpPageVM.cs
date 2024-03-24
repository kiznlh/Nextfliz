using Nextfliz.Views.MainApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Nextfliz.ViewModels.MainApp
{
    public class SignUpPageVM : INotifyPropertyChanged
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

        private SecureString _confirmPassword;

        public SecureString ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        private DateTime _birthday = DateTime.Now;


        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                OnPropertyChanged(nameof(Birthday));
            }
        }

        private ComboBoxItem _gender;
        public ComboBoxItem Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        private string _hoTen;
        public string HoTen
        {
            get { return _hoTen; }
            set
            {
                _hoTen = value;
                OnPropertyChanged(nameof(HoTen));
            }
        }
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand SignUpCommand { get; set; }
        public SignUpPageVM()
        {
            LoginCommand = new RelayCommand(login, canLogin);
            SignUpCommand = new RelayCommand(signUp, canSignUp);

        }

        public bool canLogin(object value)
        {
            return true;
        }

        public bool canSignUp(object value)
        {
            if (string.IsNullOrEmpty(UserName) || SecurePassword == null || SecurePassword.Length == 0 ||
                ConfirmPassword == null || ConfirmPassword.Length == 0 || Gender == null)
            {
                return false;
            }

            return true;
        }

        public void login(object value)
        {
            if (Application.Current.MainWindow is WindowUserMainWindow mainWindow)
            {
                mainWindow.goToLogin();
            }
        }

        public void signUp(object value)
        {
            if (ConvertSecureStringToString(SecurePassword) != ConvertSecureStringToString(ConfirmPassword))
            {
                MessageBox.Show("Mật khẩu khác với xác nhận mật khẩu, vui lòng thử lại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (var context = new NextflizContext())
            {
                var findExistingUsername = context.Users.Any(user => user.Username == UserName);
                
                if (findExistingUsername)
                {
                    MessageBox.Show("Tên người dùng đã tồn tại, vui lòng thử lại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            
            }

            using (var context = new NextflizContext())
            {
                var newUser = new User()
                {
                    Username = UserName,
                    Password = SecurePasswordHasher.Hash(ConvertSecureStringToString(SecurePassword)),
                    NgaySinh = DateOnly.FromDateTime(Birthday),
                    GioiTinh = Gender.Content.ToString(),
                    HoTen = HoTen,
                };
              
                context.Users.Add(newUser);
                try
                {
                    context.SaveChanges();
                    MessageBox.Show("Đăng kí thành công, chuyển về đăng nhập!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (Application.Current.MainWindow is WindowUserMainWindow mainWindow)
                    {
                        mainWindow.goToLogin();
                    }
                }   
                catch (Exception ex)
                {
                    // Log the exception or display an error message
                    MessageBox.Show("An error occurred while saving changes to the database: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
