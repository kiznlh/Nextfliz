using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for PageSearch.xaml
    /// </summary>
    public partial class PageSearch : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<UserControl> _searchResults;
        public ObservableCollection<UserControl> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                OnPropertyChanged(nameof(SearchResults));
            }
        }

        private ObservableCollection<UserControl> _displayedResults;
        public ObservableCollection<UserControl> DisplayedResults
        {
            get { return _displayedResults; }
            set
            {
                _displayedResults = value;
                OnPropertyChanged(nameof(DisplayedResults));
            }
        }

        public string SearchText { get; set; }
        private int currentPage = 0;
        private int itemsPerPage = 2; // Set your desired number of items per page

        public PageSearch()
        {
            InitializeComponent();
            SearchResults = new ObservableCollection<UserControl>();
            DisplayedResults = new ObservableCollection<UserControl>();
            DataContext = this;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Simulate search functionality
            // Add search results to the ObservableCollection
            SearchResults.Clear();
            for (int i = 0; i < 2; i++)
            {
                FilmCardControl filmCardControl = new FilmCardControl();

                SearchResults.Add(filmCardControl);
            }

            for (int i = 0; i < 1; i++)
            {
                FilmCardControl filmCardControl = new FilmCardControl();

                SearchResults.Add(filmCardControl);
            }

            // Show the first page of results
            ShowResultsPage(0);
        }


        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // Show the next page of results
            ShowResultsPage(currentPage + 1);
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            // Show the previous page of results
            ShowResultsPage(currentPage - 1);
        }

        private void ShowResultsPage(int page)
        {
            int maxPage = (int)Math.Ceiling((double)SearchResults.Count / itemsPerPage) - 1;
            if (page < 0)
                page = 0;
            else if (page > maxPage)
                page = maxPage;

            currentPage = page;
            int startIndex = currentPage * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, SearchResults.Count);

            // Clear the existing items
            DisplayedResults.Clear();

            // Add items for the current page
            for (int i = startIndex; i < endIndex; i++)
            {
                DisplayedResults.Add(SearchResults[i]);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
