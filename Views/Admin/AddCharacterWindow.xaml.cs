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

namespace Nextfliz.View.Admin
{
    /// <summary>
    /// Interaction logic for AddCharacterWindow.xaml
    /// </summary>
    public partial class AddCharacterWindow : Window
    {
        public AddCharacterWindow(int type, string itemId)
        {
            InitializeComponent();
            this.DataContext = new AddCharacterVM(this, type, itemId);
        }
    }
}
