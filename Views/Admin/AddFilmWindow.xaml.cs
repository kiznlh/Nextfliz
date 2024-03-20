﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Nextfliz.Views.Admin
{
    /// <summary>
    /// Interaction logic for AddFilmWindow.xaml
    /// </summary>
    public partial class AddFilmWindow : Window
    {
        public AddFilmWindow()
        {
            InitializeComponent();
            this.DataContext = new AddFilmVM(this);
        }
    }
}
