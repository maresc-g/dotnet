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
    public class ImageViewModel : LibraryViewModel
    {
        #region ImageMedias
        private ObservableCollection<ImageMedia> _imageMedias;
        public ObservableCollection<ImageMedia> ImageMedias
        {
            get { return _imageMedias; }
            set
            {
                if (_imageMedias != value)
                {
                    _imageMedias = value;
                    RaisePropertyChanged("ImageMedias");
                }
            }
        }
        #endregion

        #region CurrentImageMedia
        private ImageMedia _currentImageMedia;
        public ImageMedia CurrentImageMedia
        {
            get { return _currentImageMedia; }
            set
            {
                if (_currentImageMedia != value)
                {
                    _currentImageMedia = value;
                    RaisePropertyChanged("CurrentImageMedia");
                }
            }
        }
        #endregion

        #region SaveImageMedia
        private ImageMedia _saveImageMedia;
        public ImageMedia SaveImageMedia
        {
            get { return _saveImageMedia; }
            set
            {
                if (_saveImageMedia != value)
                {
                    _saveImageMedia = value;
                    RaisePropertyChanged("SaveImageMedia");
                }
            }
        }
        #endregion

        private bool _asc;
        private string _order;

        public ImageViewModel()
        {
            ImageMedias = getImageMedias();
            _asc = true;
            _order = "";
            MainWindow.ClosingWindow += (sender, e) =>
            {
                using (var fs = new FileStream("Libraries/image.xml", FileMode.Truncate))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<ImageMedia>));
                    xml.Serialize(fs, ImageMedias);
                }
                App.Current.Shutdown();
            };
        }

        private ObservableCollection<ImageMedia> getImageMedias()
        {
            using (var fs = new FileStream("Libraries/image.xml", FileMode.Open))
            {
                XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<ImageMedia>));
                var ret = (ObservableCollection<ImageMedia>)xml.Deserialize(fs);
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
                    var result = ImageMedias.OrderBy(a => a.Name);
                    ImageMedias = new ObservableCollection<ImageMedia>(result);
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
                    var result = ImageMedias.OrderByDescending(a => a.Name);
                    ImageMedias = new ObservableCollection<ImageMedia>(result);
                }
            }
            #endregion
        }
        private bool SortListEvaluate(object context) { return true; }

        public ICommand PlayImageMediaCommand { get { return new DelegateCommand(PlayImageMediaCall, PlayImageMediaEvaluate); } }
        private void PlayImageMediaCall(object context)
        {
            var listView = context as ListView;
            if (listView != null)
            {
                if (listView.SelectedItem != null)
                {
                    ImageMedia ImageMedia = listView.SelectedItem as ImageMedia;
                    var test = ImageMedia.Path;
                    if (ImageMedia != null)
                    {
                        this.OnNewMediaRequested(new EventArgsStr { Arg = ImageMedia.Path });
                    }
                }
            }
        }
        private bool PlayImageMediaEvaluate(object context) { return true; }

        public void addToLibrary(string path)
        {
            ImageMedia ImageMedia = new ImageMedia()
            {
                Name = path.Substring(path.LastIndexOf("\\") + 1, path.LastIndexOf(".") - path.LastIndexOf("\\") - 1),
                Path = path
            };
            ImageMedias.Add(ImageMedia);
        }

        public void removeImageMedia(ImageMedia ImageMedia)
        {
            if (ImageMedia != null)
            {
                ImageMedias.Remove(ImageMedia);
            }
        }

        public void handleKeydown(System.Collections.IList ImageMedias, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                ObservableCollection<ImageMedia> tmp = new ObservableCollection<ImageMedia>();
                foreach (ImageMedia ImageMedia in ImageMedias)
                {
                    tmp.Add(ImageMedia);
                }
                foreach (ImageMedia ImageMedia in tmp)
                {
                    ImageMedias.Remove(ImageMedia);
                }
            }
        }

        public void changePropertiesImageMedia(ImageMedia ImageMedia)
        {
            throw new NotImplementedException();
        }
    }
}
