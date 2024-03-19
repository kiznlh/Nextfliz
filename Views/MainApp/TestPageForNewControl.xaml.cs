using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nextfliz.Views.MainApp
{
    /// <summary>
    /// Interaction logic for TestPageForNewControl.xaml
    /// </summary>
    public partial class TestPageForNewControl : Page
    {
        public TestPageForNewControl()
        {
            InitializeComponent();

            ActorDetailCard actor = new ActorDetailCard();
            actor.ImageSource = new BitmapImage(new Uri("C:\\Users\\Hai Nguyen Lam\\Documents\\Nextfliz\\Resources\\Images\\avatar.png"));
            actor.Text1 = "hehe1";
            actor.Text2 = "hehe2";
            container.Children.Add(actor);
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (Application.Current.MainWindow is WindowUserMainWindow mainWindow)
        //    {
        //        mainWindow.TestNavi();
        //    }
        //}
    }
}
