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
    public class MusicViewModel : LibraryViewModel
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
                    DisplayedSongs = value;
                    RaisePropertyChanged("Songs");
                }
            }
        }
        #endregion

        #region DisplayedSongs
        private ObservableCollection<Song> _displayedSongs;
        public ObservableCollection<Song> DisplayedSongs
        {
            get { return _displayedSongs; }
            set
            {
                if (_displayedSongs != value)
                {
                    _displayedSongs = value;
                    RaisePropertyChanged("DisplayedSongs");
                }
            }
        }
        #endregion

        #region Test
        private string _test;
        public string Test
        {
            get { return _test; }
            set
            {
                if (_test != value)
                {
                    _test = value;
                    RaisePropertyChanged("Test");
                }
            }
        }
        #endregion

        #region CurrentSong
        private Song _currentSong;
        public Song CurrentSong
        {
            get { return _currentSong; }
            set
            {
                if (_currentSong != value)
                {
                    _currentSong = value;
                    RaisePropertyChanged("CurrentSong");
                }
            }
        }
        #endregion

        #region SaveSong
        private Song _saveSong;
        public Song SaveSong
        {
            get { return _saveSong; }
            set
            {
                if (_saveSong != value)
                {
                    _saveSong = value;
                    RaisePropertyChanged("SaveSong");
                }
            }
        }
        #endregion

        private bool _asc;
        private string _order;

        public MusicViewModel(CurrentPlaylistViewModel currentPlaylist)
        {
            CurrentPlaylistViewModel = currentPlaylist;
            Songs = getSongs();
            _asc = true;
            _order = "";
            MainWindow.ClosingWindow += (sender, e) =>
            {
                using (var fs = new FileStream("../../Libraries/music.xml", FileMode.Create))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Song>));
                    xml.Serialize(fs, Songs);
                }
                App.Current.Shutdown();
            };
        }

        private ObservableCollection<Song> getSongs()
        {
            try
            {
                using (var fs = new FileStream("../../Libraries/music.xml", FileMode.Open))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Song>));
                    var ret = (ObservableCollection<Song>)xml.Deserialize(fs);
                    return (ret);
                }
            }
            catch (Exception)
            {
                return (new ObservableCollection<Song>());
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
            }
            #endregion
            #region OrderDesc
            else
            {
                _asc = true;
                _order = param;
                if (param == "Number")
                {
                    var result = Songs.OrderByDescending(a => a.Number);
                    Songs = new ObservableCollection<Song>(result);
                }
                else if (param == "Name")
                {
                    var result = Songs.OrderByDescending(a => a.Name);
                    Songs = new ObservableCollection<Song>(result);
                }
                else if (param == "Artist")
                {
                    var result = Songs.OrderByDescending(a => a.Artist);
                    Songs = new ObservableCollection<Song>(result);
                }
                else if (param == "Album")
                {
                    var result = Songs.OrderByDescending(a => a.Album);
                    Songs = new ObservableCollection<Song>(result);
                }
            }
            #endregion
        }
        private bool SortListEvaluate(object context) { return true; }

        public ICommand PlaySongCommand { get { return new DelegateCommand(PlaySongCall, PlaySongEvaluate); } }
        private void PlaySongCall(object context)
        {
            var listView = context as ListView;
            if (listView != null)
            {
                if (listView.SelectedItem != null)
                {
                    Song song = listView.SelectedItem as Song;
                    if (song != null)
                    {
                        CurrentPlaylistViewModel.NewPlaylist(new ObservableCollection<Media> { new Media { Name = song.Name, Path = song.Path } });
                    }
                }
            }
        }
        private bool PlaySongEvaluate(object context) { return true; }

        public override void addToLibrary(string path)
        {
            Song song = new Song()
            {
                Number = 0,
                Name = path.Substring(path.LastIndexOf("\\") + 1, path.LastIndexOf(".") - path.LastIndexOf("\\") - 1),
                Artist = "Unknown",
                Album = "Unknown",
                Path = path
            };
            Songs.Add(song);
        }

        public void removeSong(Song song)
        {
            if (song != null)
            {
                Songs.Remove(song);
            }
        }

        public void handleKeydown(System.Collections.IList songs, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                ObservableCollection<Song> tmp = new ObservableCollection<Song>();
                foreach (Song song in songs)
                {
                    tmp.Add(song);
                }
                foreach (Song song in tmp)
                {
                    Songs.Remove(song);
                }
            }
        }

        public void changePropertiesSong(Song song)
        {
            throw new NotImplementedException();
        }

        public void filterList(string toFind)
        {
            var data = from d in Songs
                       where d.contains(toFind)
                       select d;
            DisplayedSongs = new ObservableCollection<Song>();
            foreach (var d in data)
            {
                DisplayedSongs.Add(d);
            }
        }
    }
}
