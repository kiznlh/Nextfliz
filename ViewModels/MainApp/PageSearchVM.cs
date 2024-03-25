using MaterialDesignThemes.Wpf.Internal;
using Microsoft.EntityFrameworkCore;
using Nextfliz.Views.MainApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Nextfliz.ViewModels.MainApp
{
    public class SearchData
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public String Image { get; set; }
        public String Director { get; set; }
        public String Genre { get; set; }
        public int Year { get; set; }
        public double Rating { get; set; }
    }
    class PageSearchVM : INotifyPropertyChanged
    {
        private const string ALL_TYPE = "Tất cả";
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }
        public const int numPerPage = 5; //5
        private int listSize;
        private int currentPage { get; set; } = 0;
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
        private string totalPage { get; set; } = "/ 0";
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
        public ObservableCollection<SearchData> SearchDatas { get; set; } = new ObservableCollection<SearchData>();
        public ObservableCollection<SearchData> ShowingList { get; set; } = new ObservableCollection<SearchData>();
        public ObservableCollection<SearchData> FilteredList { get; set; } = new ObservableCollection<SearchData>();
        public ObservableCollection<string> GenreFilter { get; set; } = new ObservableCollection<string>();
        public string genreFilterSelected { get; set; }
        public string GenreFilterSelected
        {
            get { return genreFilterSelected; }
            set
            {
                genreFilterSelected = value;
                OnPropertyChanged(nameof(GenreFilterSelected));
            }
        }
        public ObservableCollection<string> YearFilter { get; set; } = new ObservableCollection<string>();
        public string yearFilterSelected { get; set; }
        public string YearFilterSelected
        {
            get { return yearFilterSelected; }
            set
            {
                yearFilterSelected = value;
                OnPropertyChanged(nameof(yearFilterSelected));
            }
        }
        public ObservableCollection<string> RatingFilter { get; set; } = new ObservableCollection<string>();
        public string ratingFilterSelected { get; set; }
        public string RatingFilterSelected
        {
            get { return ratingFilterSelected; }
            set
            {
                ratingFilterSelected = value;
                OnPropertyChanged(nameof(ratingFilterSelected));
            }
        }
        private int sortSelected = 0;
        public int SortSelected
        {
            get { return sortSelected; }
            set
            {
                sortSelected = value;
                OnPropertyChanged(nameof(SortSelected));
            }
        }
        public RelayCommand toNextPage { get; set; }
        public RelayCommand toPreviousPage { get; set; }

        private ObservableCollection<ComboBoxItem> _comboBoxItems;
        public ObservableCollection<ComboBoxItem> ComboBoxItems
        {
            get { return _comboBoxItems; }
            set
            {
                _comboBoxItems = value;
                OnPropertyChanged(nameof(ComboBoxItems));
            }
        }

        private ComboBoxItem _selectedItem;
        public ComboBoxItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ToBuyTicket { get; set; }

        public PageSearchVM()
        {
            toNextPage = new RelayCommand(nextPage, canSearch);
            toPreviousPage = new RelayCommand(previousPage, canSearch);
            ToBuyTicket = new RelayCommand(toBuyTicket, canSearch);

            ComboBoxItems = new ObservableCollection<ComboBoxItem>();
            ComboBoxItems.Add(new ComboBoxItem() { Content = "Tên Phim" });
            ComboBoxItems.Add(new ComboBoxItem() { Content = "Diễn viên" });
            ComboBoxItems.Add(new ComboBoxItem() { Content = "Đạo diễn" });
            SelectedItem = ComboBoxItems[0];

            SearchCommand = new RelayCommand(onSearch,canSearch);

            GenreFilter.Add(ALL_TYPE);
            GenreFilterSelected = GenreFilter[0];

            YearFilter.Add(ALL_TYPE);
            YearFilterSelected = GenreFilter[0];

            using (var context = new NextflizContext())
            { 
                var genres = context.Genres.ToList();
                foreach (var genre in genres)
                    GenreFilter.Add(genre.TenTheLoai);
            }

                RatingFilter.Add(ALL_TYPE);
            RatingFilterSelected = RatingFilter[0];
            for (int i = 0; i < 10; i++)
            {
                int j = i + 1;
                RatingFilter.Add(i + " - " + j);
            }
        }

        private void onSearch(object Value)
        {
            string searchText = SearchText;

            sortSelected = 0;
            OnPropertyChanged(nameof(SortSelected));

            YearFilter.Clear();
            YearFilter.Add(ALL_TYPE);
            YearFilterSelected = YearFilter[0];
            GenreFilterSelected = GenreFilter[0];
            RatingFilterSelected = RatingFilter[0];

            if (string.IsNullOrEmpty(searchText))
            {
                searchText = null;
            }

            if (SelectedItem == ComboBoxItems[0])
            {
                getMovieListBasedOnName(searchText);
            }
            else if (SelectedItem == ComboBoxItems[1])
            {
                getMovieListBaseOnActor(searchText);
            }
            else if (SelectedItem == ComboBoxItems[2])
            {
                getMovieListBaseOnDirector(searchText);
            }
            paginateList(SearchDatas);
        }

        private bool canSearch(object Value)
        {
            return true;
        }

        void getMovieListBasedOnName(String? searchText)
        {
            SearchDatas.Clear();
            
            using (var context = new NextflizContext())
            {
                var allMovies = new List<Movie>();
               
                if (searchText != null)
                {
                    allMovies = context.Movies.Where(m => m.TenPhim.Contains(searchText)).ToList();
                }
                else
                {
                    return;
                }

                foreach (var movie in allMovies)
                {
                    var searchData = new SearchData
                    {
                        ID = movie.MovieId,
                        Name = movie.TenPhim ?? "null",
                        Year = movie.NamPhatHanh ?? 2000,
                        Image = movie.HinhAnh ?? "null",
                        Rating = movie.DiemDanhGia ?? 0
                    };

                    if (!YearFilter.Contains(movie.NamPhatHanh.ToString()))
                    {
                        YearFilter.Add(movie.NamPhatHanh.ToString());
                    }

                    if (movie.DirectorId != null)
                    {
                        var director = context.Directors.FirstOrDefault(d => d.DirectorId == movie.DirectorId);
                        if (director != null)
                        {
                            searchData.Director = director.HoTen;
                        }
                    }
                    else
                    {
                        searchData.Director = "Không có đạo diễn";
                    }

                    if (movie.GenreId != null)
                    {
                        var genre = context.Genres.FirstOrDefault(d => d.GenreId == movie.GenreId);
                        if (genre != null)
                        {
                            searchData.Genre = genre.TenTheLoai;
                            if (!GenreFilter.Contains(genre.TenTheLoai))
                            {
                                GenreFilter.Add(genre.TenTheLoai);
                            }
                        }
                    }
                    else
                    {
                        searchData.Genre = "Không có thể loại";
                    }


                    SearchDatas.Add(searchData);
                }
                
            }
        }

        void getMovieListBaseOnActor(String? searchText)
        {
            if (searchText == null)
                return;
            SearchDatas.Clear();
            using (var context = new NextflizContext())
            {
                var query = (from movie in context.Movies
                            join filmCast in context.FilmCasts
                                on movie.MovieId equals filmCast.MovieId
                            join actor in context.Actors
                                on filmCast.ActorId equals actor.ActorId
                            where actor.HoTen.Contains(searchText)
                             select new
                             {
                                 movie.MovieId,
                                 movie.TenPhim,
                                 movie.DirectorId,
                                 movie.GenreId,
                                 movie.HinhAnh,
                                 movie.NamPhatHanh,
                                 movie.DiemDanhGia
                             }).ToList();
                foreach (var movie in query)
                {
                    var searchData = new SearchData
                    {
                        ID = movie.MovieId,
                        Name = movie.TenPhim,
                        Year = (int)movie.NamPhatHanh,
                        Image = movie.HinhAnh,
                        Rating = (double)movie.DiemDanhGia
                    };

                    if (!YearFilter.Contains(movie.NamPhatHanh.ToString()))
                    {
                        YearFilter.Add(movie.NamPhatHanh.ToString());
                    }

                    if (movie.DirectorId != null)
                    {
                        var director = context.Directors.FirstOrDefault(d => d.DirectorId == movie.DirectorId);
                        if (director != null)
                        {
                            searchData.Director = director.HoTen;
                        }
                    }
                    else
                    {
                        searchData.Director = "Không có đạo diễn";
                    }

                    if (movie.GenreId != null)
                    {
                        var genre = context.Genres.FirstOrDefault(d => d.GenreId == movie.GenreId);
                        if (genre != null)
                        {
                            searchData.Genre = genre.TenTheLoai;
                            if (!GenreFilter.Contains(genre.TenTheLoai))
                            {
                                GenreFilter.Add(genre.TenTheLoai);
                            }
                        }
                    }
                    else
                    {
                        searchData.Genre = "Không có thể loại";
                    }


                    SearchDatas.Add(searchData);
                }
            }
        }

        void getMovieListBaseOnDirector(String? searchText)
        {
            if (searchText == null)
                return;
            SearchDatas.Clear();
            using (var context = new NextflizContext())
            {
                var query = (from movie in context.Movies
                             join director in context.Directors
                                 on movie.DirectorId equals director.DirectorId
                             where director.HoTen.Contains(searchText)
                             select new
                             {
                                 movie.MovieId,
                                 movie.TenPhim,
                                 movie.DirectorId,
                                 movie.GenreId,
                                 movie.HinhAnh,
                                 movie.NamPhatHanh,
                                 movie.DiemDanhGia
                             }).ToList();
                foreach (var movie in query)
                {
                    var searchData = new SearchData
                    {
                        ID = movie.MovieId,
                        Name = movie.TenPhim,
                        Year = (int)movie.NamPhatHanh,
                        Image = movie.HinhAnh,
                        Rating = (double)movie.DiemDanhGia
                    };

                    if (!YearFilter.Contains(movie.NamPhatHanh.ToString()))
                    {
                        YearFilter.Add(movie.NamPhatHanh.ToString());
                    }

                    if (movie.DirectorId != null)
                    {
                        var director = context.Directors.FirstOrDefault(d => d.DirectorId == movie.DirectorId);
                        if (director != null)
                        {
                            searchData.Director = director.HoTen;
                        }
                    }
                    else
                    {
                        searchData.Director = "Không có đạo diễn";
                    }

                    if (movie.GenreId != null)
                    {
                        var genre = context.Genres.FirstOrDefault(d => d.GenreId == movie.GenreId);
                        if (genre != null)
                        {
                            searchData.Genre = genre.TenTheLoai;
                            if (!GenreFilter.Contains(genre.TenTheLoai))
                            {
                                GenreFilter.Add(genre.TenTheLoai);
                            }
                        }
                    }
                    else
                    {
                        searchData.Genre = "Không có thể loại";
                    }


                    SearchDatas.Add(searchData);
                }
            }
        }

        void paginateList(ObservableCollection<SearchData> list)
        {
            ShowingList.Clear();
            listSize = list.Count;
            
            totalPage = "/ " + Math.Ceiling(list.Count() * 1.0 / numPerPage);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPage"));

            currentPage = listSize != 0 ? 1 : 0;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));

            if (list.Count > 0)
            {
                int maxIndex = listSize > numPerPage * currentPage ? numPerPage * currentPage : listSize;
                for (int i = numPerPage * (currentPage - 1); i < maxIndex; i++)
                {
                    ShowingList.Add(list[i]);
                }
            }
        }

        private void nextPage(object value)
        {
            if (currentPage == Math.Ceiling(listSize * 1.0 / numPerPage))
                return;

            if (listSize <= 0)
                return;

            ShowingList.Clear();
            currentPage++;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
            int maxIndex = listSize > numPerPage * currentPage ? numPerPage * currentPage : listSize;
            for (int i = numPerPage * (currentPage - 1); i < maxIndex; i++)
            {
                ShowingList.Add(SearchDatas[i]);
            }
        }

        private void previousPage(object value)
        {
            if (currentPage <= 1)
                return;

            if (listSize <= 0)
                return;

            ShowingList.Clear();
            currentPage--;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
            int maxIndex = listSize > numPerPage * currentPage ? numPerPage * currentPage : listSize;
            for (int i = numPerPage * (currentPage - 1); i < maxIndex; i++)
            {
                ShowingList.Add(SearchDatas[i]);
            }
        }

        public void onFilterSelected() {
            FilteredList.Clear();
            foreach (var item in SearchDatas)
            {
                FilteredList.Add(item);
            }
            if (genreFilterSelected != ALL_TYPE)
            {
                ObservableCollection<SearchData> tempList = new ObservableCollection<SearchData>();
                foreach (var data in FilteredList)
                {
                    if (data.Genre == genreFilterSelected)
                    {
                        tempList.Add(data);
                    }
                }
                FilteredList = tempList;
            }
            if (yearFilterSelected != ALL_TYPE)
            {
                ObservableCollection<SearchData> tempList = new ObservableCollection<SearchData>();
                foreach (var data in FilteredList)
                {
                    if (data.Year.ToString() == yearFilterSelected)
                    {
                        tempList.Add(data);
                    }
                }
                FilteredList = tempList;
            }
            if (ratingFilterSelected != ALL_TYPE)
            {
                int min = int.Parse(ratingFilterSelected.Split(" - ")[0].Trim());
                int max = int.Parse(ratingFilterSelected.Split(" - ")[1].Trim());
                ObservableCollection<SearchData> tempList = new ObservableCollection<SearchData>();
                foreach (var data in FilteredList)
                {
                    if (data.Rating > min && data.Rating < max)
                    {
                        tempList.Add(data);
                    }
                }
                FilteredList = tempList;
            }


            paginateList(FilteredList);
        }

        public void onSortSelected()
        {
            if (SearchDatas.Count == 0)
                return;
            onFilterSelected();
            if (sortSelected == 0)
            {
                return;
            }
            else if (sortSelected == 1)
            {
                var sortedList = new ObservableCollection<SearchData>(FilteredList.OrderBy(n => n.Name));

                paginateList(sortedList);
            }
            else if (sortSelected == 2)
            {
                var sortedList = new ObservableCollection<SearchData>(FilteredList.OrderBy(n => n.Director));

                paginateList(sortedList);
            }
            else if (sortSelected == 3)
            {
                var sortedList = new ObservableCollection<SearchData>(FilteredList.OrderBy(n => n.Year));

                paginateList(sortedList);
            }
            else if (sortSelected == 4)
            {
                var sortedList = new ObservableCollection<SearchData>(FilteredList.OrderBy(n => n.Genre));

                paginateList(sortedList);
            }
            else if (sortSelected == 5)
            {
                var sortedList = new ObservableCollection<SearchData>(FilteredList.OrderBy(n => n.Rating));

                paginateList(sortedList);
            }
        }

        private void toBuyTicket(object obj)
        {
            if (obj is SearchData movieToBuy)
            {
                FilmDetailPage filmDetailPage = new FilmDetailPage(movieToBuy.ID);
                if (Application.Current.MainWindow is WindowUserMainWindow mainWindow)
                {
                    mainWindow.navigateToAPage(filmDetailPage);
                }
            }
        }
    }
}
