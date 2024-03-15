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
        public AdminVM(Frame contentFrame) {
            this.contentFrame = contentFrame;
            contentFrame.Navigate(new FilmManagement());
        }
    }
}
