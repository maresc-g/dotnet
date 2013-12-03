using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DotNetProject.ViewModels
{
    public class LibraryViewModel : ViewModelBase
    {
        #region Songs
        private ObservableCollection<Song> _songs;
        public ObservableCollection<Song> Songs
        {
            get { return _songs; }
            set
            {
                if (_songs != value)
                {
                    _songs = value;
                    RaisePropertyChanged("Songs");
                }
            }
        }
        #endregion
        public int count;
        public LibraryViewModel()
        {
            Songs = new ObservableCollection<Song>();
            Songs.Add(new Song(0, "Unknown", "Unknown", "Unknown"));
            count = 1;
        }

        public ICommand SortList { get { return new DelegateCommand(SortListCall, SortListEvaluate); } }
        private void SortListCall(object context)
        {
            string param = (string)context;
            Songs.Add(new Song(count, (string)(context), "Unknown", "Unknown"));
            if (param == "Number")
            {
                var result = Songs.OrderBy(a => a.Number);
                Songs = new ObservableCollection<Song>(result);
            }
            else if (param == "Name")
            {
                var result = Songs.OrderBy(a => a.Name);
                Songs = new ObservableCollection<Song>(result);
            }
            else if (param == "Artist")
            {
                var result = Songs.OrderBy(a => a.Artist);
                Songs = new ObservableCollection<Song>(result);
            }
            else if (param == "Album")
            {
                var result = Songs.OrderBy(a => a.Album);
                Songs = new ObservableCollection<Song>(result);
            }
            count++;
        }
        private bool SortListEvaluate(object context) { return true; }
    }
}
