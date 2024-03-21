using Nextfliz.Views.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Nextfliz
{
    class AddFilmVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string name { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }
        private string image { get; set; }
        public string Image
        {
            get { return image; }
            set
            {
                if (image != value)
                {
                    image = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Image"));
                }
            }
        }
        private string time { get; set; }
        public string Time
        {
            get { return time; }
            set
            {
                if (time != value)
                {
                    time = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Time"));
                }
            }
        }
        private string rating { get; set; }
        public string Rating
        {
            get { return rating; }
            set
            {
                if (rating != value)
                {
                    rating = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rating"));
                }
            }
        }
        private string year { get; set; }
        public string Year
        {
            get { return year; }
            set
            {
                if (year != value)
                {
                    year = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Year"));
                }
            }
        }
        private string certification { get; set; }
        public string Certification
        {
            get { return certification; }
            set
            {
                if (certification != value)
                {
                    certification = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Certification"));
                }
            }
        }
        public ObservableCollection<Genre> genreCombobox = new ObservableCollection<Genre>();
        public ObservableCollection<Genre> GenreCombobox
        {
            get { return genreCombobox; }
            set
            {
                genreCombobox = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GenreCombobox"));
            }
        }
        public Genre chosenGenre { get; set; }
        public Genre ChosenGenre
        {
            get { return chosenGenre; }
            set
            {
                chosenGenre = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ChosenGenre"));
            }
        }
        public ObservableCollection<Director> directorList = new ObservableCollection<Director>();
        public ObservableCollection<Director> DirectorList
        {
            get { return directorList; }
            set
            {
                directorList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DirectorList"));
            }
        }
        public Director chosenDirector { get; set; }
        public Director ChosenDirector
        {
            get { return chosenDirector; }
            set
            {
                chosenDirector = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ChosenDirector"));
            }
        }

        public ObservableCollection<Actor> actorList { get; set; } = new ObservableCollection<Actor>();
        public ObservableCollection<Actor> chosenActors { get; set; } = new ObservableCollection<Actor>();
        public RelayCommand addActorCommand { get; set; }
        public RelayCommand removeActorCommand { get; set; }
        public RelayCommand addFilmCommand { get; set; }
        private AddFilmWindow window;
        public AddFilmVM(AddFilmWindow window)
        {
            name = "";
            image = "";
            time = "";
            rating = "";
            certification = "";
            year = "";
            this.window = window;
            addActorCommand = new RelayCommand(chooseActor, canPerform);
            removeActorCommand = new RelayCommand(removeActor, canPerform);
            addFilmCommand = new RelayCommand(addFilm, canPerform);

            loadData();
        }

        private void loadData()
        {
            using (var context = new NextflizContext())
            {
                var actors = context.Actors.ToList();
                foreach (var actor in actors)
                {
                    actorList.Add(actor);
                }

                var directors = context.Directors.ToList();
                foreach (var director in directors)
                {
                    directorList.Add(director);
                }

                var genres = context.Genres.ToList();
                foreach (var genre in genres)
                {
                    genreCombobox.Add(genre);
                }
            }
        }

        private void chooseActor(object obj)
        {
            if (obj is  Actor actorToAdd)
            {
                actorList.Remove(actorToAdd);
                chosenActors.Add(actorToAdd);
            }
        }

        private void removeActor(object obj)
        {
            if (obj is Actor actorToRemove)
            {
                chosenActors.Remove(actorToRemove);
                actorList.Add(actorToRemove);
            }
        }

        private void addFilm(object obj)
        {
            if (name.Length == 0 || image.Length == 0 || time.Length == 0 || rating.Length == 0 || year.Length == 0 || certification.Length == 0 || chosenGenre == null || chosenDirector == null || chosenActors.Count == 0)
                return;
            using (var context = new NextflizContext())
            {
                var newMovie = new Movie();

                newMovie.TenPhim = name;
                newMovie.HinhAnh = image;
                newMovie.ThoiLuong = int.Parse(time);
                newMovie.DiemDanhGia = Double.Parse(rating);
                newMovie.NamPhatHanh = int.Parse(year);
                newMovie.GenreId = chosenGenre.GenreId;
                newMovie.DirectorId = chosenDirector.DirectorId;
                newMovie.Certification = certification;

                foreach (Actor actor in chosenActors)
                {
                    FilmCast newItem = new FilmCast();
                    newItem.ActorId = actor.ActorId;
                    newItem.MovieId = newMovie.MovieId;
                    context.FilmCasts.Add(newItem);
                }

                context.Movies.Add(newMovie);
                context.SaveChanges();
            }
            window.Close();
        }

        private bool canPerform(object value)
        {
            return true;
        }
    }
}
