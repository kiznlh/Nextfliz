using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class VoucherCard : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _voucherNumberOfUse;
        public string VoucherNumberOfUse
        {
            get { return _voucherNumberOfUse; }
            set { 
                _voucherNumberOfUse = value;
                OnPropertyChanged(nameof(VoucherNumberOfUse));
            }
        }
        private string _voucherImage;
        public string VoucherImage
        {
            get { return _voucherImage; }
            set
            {
                _voucherImage = value;
                OnPropertyChanged();
            }
        }

        private string _voucherName;
        public string VoucherName
        {
            get { return _voucherName; }
            set
            {
                _voucherName = value;
                OnPropertyChanged();
            }
        }

        private bool _voucherChecked;
        public bool VoucherChecked
        {
            get { return _voucherChecked; }
            set
            {
                _voucherChecked = value;
                OnPropertyChanged(nameof(VoucherChecked));
            }
        }

        public double VoucherValue { get; set; }

        public VoucherCard()
        {
            InitializeComponent();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void VoucherCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                bool isChecked = checkBox.IsChecked ?? false;
                // Perform any logic based on isChecked
            }
        }
    }
}