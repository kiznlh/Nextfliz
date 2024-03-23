using MaterialDesignThemes.Wpf.Internal;
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
        public String Name { get; set; }
        public String Actors { get; set; }

        public String Director { get; set; }

        public DateTime Ngay_gio_chieu { get; set; }

        public Decimal Gia_ve { get; set; }

        public int Year { get; set; }
        public SearchData() { }
    }
    class PageSearchVM : INotifyPropertyChanged
    {
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

        private ObservableCollection<SearchData> _searchDatas;
        public ObservableCollection<SearchData> SearchDatas
        {
            get { return _searchDatas; }
            set
            {
                _searchDatas = value;
                OnPropertyChanged(nameof(SearchDatas));
            }
        }

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


        public PageSearchVM()
        {
            SearchDatas = new ObservableCollection<SearchData>();

            ComboBoxItems = new ObservableCollection<ComboBoxItem>();
            ComboBoxItems.Add(new ComboBoxItem() { Content = "Tên Phim" });
            ComboBoxItems.Add(new ComboBoxItem() { Content = "Diễn viên" });
            ComboBoxItems.Add(new ComboBoxItem() { Content = "Đạo diễn" });
            SelectedItem = ComboBoxItems[0];

            SearchCommand = new RelayCommand(onSearch,canSearch);
        }

        private void onSearch(object Value)
        {
            string searchText = SearchText;

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

            }
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
                    allMovies = context.Movies.Where(m => m.TenPhim == searchText).ToList();
                }
                else
                {
                    allMovies = context.Movies.ToList();
                }
                foreach (var movie in allMovies)
                {
                    var searchData = new SearchData
                    {
                        Name = movie.TenPhim,
                        Year = (int)movie.NamPhatHanh,
                        Ngay_gio_chieu = DateTime.MinValue,
                        Gia_ve = 0 
                    };

                    var director = context.Directors.FirstOrDefault(d => d.DirectorId == movie.DirectorId);
                    if (director != null)
                    {
                        searchData.Director = director.HoTen;
                    }

                    var allFilmCast = context.FilmCasts.Where(fc => fc.MovieId == movie.MovieId).ToList();
                    foreach (var filmCast in allFilmCast)
                    {
                        var actor = context.Actors.FirstOrDefault(a => a.ActorId == filmCast.ActorId);
                        if (actor != null)
                        {
                            if (searchData.Actors == null)
                            {
                                searchData.Actors = actor.HoTen;
                            }
                            else
                            {
                                searchData.Actors += ", " + actor.HoTen;
                            }
                        }
                    }

                    var allSuatChieu = context.SuatChieus.Where(sc => sc.MovieId == movie.MovieId);
                    foreach (var suatChieu in allSuatChieu)
                    {
                        searchData.Ngay_gio_chieu = (DateTime)suatChieu.NgayGioChieu;
                        searchData.Gia_ve = suatChieu.GiaVe.HasValue ? suatChieu.GiaVe.Value : 0;
                        break;
                    }


                    SearchDatas.Add(searchData);


                }
                
            }
        }


    }
}
