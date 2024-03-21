using LiveCharts;
using LiveCharts.Wpf;
using Nextfliz.Views;
using Nextfliz.Views.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nextfliz
{
    public class nonEmptyRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = (string)value;
            if (text == null || text.Length == 0)
            {
                return new ValidationResult(false, "Nội dung này không được để trống");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }

    public class timeInputRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = (string)value;
            string pattern = @"^[1-9]\d*$";
            if (text == null || text.Length == 0)
            {
                return new ValidationResult(false, "Nội dung này không được để trống");
            }
            if (!Regex.IsMatch(text, pattern))
            {
               return new ValidationResult(false, "Vui lòng nhập số nguyên dương");
            }
            if (int.Parse(text) <= 0)
            {
                return new ValidationResult(false, "Thời lượng phim không hợp lệ");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class ratingInputRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = (string)value;
            string pattern = @"^(0*[1-9]\d*\.?\d*|0+\.\d*[1-9]\d*)$";
            if (text == null || text.Length == 0)
            {
                return new ValidationResult(false, "Nội dung này không được để trống");
            }
            if (!Regex.IsMatch(text, pattern))
            {
                return new ValidationResult(false, "Vui lòng nhập số thập phân");
            }
            if (int.Parse(text) < 0 || int.Parse(text) > 10)
            {
                return new ValidationResult(false, "Đánh giá không hợp lệ phải trong khoảng 0.0  - 10.0");
            }
            return ValidationResult.ValidResult;
        }
    }

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
            contentFrame.Navigate(new FilmManagement((Frame) contentFrame));
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
