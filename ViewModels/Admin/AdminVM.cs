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
using System.Windows.Documents;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nextfliz
{
    public class myMath
    {
        public static double convertToDouble(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Input string is empty or null.");
            }

            double result = 0.0;
            bool decimalFound = false;
            double decimalPlace = 0.1;

            foreach (char c in text)
            {
                if (char.IsDigit(c)) 
                {
                    int digitValue = c - '0'; 

                    
                    if (decimalFound)
                    {
                        result += digitValue * decimalPlace;
                        decimalPlace *= 0.1; 
                    }
                    else
                    {
                        result = result * 10 + digitValue;
                    }
                }
                else if (c == '.' && !decimalFound) 
                {
                    decimalFound = true;
                }
                else 
                {
                    throw new ArgumentException("Invalid character in input string.");
                }
            }

            return result;
        }
    }
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
    public class integerInputRule : ValidationRule
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
                return new ValidationResult(false, "Số nhập vào không hợp lệ");
            }
            return ValidationResult.ValidResult;
        }
    }


    public class ratingInputRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = (string)value;
            string pattern = @"^[0-9.]+$";
            if (text == null || text.Length == 0)
            {
                return new ValidationResult(false, "Nội dung này không được để trống");
            }
            if (!Regex.IsMatch(text, pattern))
            {
                return new ValidationResult(false, "Vui lòng nhập số thập phân có phần thập phân gồm 1 chữ số(VD: 7.1)");
            }
            if (myMath.convertToDouble(text) < 0 || myMath.convertToDouble(text) > 10)
            {
                return new ValidationResult(false, "Đánh giá không hợp lệ phải trong khoảng 0  - 10");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class dateInputRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = (string)value;
            if (text == null || text.Length == 0)
            {
                return new ValidationResult(false, "Nội dung này không được để trống");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class hourInputRule : ValidationRule
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
            if (int.Parse(text) < 0 || int.Parse(text) > 24)
            {
                return new ValidationResult(false, "Giờ không hợp lệ");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class minuteInputRule : ValidationRule
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
            if (int.Parse(text) < 0 || int.Parse(text) > 59)
            {
                return new ValidationResult(false, "Phút không hợp lệ");
            }
            return ValidationResult.ValidResult;
        }
    }
    public class priceInputRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = (string)value;
            string pattern = @"^[0-9]\d{0,7}(\.\d{1,2})?$";
            if (text == null || text.Length == 0)
            {
                return new ValidationResult(false, "Nội dung này không được để trống");
            }
            if (!Regex.IsMatch(text, pattern))
            {
                return new ValidationResult(false, "Vui lòng nhập số nguyên dương hoặc thập phân dương có tối đa 2 chữ số sau dấu chấm");
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
            contentFrame.Navigate(new VoucherManagement());
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
