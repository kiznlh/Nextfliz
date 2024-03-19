using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nextfliz.View.Admin;

namespace Nextfliz 
{
    
    class CharacterManagementVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand toNextPage { get; set; }
        public RelayCommand toPreviousPage { get; set; }
        public RelayCommand showAddPanel { get; set; }
        public ObservableCollection<Object> showingList { get; set; } = new ObservableCollection<Object>();
        private int listSize;
        public int listType { get; set; }

        private int currentPage {  get; set; }
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

        public const int numPerPage = 2;
        public CharacterManagementVM(int listType)
        {
            this.listType = listType;
            toNextPage = new RelayCommand(nextPage, canPerform);
            toPreviousPage = new RelayCommand(previousPage, canPerform);
            showAddPanel = new RelayCommand(showAdd, canPerform);

            using (var context = new NextflizContext())
            {
                if (listType == 0)
                {
                    var actors = context.Actors.Take(numPerPage).ToList();
                    listSize = context.Actors.Count();
                    foreach (var item in actors)
                    {
                        showingList.Add(item);
                    }
                    totalPage = "/ " + Math.Ceiling(context.Actors.Count() * 1.0 / numPerPage);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPage"));
                }
                else
                {
                    var directors = context.Directors.Take(numPerPage).ToList();
                    listSize = context.Directors.Count();
                    foreach (var item in directors)
                    {
                        showingList.Add(item);
                    }
                    totalPage = "/ " + Math.Ceiling(context.Directors.Count() * 1.0 / numPerPage);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalPage"));
                }
            }

            currentPage = listSize != 0 ? 1 : 0;
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
                if (listType == 0)
                {
                    var items = context.Actors.Skip(numPerPage * (currentPage - 1)).Take(numPerPage).ToList();
                    foreach (var item in items)
                    {
                        showingList.Add(item);
                    }
                }
                else
                {
                    var items = context.Directors.Skip(numPerPage * (currentPage - 1)).Take(numPerPage).ToList();
                    foreach (var item in items)
                    {
                        showingList.Add(item);
                    }
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
                if (listType == 0)
                {
                    var items = context.Actors.Skip(numPerPage * (currentPage - 1)).Take(numPerPage).ToList();
                    foreach (var item in items)
                    {
                        showingList.Add(item);
                    }
                }
                else
                {
                    var items = context.Directors.Skip(numPerPage * (currentPage - 1)).Take(numPerPage).ToList();
                    foreach (var item in items)
                    {
                        showingList.Add(item);
                    }
                }
            }
        }
        
        private void showAdd(object value)
        {
            AddCharacterWindow addPanel = new AddCharacterWindow();
            addPanel.ShowDialog();
        }

        private bool canPerform(object value)
        {
            return true;
        }
    }
}
