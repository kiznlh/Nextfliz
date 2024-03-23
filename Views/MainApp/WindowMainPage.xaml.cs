using Nextfliz.ViewModels.MainApp;
using System.Windows;
using System.Windows.Controls;

namespace Nextfliz.Views.MainApp
{
    public partial class WindowMainPage : Page
    {
        WindowMainPageVM viewModel;
        public WindowMainPage()
        {
            InitializeComponent();

            viewModel = new WindowMainPageVM();
    
            DataContext = viewModel;

            var hotMovies = viewModel.hotMovies;

            foreach (var hotMovie in hotMovies)
            {
                hotMovieSlider.AddItem(hotMovie);
            }

            var randomMovies = viewModel.randomMovies;
            
            foreach (var randomMovie in randomMovies)
            {
                randomMovieSlider.AddItem(randomMovie);
            }
        }
       
    }
}
