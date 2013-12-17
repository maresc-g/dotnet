using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DotNetProject.ViewModels
{
    public class PlayerViewModel : ViewModelBase
    {
        #region Filename
        private string _filename { get; set; }
        public string Filename
        {
            get { return _filename; }
            set
            {
                if (_filename == value)
                    return;
                _filename = value;
                RaisePropertyChanged("Filename");
            }
        }
        #endregion
        #region FileDuration
        private Duration fileDuration { get; set; }
        public Duration FileDuration
        {
            get { return fileDuration; }
            set
            {
                if (fileDuration != value)
                {
                    fileDuration = value;
                    RaisePropertyChanged("FileDuration");
                }
            }
        }
        #endregion

        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

        public PlayerViewModel()
        {
            CurrentPlaylistViewModel.NewMediaRequested += NewMediaRequestedHandler;
        }

        public void NewMediaRequestedHandler(object sender, string e)
        {
            Filename = e;
            PlayPauseCall(0);
        }

        public void songEnded()
        {
            NextSongCall(null);
        }

        #region PlayPauseCommand
        public event EventHandler PlayRequested;
        public event EventHandler PauseRequested;
        public void PlayPauseCall(int state)
        {
            if (state == 0)
            {
                if (this.PlayRequested != null)
                {
                    this.PlayRequested(this, EventArgs.Empty);
                }
            }
            else if (state == 1)
            {
                if (this.PauseRequested != null)
                {
                    this.PauseRequested(this, EventArgs.Empty);
                }
            }
        }
        #endregion

        #region StopCommand
        public event EventHandler StopRequested;
        public ICommand StopCommand { get { return new DelegateCommand(StopCall, StopEvaluate); } }
        private void StopCall(object context)
        {
            if (this.StopRequested != null)
            {
                this.StopRequested(this, EventArgs.Empty);
            }
        }
        private bool StopEvaluate(object context) { return true; }
        #endregion

        #region NextSongCommand
        static public event EventHandler NextSongRequested;
        public ICommand NextSongCommand { get { return new DelegateCommand(NextSongCall, NextSongEvaluate); } }
        private void NextSongCall(object context)
        {
            if (PlayerViewModel.NextSongRequested != null)
            {
                PlayerViewModel.NextSongRequested(this, EventArgs.Empty);
            }
        }
        private bool NextSongEvaluate(object context) { return true; }
        #endregion

        #region PreviousSongCommand
        static public event EventHandler PreviousSongRequested;
        public ICommand PreviousSongCommand { get { return new DelegateCommand(PreviousSongCall, PreviousSongEvaluate); } }
        private void PreviousSongCall(object context)
        {
            if (PlayerViewModel.PreviousSongRequested != null)
            {
                PlayerViewModel.PreviousSongRequested(this, EventArgs.Empty);
            }
        }
        private bool PreviousSongEvaluate(object context) { return true; }
        #endregion

        #region RandomCommand
        static public event EventHandler RandomRequested;
        public ICommand RandomCommand { get { return new DelegateCommand(RandomCall, RandomEvaluate); } }
        private void RandomCall(object context)
        {
            if (PlayerViewModel.RandomRequested != null)
            {
                PlayerViewModel.RandomRequested(this, EventArgs.Empty);
            }
        }
        private bool RandomEvaluate(object context) { return true; }
        #endregion

        #region RepeatCommand
        static public event EventHandler RepeatRequested;
        public ICommand RepeatCommand { get { return new DelegateCommand(RepeatCall, RepeatEvaluate); } }
        private void RepeatCall(object context)
        {
            if (PlayerViewModel.RepeatRequested != null)
            {
                PlayerViewModel.RepeatRequested(this, EventArgs.Empty);
            }
        }
        private bool RepeatEvaluate(object context) { return true; }
        #endregion
    }
}
