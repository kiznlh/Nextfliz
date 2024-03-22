using Nextfliz.Views.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Nextfliz
{
    class GenreManagementVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Genre> showingList { get; set; } = new ObservableCollection<Genre>();
        private EditGenrePopup thisPopup;
        public RelayCommand addButtonCommand { get; set; }
        public RelayCommand deleteItemCommand { get; set; }
        public RelayCommand editItemCommand { get; set; }
        private string newItem { get; set; }
        public string NewItem
        {
            get { return newItem; }
            set
            {
                if (newItem != value)
                {
                    newItem = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NewItem"));
                }
            }
        }
        
        public GenreManagementVM(EditGenrePopup popup)
        {
            newItem = "";
            addButtonCommand = new RelayCommand(addButtonClicked, canPerform);
            deleteItemCommand = new RelayCommand(deleteItem, canPerform);
            editItemCommand = new RelayCommand(editItem, canPerform);

            updateList();
        }

        private void updateList()
        {
            showingList.Clear();
            using (var context = new NextflizContext())
            {
                var items = context.Genres.ToList();

                foreach (var item in items)
                {
                    showingList.Add(item);
                }
            }
        }

        private bool CheckDuplicateGenre(string name)
        {
            using (var dbContext = new NextflizContext())
            {
                return dbContext.Genres.Any(a => a.TenTheLoai == name);
            }
        }

        private void addButtonClicked(object value)
        {
            if (newItem.Length == 0)
                return;
            if (CheckDuplicateGenre(newItem))
            {
                MessageBox.Show("Thể loại này đã tồn tại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            using (var context = new NextflizContext())
            {
                var newGenre = new Genre();

                newGenre.TenTheLoai = newItem;
                context.Genres.Add(newGenre);

                context.SaveChanges();
            }
            newItem = "";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NewItem"));
            updateList();
        }

        private void deleteItem(object obj)
        {
            if (obj is Genre itemToDelete)
            {
                using (var dbContext = new NextflizContext())
                {
                    var movies =  dbContext.Movies.Where(a => a.GenreId == itemToDelete.GenreId).ToList();
                    foreach (var movie in movies)
                    {
                        movie.GenreId = null;
                    }
                    dbContext.SaveChanges();
                    dbContext.Genres.Remove(itemToDelete);
                    dbContext.SaveChanges();
                }
            }
            updateList();
        }

        private void editItem(object obj)
        {
            if (obj is Genre itemToUpdate)
            {
                EditGenrePopup popup = new EditGenrePopup(itemToUpdate.GenreId);
                popup.ShowDialog();
                updateList();
            }
        }

        private bool canPerform(object value)
        {
            return true;
        }
    }
}
