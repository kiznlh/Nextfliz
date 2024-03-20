using Nextfliz.View.Admin;
using Nextfliz.Views.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nextfliz
{
    class FilmManagementVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Movie> showingList { get; set; } = new ObservableCollection<Movie>();

        public RelayCommand showGenreManagement { get; set; }
        private int listSize;
        private int currentPage { get; set; }
        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                if (currentPage != value)
                {
                    currentPage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
                }
            }
        }
        private string totalPage { get; set; }
        public string TotalPage
        {
            get { return totalPage; }
            set
            {
                if (totalPage != value)
                {
                    totalPage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPage"));
                }
            }
        }

        public const int numPerPage = 2;

        public FilmManagementVM()
        {
            showGenreManagement = new RelayCommand(showGenre, canPerform);

            updateList();
        }

        private void updateList()
        {
            showingList.Clear();
            using (var context = new NextflizContext())
            {
                var movies = context.Movies.Take(numPerPage).ToList();
                listSize = context.Movies.Count();

                foreach (var item in movies)
                {
                    showingList.Add(item);
                }

                totalPage = "/ " + Math.Ceiling(context.Movies.Count() * 1.0 / numPerPage);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPage"));

                currentPage = listSize != 0 ? 1 : 0;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
            }

            currentPage = listSize != 0 ? 1 : 0;
        }

        private void showGenre(object value)
        {
            GenreManagement genrePanel = new GenreManagement();
            genrePanel.ShowDialog();
        }

        private bool canPerform(object value)
        {
            return true;
        }
    }
}
