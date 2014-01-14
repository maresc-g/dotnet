using DotNetProject.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetProject.ViewModels
{
    class CurrentViewModel : ViewModelBase
    {
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

        #region MusicViewModel
        private MusicViewModel _MusicViewModel;
        public MusicViewModel MusicViewModel
        {
            get { return _MusicViewModel; }
            set
            {
                if (_MusicViewModel != value)
                {
                    _MusicViewModel = value;
                    RaisePropertyChanged("MusicViewModel");
                }
            }
        }
        #endregion

        #region ImageViewModel
        private ImageViewModel _imageViewModel;
        public ImageViewModel ImageViewModel
        {
            get { return _imageViewModel; }
            set
            {
                if (_imageViewModel != value)
                {
                    _imageViewModel = value;
                    RaisePropertyChanged("ImageViewModel");
                }
            }
        }
        #endregion

        #region VideoViewModel
        private VideoViewModel _VideoViewModel;
        public VideoViewModel VideoViewModel
        {
            get { return _VideoViewModel; }
            set
            {
                if (_VideoViewModel != value)
                {
                    _VideoViewModel = value;
                    RaisePropertyChanged("VideoViewModel");
                }
            }
        }
        #endregion

        #region PlayerViewModel
        private PlayerViewModel _playerViewModel;
        public PlayerViewModel PlayerViewModel
        {
            get { return _playerViewModel; }
            set
            {
                if (_playerViewModel != value)
                {
                    _playerViewModel = value;
                    RaisePropertyChanged("PlayerViewModel");
                }
            }
        }
        #endregion

        #region PlaylistViewModel
        private PlaylistViewModel _playlistViewModel;
        public PlaylistViewModel PlaylistViewModel
        {
            get { return _playlistViewModel; }
            set
            {
                if (_playlistViewModel != value)
                {
                    _playlistViewModel = value;
                    RaisePropertyChanged("PlaylistViewModel");
                }
            }
        }
        #endregion

        #region CurrentPlaylistViewModel
        private CurrentPlaylistViewModel _currentPlaylistViewModel;
        public CurrentPlaylistViewModel CurrentPlaylistViewModel
        {
            get { return _currentPlaylistViewModel; }
            set
            {
                if (_currentPlaylistViewModel != value)
                {
                    _currentPlaylistViewModel = value;
                    RaisePropertyChanged("CurrentPlaylistViewModel");
                }
            }
        }
        #endregion

        public CurrentViewModel()
        {
            CurrentPlaylistViewModel = new CurrentPlaylistViewModel();
            MusicViewModel = new MusicViewModel(CurrentPlaylistViewModel);
            PlayerViewModel = new PlayerViewModel();
            VideoViewModel = new VideoViewModel(CurrentPlaylistViewModel);
            ImageViewModel = new ImageViewModel(CurrentPlaylistViewModel);
            PlaylistViewModel = null;
            MainWindowViewModel.AddToLibrary += addToLibrary;
            MainWindowViewModel.ChangeView += HandleChangeView;
        }

        private void HandleChangeView(object sender, EventArgsStr e)
        {
            if (e.Arg.Contains("Playlist"))
            {
                string[] tmp = e.Arg.Split(' ');
                if (tmp.Length > 1)
                {
                    PlaylistViewModel = null;
                    PlaylistViewModel = new PlaylistViewModel(tmp[1], CurrentPlaylistViewModel);
                }
                else
                    return;
            }
        }

        public void addToLibrary(object sender, EventArgs e)
        {
            dlg.Filter = "Media files (" + Globals.filterMusic + ";" + Globals.filterImg + ";" + Globals.filterVideo + ")|"
                + Globals.filterMusic + ";" + Globals.filterImg + ";" + Globals.filterVideo +
                "|Music files (" + Globals.filterMusic + ")|" + Globals.filterMusic +
                "|Video files (" + Globals.filterVideo + ")|" + Globals.filterVideo +
                "|Images files (" + Globals.filterImg + ")|" + Globals.filterImg +
                "|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                foreach (string item in Globals.tabImg)
                {
                    if (dlg.FileName.IndexOf(item) != -1)
                    {
                        ImageViewModel.addToLibrary(dlg.FileName);
                        break;
                    }
                }
                foreach (string item in Globals.tabMusic)
                {
                    if (dlg.FileName.IndexOf(item) != -1)
                    {
                        MusicViewModel.addToLibrary(dlg.FileName);
                        break;
                    }
                }
                foreach (string item in Globals.tabVideo)
                {
                    if (dlg.FileName.IndexOf(item) != -1)
                    {
                        VideoViewModel.addToLibrary(dlg.FileName);
                        break;
                    }
                }
            }
        }
    }
}
