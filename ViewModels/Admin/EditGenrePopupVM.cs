using Nextfliz.Views.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Nextfliz
{
    class EditGenrePopupVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand saveEditItemCommand { get; set; }
        private string itemId;
        private EditGenrePopup popup; 
        private string newItemName { get; set; }
        public string NewItemName
        {
            get { return newItemName; }
            set
            {
                if (newItemName != value)
                {
                    newItemName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NewItemName"));
                }
            }
        }

        public EditGenrePopupVM(EditGenrePopup popup, string itemId)
        {
            newItemName = "";
            this.itemId = itemId;
            this.popup = popup;
            saveEditItemCommand = new RelayCommand(saveEditItem, canPerform);
        }

        private bool CheckDuplicateGenre(string name)
        {
            using (var dbContext = new NextflizContext())
            {
                return dbContext.Genres.Any(a => a.TenTheLoai == name);
            }
        }

        private void saveEditItem(object obj)
        {
            if (newItemName.Length == 0)
                return;

            using (var dbContext = new NextflizContext())
            {
                var itemToUpdate = dbContext.Genres.FirstOrDefault(a => a.GenreId == itemId);
                string oldName = itemToUpdate.TenTheLoai;

                if (itemToUpdate != null)
                {
                    itemToUpdate.TenTheLoai = "";

                    dbContext.SaveChanges();
                }

                if (CheckDuplicateGenre(newItemName))
                {
                    MessageBox.Show("Thể loại này đã tồn tại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (itemToUpdate != null)
                {
                    itemToUpdate.TenTheLoai = newItemName;

                    dbContext.SaveChanges();
                }
            }
            popup.Close();
        }

        private bool canPerform(object value)
        {
            return true;
        }
    }
}
