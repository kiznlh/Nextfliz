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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Nextfliz.ViewModels.MainApp
{
    public class UserProfilePageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set 
            { 
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        private SecureString _oldPassword;
        public SecureString OldPassword
        {
            get { return _oldPassword; }
            set
            {
                _oldPassword = value;
                OnPropertyChanged(nameof(OldPassword));
            }
        }
        private SecureString _newPassword;
        public SecureString NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
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
        private int _gender;
        public int Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        public RelayCommand SaveInformation { get; set; }
        public RelayCommand SavePassword { get; set; }

        public RelayCommand PurchaseHistory {  get; set; }

        public RelayCommand LogoutCommand { get; set; }
        public UserProfilePageVM(string username) 
        {
            UserName = username;
            SaveInformation = new RelayCommand(saveInfor,canSaveInfor);
            SavePassword = new RelayCommand(savePass,canSavePass);
            PurchaseHistory = new RelayCommand(showHistory, canShowHistory);
            LogoutCommand = new RelayCommand(logout,canLogout);

            using (var context = new NextflizContext()) 
            {
                var saidUser = context.Users.Where(user => user.Username == UserName).FirstOrDefault();
                if (saidUser != null)
                {
                    HoTen = saidUser.HoTen;
                    DateOnly birthdayDateOnly = saidUser.NgaySinh ?? DateOnly.FromDateTime(DateTime.Now);
                    Birthday = birthdayDateOnly.ToDateTime(new TimeOnly(0,0,0));
                    var gioiTinh =  saidUser.GioiTinh;
                    Gender = gioiTinh == "Nam" ? 0 : 1;
                    
                }

            }
        }
        public bool canShowHistory(object value)
        {
            return true;
        }
        public void showHistory(object value)
        {
            HistoryPage historyPage = new HistoryPage();
            if (Application.Current.MainWindow is WindowUserMainWindow mainWindow)
            {
                mainWindow.navigateToAPage(historyPage);
            }
        }
        public bool canSaveInfor (object value)
        {
            return true;
        }
        
        public bool canSavePass (object value)
        {
            return true;
        }
        public void savePass (object value)
        {
            if (checkPasswordNotNullOrEmpty(OldPassword) && checkPasswordNotNullOrEmpty(NewPassword) && checkPasswordNotNullOrEmpty(ConfirmPassword) )
            {
                if (ConvertSecureStringToString(NewPassword) != ConvertSecureStringToString(ConfirmPassword))
                {
                    MessageBox.Show("Mật khẩu mới khác mật khẩu xác nhận, vui lòng thử lại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                using (var context = new NextflizContext())
                {
                    var saidUser = context.Users.Where(user => user.Username == UserName).FirstOrDefault();
                    if (!SecurePasswordHasher.Verify(ConvertSecureStringToString(OldPassword), saidUser.Password))
                    {
                        MessageBox.Show("Mật khẩu cũ không trùng khớp, vui lòng thử lại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    saidUser.Password = SecurePasswordHasher.Hash(ConvertSecureStringToString(NewPassword));
                    context.SaveChanges();

                    MessageBox.Show("Mật khẩu thay đổi thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    System.Windows.Forms.Application.Restart();
                    Application.Current.Shutdown();
                }
            }
            else
            {
                MessageBox.Show("Các mật khẩu không được để trống, vui lòng thử lại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        bool checkPasswordNotNullOrEmpty(SecureString secureString)
        {
            return secureString != null && secureString.Length > 0;
        }
        public void saveInfor (object value)
        {
            if (string.IsNullOrEmpty(HoTen))
            {
                MessageBox.Show("Các ô thông tin không được bỏ trống, vui lòng thử lại!", "Lỗi", MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            using (var context = new NextflizContext())
            {
                var saidUser = context.Users.Where(user => user.Username == UserName).FirstOrDefault();
                saidUser.GioiTinh = Gender == 0 ? "Nam" : "Nữ";
                saidUser.NgaySinh = DateOnly.FromDateTime(Birthday);
                saidUser.HoTen = HoTen;

                context.SaveChanges();

                MessageBox.Show("Lưu thông tin thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public bool canLogout(object value)
        {
            return true;
        }
        public void logout(object value)
        {
            //Restart the app to prevent any bug may rise from navigation service
            System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();
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
