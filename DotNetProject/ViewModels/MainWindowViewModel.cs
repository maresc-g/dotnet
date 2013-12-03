using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetProject.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region PlayerViewModel
        private ViewModelBase _playerViewModel;
        public ViewModelBase PlayerViewModel
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
        #region LibraryViewModel
        private ViewModelBase _libraryViewModel;
        public ViewModelBase LibraryViewModel
        {
            get { return _libraryViewModel; }
            set
            {
                if (_libraryViewModel != value)
                {
                    _libraryViewModel = value;
                    RaisePropertyChanged("LibraryViewModel");
                }
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            PlayerViewModel = new PlayerViewModel();
            LibraryViewModel = new LibraryViewModel();
        }
    }
}
