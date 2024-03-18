using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nextfliz.ViewModels.MainApp
{
    public class FilmCardViewModel : INotifyPropertyChanged
    {
        private double controlWidth;
        private double controlHeight;

        public double ControlWidth
        {
            get { return controlWidth; }
            set
            {
                controlWidth = value;
                OnPropertyChanged(nameof(ControlWidth));
            }
        }

        public double ControlHeight
        {
            get { return controlHeight; }
            set
            {
                controlHeight = value;
                OnPropertyChanged(nameof(ControlHeight));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
