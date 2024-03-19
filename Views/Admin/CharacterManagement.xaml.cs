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

namespace Nextfliz.Views.Admin
{
    /// <summary>
    /// Interaction logic for CharacterManagement.xaml
    /// </summary>
    public partial class CharacterManagement : Page
    {
        public CharacterManagement()
        {
            InitializeComponent();
            TypeCombobox.SelectedIndex = 0;
            this.DataContext = new CharacterManagementVM(TypeCombobox.SelectedIndex);//0: actor, 1: director
        }

        private void Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.DataContext = new CharacterManagementVM(TypeCombobox.SelectedIndex);
        }
    }
}
