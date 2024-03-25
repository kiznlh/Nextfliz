using Microsoft.EntityFrameworkCore;
using Nextfliz.Views.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Nextfliz
{
    class AddFilmVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string id;
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
        private Genre chosenGenre { get; set; }
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
        private Director chosenDirector { get; set; }
        public Director ChosenDirector
        {
            get { return chosenDirector; }
            set
            {
                chosenDirector = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ChosenDirector"));
            }
        }
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
        public ObservableCollection<Actor> actorList { get; set; } = new ObservableCollection<Actor>();
        public ObservableCollection<Actor> chosenActors { get; set; } = new ObservableCollection<Actor>();
        public RelayCommand addActorCommand { get; set; }
        public RelayCommand removeActorCommand { get; set; }
        public RelayCommand searchCharacterCommand { get; set; }
        public RelayCommand addFilmCommand { get; set; }
        private AddFilmWindow window;
        public AddFilmVM(AddFilmWindow window, string id)
        {
            name = "";
            image = "";
            time = "";
            rating = "";
            certification = "";
            year = "";
            this.window = window;
            this.id = id;
            addActorCommand = new RelayCommand(chooseActor, canPerform);
            removeActorCommand = new RelayCommand(removeActor, canPerform);
            searchCharacterCommand = new RelayCommand(searchFilm, canPerform);


            if (id.Length != 0)
            {
                loadDataToEdit(id);
                addFilmCommand = new RelayCommand(editFilm, canPerform);
            }
            else
            {
                loadData();
                addFilmCommand = new RelayCommand(addFilm, canPerform);
            }
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

        private void loadDataToEdit(string id)
        {
            using (var context = new NextflizContext())
            {
                var movie = context.Movies.FirstOrDefault(m => m.MovieId == id);
                if (movie == null)
                {
                    window.Close();
                    return;
                }

                name = movie.TenPhim;
                image = movie.HinhAnh;
                time = movie.ThoiLuong.ToString();
                rating = movie.DiemDanhGia.ToString().Replace(",", ".");
                certification = movie.Certification;
                year = movie.NamPhatHanh.ToString();

                var actorsNotInMovie = context.Actors
                                        .Where(actor => !context.FilmCasts.Any(fc => fc.MovieId == movie.MovieId && fc.ActorId == actor.ActorId))
                                        .ToList();
                foreach (var actor in actorsNotInMovie)
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

                if (movie.GenreId != null)
                {
                    var genre = context.Genres.FirstOrDefault(g => g.GenreId == movie.GenreId);
                    chosenGenre = genre;
                }
                if (movie.DirectorId != null)
                {
                    var director = context.Directors.FirstOrDefault(d => d.DirectorId == movie.DirectorId);
                    chosenDirector = director;
                }
                var actorsInMovie = (
                    from actor in context.Actors
                    join filmCast in context.FilmCasts on actor.ActorId equals filmCast.ActorId
                    where filmCast.MovieId == movie.MovieId
                    select new
                    {
                        actor.ActorId,
                        actor.HoTen,
                        actor.TieuSu,
                        actor.HinhAnh,
                    }
                ).ToList();
                foreach (var actor in actorsInMovie)
                {
                    Actor actorToAdd = new Actor();
                    actorToAdd.ActorId = actor.ActorId;
                    actorToAdd.HinhAnh = actor.HinhAnh;
                    actorToAdd.HoTen = actor.HoTen;
                    actorToAdd.TieuSu = actor.TieuSu;
                    chosenActors.Add(actorToAdd);
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
            using (var context = new NextflizContext())
            {
                var filmCastsToRemove = context.FilmCasts
                                                .Where(fc => fc.MovieId == id)
                                                .ToList();

                context.FilmCasts.RemoveRange(filmCastsToRemove);
                context.SaveChanges();

                foreach (Actor actor in chosenActors)
                {
                    FilmCast newItem = new FilmCast();
                    newItem.ActorId = actor.ActorId;
                    newItem.MovieId = id;
                    context.FilmCasts.Add(newItem);
                }
            }
        }

        private void removeActor(object obj)
        {
            if (obj is Actor actorToRemove)
            {
                chosenActors.Remove(actorToRemove);
                actorList.Add(actorToRemove);
            }
            using (var context = new NextflizContext())
            {
                var filmCastsToRemove = context.FilmCasts
                                                .Where(fc => fc.MovieId == id)
                                                .ToList();

                context.FilmCasts.RemoveRange(filmCastsToRemove);
                context.SaveChanges();

                foreach (Actor actor in chosenActors)
                {
                    FilmCast newItem = new FilmCast();
                    newItem.ActorId = actor.ActorId;
                    newItem.MovieId = id;
                    context.FilmCasts.Add(newItem);
                }
            }
        }

        private void addFilm(object obj)
        {
            if (name.Length == 0 || image.Length == 0 || time.Length == 0 || rating.Length == 0 || year.Length == 0 || certification.Length == 0 || chosenGenre == null || chosenDirector == null || chosenActors.Count == 0)
            {
                MessageBox.Show("Có trường đang để trống hoặc không hợp lệ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            using (var context = new NextflizContext())
            {
                var newMovie = new Movie();

                newMovie.TenPhim = name;
                newMovie.HinhAnh = image;
                newMovie.ThoiLuong = int.Parse(time);
                newMovie.DiemDanhGia = myMath.convertToDouble(rating);
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

        private void editFilm(object obj)
        {
            if (name.Length == 0 || image.Length == 0 || time.Length == 0 || rating.Length == 0 || year.Length == 0 || certification.Length == 0 || chosenGenre == null || chosenDirector == null || chosenActors.Count == 0)
            {
                MessageBox.Show("Có trường đang để trống hoặc không hợp lệ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            using (var context = new NextflizContext())
            {
                var movie = context.Movies.FirstOrDefault(m => m.MovieId == id);

                movie.TenPhim = name;
                movie.HinhAnh = image;
                movie.ThoiLuong = int.Parse(time);
                movie.DiemDanhGia = myMath.convertToDouble(rating);
                movie.NamPhatHanh = int.Parse(year);
                movie.GenreId = chosenGenre.GenreId;
                movie.DirectorId = chosenDirector.DirectorId;
                movie.Certification = certification;

                var filmCastsToRemove = context.FilmCasts
                                                .Where(fc => fc.MovieId == id)
                                                .ToList();

                context.FilmCasts.RemoveRange(filmCastsToRemove);
                context.SaveChanges();

                foreach (Actor actor in chosenActors)
                {
                    FilmCast newItem = new FilmCast();
                    newItem.ActorId = actor.ActorId;
                    newItem.MovieId = movie.MovieId;
                    context.FilmCasts.Add(newItem);
                }

                context.SaveChanges();
            }
            window.Close();
        }

        private void searchFilm(object value)
        {
            if (searchText.Length == 0)
                return;

            actorList.Clear();
            using (var context = new NextflizContext())
            {
                var items = context.Actors.Where(actor => !context.FilmCasts.Any(fc => fc.MovieId == id && fc.ActorId == actor.ActorId)).Where(e => e.HoTen.Contains(searchText)).ToList();

                foreach (var actor in chosenActors)
                {
                    var chosen = items.FirstOrDefault(x => x.ActorId == actor.ActorId);
                    items.Remove(chosen);
                }

                foreach (var item in items)
                {
                    actorList.Add(item);
                }
            }
        }
        private bool canPerform(object value)
        {
            return true;
        }
    }
}
