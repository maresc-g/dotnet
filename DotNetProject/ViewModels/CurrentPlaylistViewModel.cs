using DotNetProject.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace DotNetProject.ViewModels
{
    public class CurrentPlaylistViewModel : LibraryViewModel
    {
        #region Medias
        private ObservableCollection<Media> _medias;
        public ObservableCollection<Media> Medias
        {
            get { return _medias; }
            set
            {
                if (_medias != value)
                {
                    _medias = value;
                    DisplayedMedias = value;
                    RaisePropertyChanged("Medias");
                }
            }
        }
        #endregion

        #region DisplayedMedias
        private ObservableCollection<Media> _displayedMedias;
        public ObservableCollection<Media> DisplayedMedias
        {
            get { return _displayedMedias; }
            set
            {
                if (_displayedMedias != value)
                {
                    _displayedMedias = value;
                    RaisePropertyChanged("DisplayedMedias");
                }
            }
        }
        #endregion

        #region CurrentMedia
        private Media _currentMedia;
        public Media CurrentMedia
        {
            get { return _currentMedia; }
            set
            {
                if (_currentMedia != value)
                {
                    _currentMedia = value;
                    RaisePropertyChanged("CurrentMedia");
                }
            }
        }
        #endregion

        #region SaveCurrentPlaylist
        private Media _saveMedia;
        public Media SaveMedia
        {
            get { return _saveMedia; }
            set
            {
                if (_saveMedia != value)
                {
                    _saveMedia = value;
                    RaisePropertyChanged("SaveMedia");
                }
            }
        }
        #endregion

        public static event EventHandler<string> NewMediaRequested;
        static public event EventHandler<int> CurrentRepeatState;
        static public event EventHandler<bool> CurrentRandomState;

        private bool _asc;
        private string _order;
        private int _currentSong;
        private int _repeat;
        private bool _random;
        private const int REPEAT_ALL = 1;
        private const int REPEAT_SINGLE = 2;

        public CurrentPlaylistViewModel()
        {
            Medias = null;
            _asc = true;
            _order = "";
            _currentSong = 0;
            PlayerViewModel.NextSongRequested += NextSongRequestedHandler;
            PlayerViewModel.PreviousSongRequested += PreviousSongRequestedHandler;
            PlayerViewModel.RepeatRequested += RepeatRequestedHandler;
            PlayerViewModel.RandomRequested += RandomRequestedHandler;
            PlayerViewModel.FirstSongRequested += FirstSongRequestedHandler;
            _repeat = 0;
            _random = false;
        }

        private void RandomRequestedHandler(object sender, EventArgs e)
        {
            _random = (_random == true ? false : true);
            if (CurrentPlaylistViewModel.CurrentRandomState != null)
                CurrentPlaylistViewModel.CurrentRandomState(this, _random);
        }

        private void RepeatRequestedHandler(object sender, EventArgs e)
        {
            if (_repeat == 0)
                _repeat = REPEAT_ALL;
            else if (_repeat == REPEAT_ALL)
                _repeat = REPEAT_SINGLE;
            else if (_repeat == REPEAT_SINGLE)
                _repeat = 0;
            if (CurrentPlaylistViewModel.CurrentRepeatState != null)
                CurrentPlaylistViewModel.CurrentRepeatState(this, _repeat);
        }

        private void FirstSongRequestedHandler(object sender, EventArgs e)
        {
            if (Medias != null)
            {
                _currentSong = 0;
                var tmp = Medias.ElementAt(_currentSong);
                if (CurrentPlaylistViewModel.NewMediaRequested != null)
                {
                    CurrentPlaylistViewModel.NewMediaRequested(this, Medias.ElementAt(_currentSong).Path);
                }
            }
        }

        private void NextSongRequestedHandler(object sender, EventArgs e)
        {
            if (Medias == null)
                return;
            int size = Medias.Count();
            if (_random)
            {
                var rand = new Random();
                var save = _currentSong;
                _currentSong = rand.Next(size);
                while (_currentSong == save)
                {
                    _currentSong = rand.Next(size);
                }
                var tmp = Medias.ElementAt(_currentSong);
                if (CurrentPlaylistViewModel.NewMediaRequested != null)
                {
                    CurrentPlaylistViewModel.NewMediaRequested(this, Medias.ElementAt(_currentSong).Path);
                }
            }
            else if (_repeat == REPEAT_SINGLE)
            {
                var tmp = Medias.ElementAt(_currentSong);
                if (CurrentPlaylistViewModel.NewMediaRequested != null)
                {
                    CurrentPlaylistViewModel.NewMediaRequested(this, Medias.ElementAt(_currentSong).Path);
                }
            }
            else if (_currentSong + 1 < size)
            {
                _currentSong++;
                var tmp = Medias.ElementAt(_currentSong);
                if (CurrentPlaylistViewModel.NewMediaRequested != null)
                {
                    CurrentPlaylistViewModel.NewMediaRequested(this, Medias.ElementAt(_currentSong).Path);
                }
            }
            else
            {
                if (_repeat == REPEAT_ALL)
                {
                    _currentSong = 0;
                    var tmp = Medias.ElementAt(_currentSong);
                    if (CurrentPlaylistViewModel.NewMediaRequested != null)
                    {
                        CurrentPlaylistViewModel.NewMediaRequested(this, Medias.ElementAt(_currentSong).Path);
                    }
                }
                else
                {
                    if (CurrentPlaylistViewModel.NewMediaRequested != null)
                    {
                        CurrentPlaylistViewModel.NewMediaRequested(this, "");
                    }
                }
            }
        }

        private void PreviousSongRequestedHandler(object sender, EventArgs e)
        {
            if (Medias == null)
                return;
            if (_currentSong - 1 >= 0)
            {
                _currentSong--;
                var tmp = Medias.ElementAt(_currentSong);
                if (CurrentPlaylistViewModel.NewMediaRequested != null)
                {
                    CurrentPlaylistViewModel.NewMediaRequested(this, Medias.ElementAt(_currentSong).Path);
                }
            }
            else
            {
                if (CurrentPlaylistViewModel.NewMediaRequested != null)
                {
                    CurrentPlaylistViewModel.NewMediaRequested(this, "");
                }
            }
        }

        public void AddToPlaylist(ObservableCollection<Media> e)
        {
            if (Medias == null)
            {
                Medias = new ObservableCollection<Media>();
            }
            foreach (Media media in e)
            {
                Medias.Add(media);
            }
        }

        public void NewPlaylist(ObservableCollection<Media> e)
        {
            Medias = null;
            Medias = new ObservableCollection<Media>();
            foreach (Media media in e)
            {
                Medias.Add(media);
            }
            _currentSong = 0;
            if (CurrentPlaylistViewModel.NewMediaRequested != null)
            {
                CurrentPlaylistViewModel.NewMediaRequested(this, Medias.First().Path);
            }
        }

        public ICommand SortList { get { return new DelegateCommand(SortListCall, SortListEvaluate); } }
        private void SortListCall(object context)
        {
            string param = (string)context;
            #region OrderAsc
            if (_asc == true || _order != param)
            {
                _asc = false;
                _order = param;
                if (param == "Name")
                {
                    var result = Medias.OrderBy(a => a.Name);
                    Medias = new ObservableCollection<Media>(result);
                }
            }
            #endregion
            #region OrderDesc
            else
            {
                _asc = true;
                _order = param;
                if (param == "Name")
                {
                    var result = Medias.OrderByDescending(a => a.Name);
                    Medias = new ObservableCollection<Media>(result);
                }
            }
            #endregion
        }
        private bool SortListEvaluate(object context) { return true; }

        public ICommand PlayMediaCommand { get { return new DelegateCommand(PlayMediaCall, PlayMediaEvaluate); } }
        private void PlayMediaCall(object context)
        {
            var listView = context as ListView;
            if (listView != null)
            {
                if (listView.SelectedItem != null)
                {
                    Media media = listView.SelectedItem as Media;
                    var test = media.Path;
                    if (media != null)
                    {
                        _currentSong = Medias.IndexOf(media);
                        if (CurrentPlaylistViewModel.NewMediaRequested != null)
                        {
                            CurrentPlaylistViewModel.NewMediaRequested(this, media.Path);
                        }
                    }
                }
            }
        }
        private bool PlayMediaEvaluate(object context) { return true; }

        public override void addToLibrary(string path)
        {
            Media media = new Media()
            {
                Name = path.Substring(path.LastIndexOf("\\") + 1, path.LastIndexOf(".") - path.LastIndexOf("\\") - 1),
                Path = path
            };
            if (Medias == null)
                Medias = new ObservableCollection<Media>();
            Medias.Add(media);
        }

        public void removeMedia(Media media)
        {
            if (media != null)
            {
                Medias.Remove(media);
            }
        }

        public void handleKeydown(System.Collections.IList playlists, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                ObservableCollection<Media> tmp = new ObservableCollection<Media>();
                foreach (Media media in playlists)
                {
                    tmp.Add(media);
                }
                foreach (Media media in tmp)
                {
                    Medias.Remove(media);
                }
            }
        }

        public void changePropertiesCurrentPlaylist(Media media)
        {
            throw new NotImplementedException();
        }

        public void filterList(string toFind)
        {
            var data = from d in Medias
                       where d.contains(toFind)
                       select d;
            DisplayedMedias = new ObservableCollection<Media>();
            foreach (var d in data)
            {
                DisplayedMedias.Add(d);
            }
        }
    }
}
