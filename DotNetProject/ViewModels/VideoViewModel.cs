﻿using DotNetProject.Models;
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
    public class VideoViewModel : LibraryViewModel
    {
        #region Videos
        private ObservableCollection<Video> _videos;
        public ObservableCollection<Video> Videos
        {
            get { return _videos; }
            set
            {
                if (_videos != value)
                {
                    _videos = value;
                    RaisePropertyChanged("Videos");
                }
            }
        }
        #endregion

        #region CurrentVideo
        private Video _currentVideo;
        public Video CurrentVideo
        {
            get { return _currentVideo; }
            set
            {
                if (_currentVideo != value)
                {
                    _currentVideo = value;
                    RaisePropertyChanged("CurrentVideo");
                }
            }
        }
        #endregion

        #region SaveVideo
        private Video _saveVideo;
        public Video SaveVideo
        {
            get { return _saveVideo; }
            set
            {
                if (_saveVideo != value)
                {
                    _saveVideo = value;
                    RaisePropertyChanged("SaveVideo");
                }
            }
        }
        #endregion

        private bool _asc;
        private string _order;

        public VideoViewModel()
        {
            Videos = getVideos();
            _asc = true;
            _order = "";
            MainWindow.ClosingWindow += (sender, e) =>
            {
                using (var fs = new FileStream("Libraries/video.xml", FileMode.Truncate))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Video>));
                    xml.Serialize(fs, Videos);
                }
                App.Current.Shutdown();
            };
        }

        private ObservableCollection<Video> getVideos()
        {
            using (var fs = new FileStream("Libraries/video.xml", FileMode.Open))
            {
                XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<Video>));
                var ret = (ObservableCollection<Video>)xml.Deserialize(fs);
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
                    var result = Videos.OrderBy(a => a.Name);
                    Videos = new ObservableCollection<Video>(result);
                }
                else if (param == "Artist")
                {
                    var result = Videos.OrderBy(a => a.Artist);
                    Videos = new ObservableCollection<Video>(result);
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
                    var result = Videos.OrderByDescending(a => a.Name);
                    Videos = new ObservableCollection<Video>(result);
                }
                else if (param == "Artist")
                {
                    var result = Videos.OrderByDescending(a => a.Artist);
                    Videos = new ObservableCollection<Video>(result);
                }
            }
            #endregion
        }
        private bool SortListEvaluate(object context) { return true; }

        public ICommand PlayVideoCommand { get { return new DelegateCommand(PlayVideoCall, PlayVideoEvaluate); } }
        private void PlayVideoCall(object context)
        {
            var listView = context as ListView;
            if (listView != null)
            {
                if (listView.SelectedItem != null)
                {
                    Video Video = listView.SelectedItem as Video;
                    var test = Video.Path;
                    if (Video != null)
                    {
                        this.OnNewMediaRequested(new EventArgsStr { Arg = Video.Path });
                    }
                }
            }
        }
        private bool PlayVideoEvaluate(object context) { return true; }

        public void addToLibrary(string path)
        {
            Video Video = new Video()
            {
                Name = path.Substring(path.LastIndexOf("\\") + 1, path.LastIndexOf(".") - path.LastIndexOf("\\") - 1),
                Artist = "Unknown",
                Path = path
            };
            Videos.Add(Video);
        }

        public void removeVideo(Video Video)
        {
            if (Video != null)
            {
                Videos.Remove(Video);
            }
        }

        public void handleKeydown(System.Collections.IList Videos, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                ObservableCollection<Video> tmp = new ObservableCollection<Video>();
                foreach (Video Video in Videos)
                {
                    tmp.Add(Video);
                }
                foreach (Video Video in tmp)
                {
                    Videos.Remove(Video);
                }
            }
        }

        public void changePropertiesVideo(Video Video)
        {
            throw new NotImplementedException();
        }
    }
}