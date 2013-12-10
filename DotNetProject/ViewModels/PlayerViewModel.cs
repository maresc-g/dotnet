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
            LibraryViewModel.NewMediaRequested += NewMediaRequestedHandler;
        }

        public void NewMediaRequestedHandler(object sender, EventArgsStr e)
        {
            Filename = e.Arg;
        }

        #region BrowseCommand
        public ICommand BrowseCommand { get { return new DelegateCommand(BrowseCall, BrowseEvaluate); } }
        private void BrowseCall(object context)
        {
            dlg.Filter = "Media files (*.mp3;*.mp4;*.wma;*.wav;*.jpg;*.jpeg;*.bmp;*.png;*.gif)|*.mp3;*.mp4;*.wma;*.wav;*.jpg;*.jpeg;*.bmp;*.png;*.gif|Music files (*.mp3;*.wma;*.wav)|*.mp3;*.wma;*.wav|Video files (*.mp4)|*.mp4|Images files (*.jpg;*.jpeg;*.bmp;*.png;*.gif)|*.jpg;*.jpeg;*.bmp;*.png;*.gif|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                Filename = dlg.FileName;
                if (this.PlayRequested != null)
                {
                    this.PlayRequested(this, EventArgs.Empty);
                }
            }
        }
        private bool BrowseEvaluate(object context) { return true; }
        #endregion
        #region PlayCommand
        public event EventHandler PlayRequested;
        public ICommand PlayCommand { get { return new DelegateCommand(PlayCall, PlayEvaluate); } }
        private void PlayCall(object context)
        {
            if (this.PlayRequested != null)
            {
                this.PlayRequested(this, EventArgs.Empty);
            }
        }
        private bool PlayEvaluate(object context) { return true; }
        #endregion
        #region PauseCommand
        public event EventHandler PauseRequested;
        public ICommand PauseCommand { get { return new DelegateCommand(PauseCall, PauseEvaluate); } }
        private void PauseCall(object context)
        {
            if (this.PauseRequested != null)
            {
                this.PauseRequested(this, EventArgs.Empty);
            }
        }
        private bool PauseEvaluate(object context) { return true; }
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
    }
}
