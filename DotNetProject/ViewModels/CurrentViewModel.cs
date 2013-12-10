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

        public CurrentViewModel()
        {
            MusicViewModel = new MusicViewModel();
            PlayerViewModel = new PlayerViewModel();
            VideoViewModel = new VideoViewModel();
            ImageViewModel = new ImageViewModel();
            MainWindowViewModel.AddToLibrary += addToLibrary;
        }

        public void addToLibrary(object sender, EventArgs e)
        {
            string filterMusic = "*.mp3;*.wma;*.wav";
            string filterImg = "*.jpg;*.jpeg;*.bmp;*.png;*.gif";
            string filterVideo = "*.mp4";
            dlg.Filter = "Media files (" + filterMusic + ";" + filterImg + ";" + filterVideo + ")|"
                + filterMusic + ";" + filterImg + ";" + filterVideo +
                "|Music files (" + filterMusic + ")|" + filterMusic +
                "|Video files (" + filterVideo + ")|" + filterVideo +
                "|Images files (" + filterImg + ")|" + filterImg +
                "|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                int pos;
                while ((pos = filterImg.IndexOf('*')) != -1) { filterImg = filterImg.Remove(pos); }
                while ((pos = filterMusic.IndexOf('*')) != -1) { filterMusic = filterMusic.Remove(pos); }
                while ((pos = filterVideo.IndexOf('*')) != -1) { filterVideo = filterVideo.Remove(pos); }
                string []tabImg = filterImg.Split(';');
                string []tabMusic = filterMusic.Split(';');
                string []tabVideo = filterVideo.Split(';');
                foreach (string item in tabImg)
                {
                    if (dlg.FileName.IndexOf(item) != -1)
                    {
                        //MusicViewModel.addToLibrary(dlg.FileName);
                        break;
                    }
                }
                foreach (string item in tabMusic)
                {
                    if (dlg.FileName.IndexOf(item) != -1)
                    {
                        MusicViewModel.addToLibrary(dlg.FileName);
                        break;
                    }
                }
                foreach (string item in tabVideo)
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
