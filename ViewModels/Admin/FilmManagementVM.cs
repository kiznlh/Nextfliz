using Nextfliz.View.Admin;
using Nextfliz.Views.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Nextfliz
{
    class FilmManagementVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Frame contentFrame;
        public ObservableCollection<Movie> showingList { get; set; } = new ObservableCollection<Movie>();

        public RelayCommand showGenreManagement { get; set; }
        public RelayCommand addFilmCommand { get; set; }
        public RelayCommand toNextPage { get; set; }
        public RelayCommand toPreviousPage { get; set; }
        public RelayCommand deleteItemCommand { get; set; }
        public RelayCommand searchFilmCommand { get; set; }
        public RelayCommand viewDetailCommand { get; set; }

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
        private string searchText { get; set; }
        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SearchText"));
                }
            }
        }
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

        public const int numPerPage = 10;

        public FilmManagementVM(Frame contentFrame)
        {
            this.contentFrame = contentFrame;
            showGenreManagement = new RelayCommand(showGenre, canPerform);
            addFilmCommand = new RelayCommand(showAddPanel, canPerform);
            toNextPage = new RelayCommand(nextPage, canPerform);
            toPreviousPage = new RelayCommand(previousPage, canPerform);
            deleteItemCommand = new RelayCommand(deleteItem, canPerform);
            searchFilmCommand = new RelayCommand(searchFilm, canPerform);
            viewDetailCommand = new RelayCommand(viewDetail, canPerform);
            searchText = "";

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

        private void searchFilm(object value)
        {
            if (searchText.Length == 0)
            {
                updateList();
                return;
            }

            showingList.Clear();
            using (var context = new NextflizContext())
            {
                var movies = context.Movies.Where(e => e.TenPhim.Contains(searchText)).ToList();
                listSize = movies.Count();

                foreach (var item in movies)
                {
                    showingList.Add(item);
                }

                totalPage = "/ " + Math.Ceiling(movies.Count() * 1.0 / numPerPage);
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

        private void showAddPanel(object value)
        {
            AddFilmWindow addWindow = new AddFilmWindow("");
            addWindow.ShowDialog();
            updateList();
        }

        private void nextPage(object value)
        {
            if (currentPage == Math.Ceiling(listSize * 1.0 / numPerPage))
                return;

            showingList.Clear();
            currentPage++;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
            using (var context = new NextflizContext())
            {
                var items = context.Movies.Skip(numPerPage * (currentPage - 1)).Take(numPerPage).ToList();
                foreach (var item in items)
                {
                    showingList.Add(item);
                }
            }
        }

        private void previousPage(object value)
        {
            if (currentPage <= 1)
                return;

            showingList.Clear();
            currentPage--;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
            using (var context = new NextflizContext())
            {
                var items = context.Movies.Skip(numPerPage * (currentPage - 1)).Take(numPerPage).ToList();
                foreach (var item in items)
                {
                    showingList.Add(item);
                }
            }
        }

        private void deleteItem(object obj)
        {
            if (obj is Movie movieToDelete)
            {
                using (var dbContext = new NextflizContext())
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn tiếp tục? \n Lưu ý: xoá phim sẽ xóa luôn vé của khách hàng đã đặt ở phim này", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        Movie.DeleteMovie(movieToDelete.MovieId);
                    }

                    dbContext.SaveChanges();
                }
                updateList();
            } 
        }

        private void viewDetail(object value)
        {
            if (value is Movie movie)
            {
                contentFrame.Navigate(new FilmDetail(movie.MovieId));
            }
        }

        private bool canPerform(object value)
        {
            return true;
        }
    }
}
