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
using System.Windows.Shapes;

namespace Nextfliz.Views.Admin
{
    /// <summary>
    /// Interaction logic for EditGenrePopup.xaml
    /// </summary>
    public partial class EditGenrePopup : Window
    {
        public EditGenrePopup(string itemId)
        {
            InitializeComponent();
            this.DataContext = new EditGenrePopupVM(this, itemId);
        }
    }
}
