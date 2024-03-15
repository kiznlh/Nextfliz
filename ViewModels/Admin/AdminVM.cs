using LiveCharts;
using LiveCharts.Wpf;
using Nextfliz.Views;
using Nextfliz.Views.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Nextfliz
{
    class AdminVM
    {
        private Frame contentFrame;

        public RelayCommand ToDashboard { get; set; }
        public RelayCommand ToFilmManagement { get; set; }
        public RelayCommand ToVoucherManagement { get; set; }
        public RelayCommand ToCharacterManagement { get; set; }

        public AdminVM(Frame contentFrame) {
            this.contentFrame = contentFrame;
            contentFrame.Navigate(new AdminDashboard());

            ToDashboard = new RelayCommand(OnToDashboard, CanPerform);
            ToFilmManagement = new RelayCommand(OnToFilmManagement, CanPerform);
            ToVoucherManagement = new RelayCommand(OnToVoucherManagement, CanPerform);
            ToCharacterManagement = new RelayCommand(OnToCharacterManagement, CanPerform);
        }

        private bool CanPerform(object value)
        {
            return true;
        }

        private void OnToDashboard(object value)
        {
            contentFrame.Navigate(new AdminDashboard());
        }

        private void OnToFilmManagement(object value)
        {
            contentFrame.Navigate(new FilmManagement());
        }

        private void OnToVoucherManagement(object value)
        {

        }

        private void OnToCharacterManagement(object value)
        {
            contentFrame.Navigate(new CharacterManagement());
        }
    }
}
