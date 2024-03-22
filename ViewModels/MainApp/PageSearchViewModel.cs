using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nextfliz.ViewModels.MainApp
{
    public class MovieSearchClass
    {
        String Name { get; set; }
        String Actors { get; set; }
        
        String Director {  get; set; }

        DateTime Ngay_gio_chieu { get; set; }

        Decimal Gia_ve {  get; set; }
        
       
        public MovieSearchClass() { }
    }
    class PageSearchViewModel 
    {
        public ObservableCollection<Movie> Movies { get; set; }

        public RelayCommand MovieCommand { get; set; }

        public PageSearchViewModel() 
        {
            Movies = new ObservableCollection<Movie>();


        }

        void getMovieList()
        {
            Movies.Clear();
            using (var context = new NextflizContext())
            {
                var allMovies = context.Movies.ToList();

            }
        }
    }
}
