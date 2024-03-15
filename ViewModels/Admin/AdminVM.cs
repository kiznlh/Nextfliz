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
using System.Windows;
using System.Windows.Controls;

namespace Nextfliz
{
    class AdminVM
    {
        private Frame contentFrame;
        private Window mainWindow;
        private Button currentTabButton;

        public RelayCommand ToDashboard { get; set; }
        public RelayCommand ToFilmManagement { get; set; }
        public RelayCommand ToVoucherManagement { get; set; }
        public RelayCommand ToCharacterManagement { get; set; }

        public AdminVM(Frame contentFrame, Window mainWindow) {
            this.contentFrame = contentFrame;
            this.mainWindow = mainWindow;

            contentFrame.Navigate(new AdminDashboard());
            currentTabButton = (Button)mainWindow.FindName("dashboardNavBtn");

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
            Style selected = (Style) Application.Current.FindResource("AdminNavButtonSelected");
            Style unselected = (Style) Application.Current.FindResource("AdminNavButton");
            currentTabButton.Style = unselected;
            currentTabButton = (Button)mainWindow.FindName("dashboardNavBtn");
            currentTabButton.Style = selected;
        }

        private void OnToFilmManagement(object value)
        {
            contentFrame.Navigate(new FilmManagement());
            Style selected = (Style)Application.Current.FindResource("AdminNavButtonSelected");
            Style unselected = (Style)Application.Current.FindResource("AdminNavButton");
            currentTabButton.Style = unselected;
            currentTabButton = (Button)mainWindow.FindName("filmNavBtn");
            currentTabButton.Style = selected;
        }

        private void OnToVoucherManagement(object value)
        {
            Style selected = (Style)Application.Current.FindResource("AdminNavButtonSelected");
            Style unselected = (Style)Application.Current.FindResource("AdminNavButton");
            currentTabButton.Style = unselected;
            currentTabButton = (Button)mainWindow.FindName("voucherNavBtn");
            currentTabButton.Style = selected;
        }

        private void OnToCharacterManagement(object value)
        {
            contentFrame.Navigate(new CharacterManagement());
            Style selected = (Style)Application.Current.FindResource("AdminNavButtonSelected");
            Style unselected = (Style)Application.Current.FindResource("AdminNavButton");
            currentTabButton.Style = unselected;
            currentTabButton = (Button)mainWindow.FindName("characterNavBtn");
            currentTabButton.Style = selected;
        }
    }
}
