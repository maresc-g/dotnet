﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DotNetProject.ViewModels
{
    public class EventArgsStr : EventArgs
    {
        public string Arg { get; set; }
    }

    public class MainWindowViewModel : ViewModelBase
    {
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

        static public event EventHandler<EventArgsStr> ChangeView;
        static public event EventHandler AddToLibrary;

        #region CurrentViewModel
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    RaisePropertyChanged("CurrentViewModel");
                }
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            CurrentViewModel = new CurrentViewModel();
        }

        #region ChangeViewCommand
        public ICommand ChangeViewCommand { get { return new DelegateCommand(ChangeViewCall, ChangeViewEvaluate); } }
        private void ChangeViewCall(object context)
        {
            var item = context as TreeViewItem;
            if (item != null)
            {
                var header = item.Header as string;
                if (MainWindowViewModel.ChangeView != null)
                {
                    MainWindowViewModel.ChangeView(this, new EventArgsStr { Arg = header });
                }
            }
        }
        private bool ChangeViewEvaluate(object context) { return true; }
        #endregion

        public void addToLibrary()
        {
            if (MainWindowViewModel.AddToLibrary != null)
            {
                MainWindowViewModel.AddToLibrary(this, EventArgs.Empty);
            }
        }
    }
}
