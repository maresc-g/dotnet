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
    public class PlaylistViewModel : LibraryViewModel
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
                    RaisePropertyChanged("Medias");
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

        #region SavePlaylist
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

        private bool _asc;
        private string _order;
        private string _name;

        public PlaylistViewModel(string name, CurrentPlaylistViewModel currentPlaylist)
        {
            CurrentPlaylistViewModel = currentPlaylist;
            _name = name;
            Medias = getPlaylists();
            _asc = true;
            _order = "";
            MainWindow.ClosingWindow += (sender, e) =>
            {
                using (var fs = new FileStream("../../Libraries/Playlists/" + _name + ".xml", FileMode.Truncate))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Media>));
                    xml.Serialize(fs, Medias);
                }
                App.Current.Shutdown();
            };
        }

        public void savePlaylist()
        {
            using (var fs = new FileStream("../../Libraries/Playlists/" + _name + ".xml", FileMode.Truncate))
            {
                XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Media>));
                xml.Serialize(fs, Medias);
            }
        }

        private ObservableCollection<Media> getPlaylists()
        {
            using (var fs = new FileStream("../../Libraries/Playlists/" + _name + ".xml", FileMode.Open))
            {
                XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Media>));
                var ret = (ObservableCollection<Media>)xml.Deserialize(fs);
                return (ret);
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
                        CurrentPlaylistViewModel.NewPlaylist(new ObservableCollection<Media> { new Media { Name = media.Name, Path = media.Path } });
                    }
                }
            }
        }
        private bool PlayMediaEvaluate(object context) { return true; }

        public void addToLibrary(string path)
        {
            Media media = new Media()
            {
                Name = path.Substring(path.LastIndexOf("\\") + 1, path.LastIndexOf(".") - path.LastIndexOf("\\") - 1),
                Path = path
            };
            Medias.Add(media);
        }

        public void removePlaylist(Media media)
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

        public void changePropertiesPlaylist(Media media)
        {
            throw new NotImplementedException();
        }
    }
}
