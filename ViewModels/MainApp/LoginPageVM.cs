using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

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
        public SecureString SecurePassword { private get; set; }

        public string UserName 
        { 
            get { return _userName; } 
            set 
            { 
                _userName = value;
                OnPropertyChanged(UserName);
            } 
        }

        LoginPageVM() 
        {
            
        }
    }
}
