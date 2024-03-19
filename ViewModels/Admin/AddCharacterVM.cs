using Nextfliz.View.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nextfliz
{
    class AddCharacterVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private AddCharacterWindow dialog;
        private int type;
        public RelayCommand addCharacterCommand { get; set; }

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
        private string biography { get; set; }
        public string Biography
        {
            get { return biography; }
            set
            {
                if (biography != value)
                {
                    biography = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Biography"));
                }
            }
        }

        public AddCharacterVM(AddCharacterWindow dialog, int type)
        {
            this.dialog = dialog;
            this.type = type;
            name = "";
            image = "";
            biography = "";
            addCharacterCommand = new RelayCommand(addCharacter, canPerform);
        }

        private void addCharacter(object value)
        {
            if (name.Length == 0 || image.Length == 0 || biography.Length == 0)
                return;

            if (type == 0)
            {
                using (var context = new NextflizContext())
                {
                    var newActor = new Actor();

                    newActor.HoTen = name;
                    newActor.HinhAnh = image;
                    newActor.TieuSu = biography;

                    context.Actors.Add(newActor);

                    context.SaveChanges();
                }
            }
            else
            {
                using (var context = new NextflizContext())
                {
                    var newDirector = new Director();

                    newDirector.HoTen = name;
                    newDirector.HinhAnh = image;
                    newDirector.TieuSu = biography;

                    context.Directors.Add(newDirector);

                    context.SaveChanges();
                }
            }
            dialog.Close();
        }

        private bool canPerform(object value)
        {
            return true;
        }
    }
}
